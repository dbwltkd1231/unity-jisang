using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class zenith : MonoBehaviour
{
    [SerializeField]
    GameObject SreenYellow;

    Ray ray;
    RaycastHit hit;
    GameObject target;

    bool OneShot_find = false;
    bool OneShot_Inst = false;
    float OneSHot_Duration = 0.2f;

    void Start()
    {
        SreenYellow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(SixShot());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OneShot_Inst = true;
            StartCoroutine(OneShot());
        }

        if (OneShot_Inst == true)
        {
            OneShotFind();
        }
    }
    void OneShotFind()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Monster"))
            {
                OneSHot_Duration -= Time.deltaTime;
                if (OneSHot_Duration < 0f)
                {
                    target = hit.collider.gameObject;
                    OneShot_find = true;
                    OneShot_Inst = false;
                    OneSHot_Duration = 0.2f;
                }

            }
            else
            {
                OneSHot_Duration = 0.2f;
            }
        }
    }
    IEnumerator SixShot()
    {
        Vector3 originpos = this.transform.position;
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        Collider[] monsters2 = Physics.OverlapSphere(this.transform.position, 10f);

        for (int i = 0; i < monsters2.Length; i++)
        {
            if (monsters2[i].gameObject.CompareTag("Monster"))
            {
                monsters[i] = monsters2[i].gameObject;
            }
        }



        TimeSlow(0.1f);
        SreenYellow.SetActive(true);
        for (int i = 0; i < (monsters.Length > 6 ? 6 : monsters.Length); i++)
        {
            GameObject target = monsters[i];
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            Vector3 GoalPos = target.transform.position + (dir * 1.3f);
            transform.forward = dir;
            yield return new WaitForSeconds(0.01f);
            //케릭터 기모으는 애니메이션 true
            this.transform.position = GoalPos;
        }
        this.transform.position = originpos;
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < monsters.Length; i++)
        {
            Destroy(monsters[i].gameObject);
        }

        //몬스터 데스트리거 true


        TimeReset();
        SreenYellow.SetActive(false);

        yield return null;
    }

    IEnumerator OneShot()
    {
        SreenYellow.SetActive(true);
        TimeSlow(0.1f);

        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => OneShot_find == true);

        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        Vector3 GoalPos = target.transform.position + (dir * 1.3f);
        transform.forward = dir;
        this.transform.position = GoalPos;
        yield return new WaitForSeconds(0.2f);
        //케릭터 기모으는 애니메이션 true
        Destroy(target);

        /*
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000f);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Monster"))
            {
                target = hit.collider.gameObject;
                Vector3 dir = target.transform.position - transform.position;
                dir.y = 0;
                dir.Normalize();
                Vector3 GoalPos = target.transform.position + (dir * 1.3f);
                transform.forward = dir;
                this.transform.position = GoalPos;
                yield return new WaitForSeconds(0.2f);
                //케릭터 기모으는 애니메이션 true
                Destroy(target);

            }
        }
        */
        TimeReset();
        SreenYellow.SetActive(false);
        yield return null;


    }

   void TimeSlow(float Scale)
    {
        Time.timeScale = Scale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
     void TimeReset()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    void GetMinMax(List<float> array, out float min, out float max)
    {
        min = array[0];
        max = array[0];

        for (int i = 0; i < array.Count; i++)
        {
            if (min > array[i])
                min = array[i];

            else if (min < array[i])
                max = array[i];
        }
    }
}
   
