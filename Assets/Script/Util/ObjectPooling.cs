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
            Instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (Instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
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
