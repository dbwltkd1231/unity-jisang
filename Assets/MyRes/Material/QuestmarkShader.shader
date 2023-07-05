Shader "Custom/QuestmarkShader"
{
    Properties
    {
        // _FlickerTime("Flicker Time", Range(0,10)) = 1
        _HoloColor("Holo Color", Color) = (1,1,1,1)

        _HoloCycle("Hologram Cycle", Range(0,10)) = 1

        _RimThickness("RimLight Thickness", Float) = 3
        _HoloInterval("Hologram Interval", Float) = 1
        _HoloThickness("Hologram Thickness", Float) = 1

         _MainTex("Albedo (RGB)", 2D) = "white" {}
         _FlickerTime("Flicker Time", Range(0,10)) = 1
    }
        SubShader
         {
              Tags { "RenderType" = "Fade" "Queue" = "Transparent" }

              CGPROGRAM
              #pragma surface surf Lambert noambient alpha:fade

             // Use shader model 3.0 target, to get nicer looking lighting
          float4 _HoloColor;
         half _HoloCycle;
         //half _FlickerTime;
         struct Input
         {
            float2 uv_MainTex;
            float3 viewDir;
            //  월드공간상의 좌표 정보..
            float3 worldPos;
            float4 color:COLOR;
         };

         float _RimThickness;
         float _HoloInterval;
         float _HoloThickness;

         sampler2D _MainTex;
         half _FlickerTime;

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Emission = c.rgb + IN.color.rgb; // 자체 발광 o.Emission = IN.color.rgb;
                
           

             float rim = saturate(dot(o.Normal, IN.viewDir));

              o.Emission = c.rgb + _HoloColor;
              // o.Emission = 0;
              // o.Emission = IN.worldPos.g;
            // o.Emission = frac(IN.worldPos.g);


             //rim = pow(1 - rim, 3);
             rim = pow(1 - rim, _RimThickness)
                 + pow(frac(IN.worldPos.g * _HoloInterval - _Time.y * _HoloCycle), _HoloThickness);


            o.Alpha = c.a*rim * abs(sin(_Time.y * _FlickerTime));
            //o.Alpha = 1;
           // o.Alpha = rim;
           }
           ENDCG
         }
             FallBack "Diffuse"
}