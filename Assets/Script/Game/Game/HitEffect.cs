using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    float time;
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf==true)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                BattleManger.Instance.ReceiveHitEffect(this.gameObject);
                time = 0;
            }
        }

        
    }
}
