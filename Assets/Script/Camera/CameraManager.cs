using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField]
    Transform CameraParent;
    [SerializeField]
    float ShakeDuration;
    [SerializeField]
    float ShakeMag;

 
    void Start()
    {
        if(Instance==null)
        {
            Instance = this;
        }
   
    }


    public void ShakeCamera()
    {
    
       StartCoroutine(ShakeCort(ShakeDuration, ShakeMag));
        

    }
    float timer = 0;
    IEnumerator ShakeCort(float duration,float magnitude)
    {
        timer -= duration;
        while (timer< duration)
        {
           // Camera.main.fieldOfView = 50;
            CameraParent.position = Random.insideUnitSphere * magnitude + CameraParent.position;
            timer += Time.deltaTime;
            yield return null;
        }
        CameraParent.position = new Vector3(0, 0, 0);
        
       // StartCoroutine(ZoomReturn());

    }
    IEnumerator ZoomReturn()
    {

        yield return new WaitForSeconds(1.5f);
        Camera.main.fieldOfView = 60;
        yield return null;
    }

}
