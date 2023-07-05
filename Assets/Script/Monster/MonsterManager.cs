using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance;
    [Header("솔져 프리팹"), SerializeField]
    GameObject SoldierPrefab;
    [Header("솔져 풀"), SerializeField]
    Transform SoldierPoolTransform;
    [Header("솔져 생성 좌표"), SerializeField]
    Vector3 SoldierPos=new Vector3(-135,0,-33);
    [Header("솔져 최대 소환 수"), SerializeField]
    int SoldierMaxNum;

    [Header("리퍼 프리팹"), SerializeField]
    GameObject ReaperPrefab;
    [Header("리퍼 풀"), SerializeField]
    Transform ReaperPoolTransform;
    [Header("리퍼 생성 좌표"), SerializeField]
    Vector3 ReaperPos = new Vector3(-135, 0, -33);
 

    [Header("코인 프리팹"), SerializeField]
    GameObject CoinPrefab;
    [Header("코인 풀"), SerializeField]
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
//공격이 가끔안맞는문제, 솔져가 플레이어 인식못하는문제, 재생된몬스터의 기울기