Shader "Custom/tree"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Transp("Transparency", Range(0, 1)) = 1
        _Color("Color",Color) = (1,1,1,1)
       _Metallic("Metallic", Range(0,1)) = 0.0
        _BumpMap("NormalMap",2D) = "bump"{}
        _BumpRate("Normal Rate",Range(0,2))=1
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
            zwrite on

                ColorMask 0

                CGPROGRAM
                #pragma surface surf nolight noambient noforwardadd nolightmap novertexlights noshadow
                #pragma enable_d3d11_debug_symbols
                
                struct Input {
                    float3 color:COLOR;
                };

                void surf(Input IN, inout SurfaceOutput o) {}

                float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten)
                {
                    return float4(0, 0, 0, 0);
                }
                ENDCG

                    //  2nd Pass ------------------------------
                    zwrite off
                    CGPROGRAM
                    #pragma surface surf Lambert alpha:fade
                    sampler2D _MainTex;
                sampler2D _BumpMap;
                    struct Input { 
                        float2 uv_MainTex; 
                        float2 uv_BumpMap;
                    };
                    float _Transp;
                    float4 _Color;
                    half _Metallic;
                    float _BumpRate;
                    void surf(Input IN, inout SurfaceOutput o)
                    {
                        fixed4 c = tex2D(_MainTex, IN.uv_MainTex)*_Color;
                        fixed3 Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
                        if (_BumpRate >= 0.01)
                        {
                            o.Normal = float3(Normal.x * _BumpRate, Normal.y * _BumpRate, Normal.z);
                        }
                        o.Albedo = c.rgb;
                        o.Alpha = _Transp;
                    }
                    ENDCG

        }// SubShader
            FallBack "Diffuse"

}
//³ë¸»¸Ê
//¸ÞÅ»¸¯