using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDanceCtrl : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] myobjs;
    

    void Start()
    {
        myobjs = GetComponentsInChildren<ParticleSystem>();
    


    }
    
    public void SKillOn()
    {
        int i = 0;
        for(;i<10;i++)
        {
            myobjs[i].startDelay = 0f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 0.1f), Random.Range(0, 0.1f), Random.Range(0, 0.1f));
            myobjs[i].Play();
        }
        for (; i < 20; i++)
        {
            myobjs[i].startDelay = 0.1f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            myobjs[i].Play();
        }
        for(;i<30;i++)
        {
            myobjs[i].startDelay = 0.2f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));
            myobjs[i].Play();
        }
        for (; i < 40; i++)
        {
            myobjs[i].startDelay = 0.3f;
           myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-5,5), 0, Random.Range(-5, 5));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 4f), Random.Range(0, 4f), Random.Range(0, 4f));
            myobjs[i].Play();
        }
        for (; i < 50; i++)
        {
            myobjs[i].startDelay = 0.4f;
           myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 10f), Random.Range(0, 10f), Random.Range(0, 10f));
            myobjs[i].Play();
        }
        for (; i < 60; i++)
        {
            myobjs[i].startDelay = 0.5f;
           myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));
            myobjs[i].Play(); ;
        }
        for (; i < 64; i++)
        {
            myobjs[i].startDelay = 1.45f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 2.5f), Random.Range(0, 2.5f), Random.Range(0, 2.5f));
            myobjs[i].Play();
        }
        for (; i < 74; i++)
        {
            myobjs[i].startDelay = 0.85f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));
            myobjs[i].Play();
        }
        for (; i < 84; i++)
        {
            myobjs[i].startDelay = 0.95f;
           myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), Random.Range(-8, 8));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));
            myobjs[i].Play();
        }
        for (; i < 94; i++)
        {
            myobjs[i].startDelay = 1.05f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-6, 6), Random.Range(-6, 6), Random.Range(-6, 6));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 2f), Random.Range(0, 2f), Random.Range(0, 2f));
            myobjs[i].Play();
        }
        for (; i < 104; i++)
        {
            myobjs[i].startDelay = 1.15f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            myobjs[i].Play();

        }
        for (; i < 114; i++)
        {
            myobjs[i].startDelay = 1.25f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            myobjs[i].Play();
        }
        for (; i < 124; i++)
        {
            myobjs[i].startDelay = 1.35f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(0,0,0);
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            myobjs[i].Play();
        }
        for (; i < myobjs.Length; i++)
        {
            myobjs[i].startDelay = 2.75f;
            myobjs[i].gameObject.transform.localPosition = new Vector3(15, 0, 15);
            myobjs[i].gameObject.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            myobjs[i].gameObject.transform.localScale += new Vector3(Random.Range(0, 1f), 0,0);
            myobjs[i].Play();
        }
    }

   
   
   
}