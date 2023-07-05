using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance;
    [Header("���� ������"), SerializeField]
    GameObject SoldierPrefab;
    [Header("���� Ǯ"), SerializeField]
    Transform SoldierPoolTransform;
    [Header("���� ���� ��ǥ"), SerializeField]
    Vector3 SoldierPos=new Vector3(-135,0,-33);
    [Header("���� �ִ� ��ȯ ��"), SerializeField]
    int SoldierMaxNum;

    [Header("���� ������"), SerializeField]
    GameObject ReaperPrefab;
    [Header("���� Ǯ"), SerializeField]
    Transform ReaperPoolTransform;
    [Header("���� ���� ��ǥ"), SerializeField]
    Vector3 ReaperPos = new Vector3(-135, 0, -33);
 

    [Header("���� ������"), SerializeField]
    GameObject CoinPrefab;
    [Header("���� Ǯ"), SerializeField]
    Transform CoinPoolTransform;
    

    Queue<GameObject> CoinQueue;
    Queue<GameObject> SoldierQueue;
    Queue<GameObject> ReqperQueue;
    delegate void Regen();
    Regen regen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
         
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        init();
    }

    void init()
    {
        SoldierQueue = ObjectPooling.Instance.CreateQueue();
        CoinQueue = ObjectPooling.Instance.CreateQueue();
        ReqperQueue = ObjectPooling.Instance.CreateQueue();
        for (int i = 0; i < 3; i++)
        {
            ObjectPooling.Instance.CreatePoolObj(SoldierQueue, SoldierPoolTransform, SoldierPrefab);
            ObjectPooling.Instance.CreatePoolObj(CoinQueue, CoinPoolTransform, CoinPrefab);
            
        }
        ObjectPooling.Instance.CreatePoolObj(ReqperQueue, ReaperPoolTransform, ReaperPrefab);
        SendSoldier();
        SendReaper();
    }
    public void SendCoin(Vector3 pos,int mag)
    {
        GameObject Coin = ObjectPooling.Instance.GetPoolObj(CoinQueue, CoinPoolTransform, CoinPrefab);
        Coin.GetComponent<Coin>().CoinMag = mag;
        Coin.transform.localPosition= pos;
    }
    public void RecevieCoin(GameObject coin)
    {
        ObjectPooling.Instance.ReturnPoolObj(CoinQueue, CoinPoolTransform, coin);
    }
    public void SendSoldier()
    {
        if(SoldierQueue.Count== SoldierMaxNum)
        {
            for(int i=0;i< SoldierMaxNum;i++)
            {
                GameObject Soldier = ObjectPooling.Instance.GetPoolObj(SoldierQueue, SoldierPoolTransform, SoldierPrefab);
                Soldier.transform.localPosition = SoldierPos + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                Soldier.GetComponent<Soldier>().Init();
            }
            
        }
        
    }
    public void ReceiveSoldier(GameObject prefab)
    {
        ObjectPooling.Instance.ReturnPoolObj(SoldierQueue, SoldierPoolTransform, prefab);
        regen = SendSoldier;
        StartCoroutine(RegenTimer(prefab.GetComponent<Monster>().rezenTIme));
    }

    public void SendReaper()
    {

        GameObject Reaper = ObjectPooling.Instance.GetPoolObj(ReqperQueue, ReaperPoolTransform, ReaperPrefab);
        Reaper.transform.localPosition = ReaperPos + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        Reaper.GetComponent<Reaper>().Init();
    }
    public void ReceiveReaper(GameObject prefab)
    {
        ObjectPooling.Instance.ReturnPoolObj(ReqperQueue, ReaperPoolTransform, prefab);
        regen = SendReaper;
        
        StartCoroutine(RegenTimer(prefab.GetComponent<Monster>().rezenTIme));
    }
    IEnumerator RegenTimer(float time)
    {
        yield return new WaitForSeconds(time);
        regen();
        yield return null;

    }
}
//������ �����ȸ´¹���, ������ �÷��̾� �νĸ��ϴ¹���, ����ȸ����� ����