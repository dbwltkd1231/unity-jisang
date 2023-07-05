using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (Instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }

  
    public Queue<GameObject> CreateQueue()
    {
        Queue<GameObject> PoolingQueue = new Queue<GameObject>();

        return PoolingQueue;
    }
    public GameObject CreatePoolObj(Queue<GameObject> queue,Transform parent,GameObject prefab)
    {
        GameObject PoolObj = Instantiate(prefab);
        PoolObj.SetActive(false);
        PoolObj.transform.SetParent(parent);
        queue.Enqueue(PoolObj);
        return PoolObj;
    }
    public GameObject GetPoolObj(Queue<GameObject> queue, Transform parent, GameObject prefab)
    {
        GameObject newObj=null;
       
        
        if (queue.Count > 0)
        {
            newObj = queue.Dequeue();
            //newObj.transform.SetParent(parent);
            newObj.gameObject.SetActive(true);

        }
        else
        {
            newObj = CreatePoolObj(queue,parent, prefab);
            newObj.gameObject.SetActive(true);
            //newObj.transform.SetParent(parent);

        }
        
        


        return newObj;
    }
    public void ReturnPoolObj(Queue<GameObject> queue, Transform parent, GameObject PoolObj)
    {
        PoolObj.SetActive(false);
        PoolObj.transform.SetParent(parent);
        queue.Enqueue(PoolObj);


    }
}
