using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleManger : MonoBehaviour
{
    public static BattleManger Instance;

    [Header("데미지텍스트 프리팹"), SerializeField]
    GameObject DamageTextPrefab;
    [Header("크리티컬데미지텍스트 프리팹"), SerializeField]
    GameObject CriticalDamageTextPrefab;
    [Header("데미지텍스트 풀"), SerializeField]
    Transform DamageTextPoolTransform;
    [Header("타격이펙트 풀"), SerializeField]
    Transform HitEffectPoolTransform;
    [Header("타격이펙트 프리팹"), SerializeField]
    GameObject HitEffectPrefab;
    [Header("리워드 프리팹"), SerializeField]
    GameObject RewardObj;
    [Header("리워드텍스트 풀"), SerializeField]
    Transform RewardTextPoolTransform;

    [Header("데미지텍스트 초기 개수"), SerializeField]
    int damageTextPrefabCount;
    [Header("타격이펙트 초기 개수"), SerializeField]
    int HitEffectPrefabCount;
    [Header("전투리워드 텍스트프리팹 초기 개수"), SerializeField]
    int RewardTextPrefabCount;


    Queue<GameObject> CriticalDamageTextQueue;
    Queue<GameObject> DamageTextQueue;
    Queue<GameObject> HitEffectQueue;
    Queue<GameObject> RewardTexttQueue;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject); 
        }
        GameInit();
    }


    void GameInit()
    {
        CriticalDamageTextQueue = ObjectPooling.Instance.CreateQueue();
        DamageTextQueue = ObjectPooling.Instance.CreateQueue();
        HitEffectQueue = ObjectPooling.Instance.CreateQueue();
        RewardTexttQueue = ObjectPooling.Instance.CreateQueue();
        for (int i = 0; i < damageTextPrefabCount; i++)
        {
            ObjectPooling.Instance.CreatePoolObj(DamageTextQueue, DamageTextPoolTransform, DamageTextPrefab);
            ObjectPooling.Instance.CreatePoolObj(CriticalDamageTextQueue, DamageTextPoolTransform, CriticalDamageTextPrefab);
        }

        for (int i = 0; i < HitEffectPrefabCount; i++)
        {
            ObjectPooling.Instance.CreatePoolObj(HitEffectQueue, HitEffectPoolTransform, HitEffectPrefab);
        }
        for (int i = 0; i < RewardTextPrefabCount; i++)
        {
            ObjectPooling.Instance.CreatePoolObj(RewardTexttQueue, RewardTextPoolTransform, RewardObj);
        }
 

    }
    //데미지텍스트 오브젝트 풀링
    public void SendDamageText(int damage, Transform parent)
    {
        GameObject damagetext = ObjectPooling.Instance.GetPoolObj(DamageTextQueue, parent, DamageTextPrefab);
        damagetext.GetComponent<TextMeshPro>().text = damage.ToString();
        damagetext.transform.position = parent.position;
        damagetext.GetComponent<Damage>().Restart();
    }
    public void ReceiveDamageText(GameObject DamageTextObj)
    {
        ObjectPooling.Instance.ReturnPoolObj(DamageTextQueue, DamageTextPoolTransform, DamageTextObj);
    }
    public void SendCrtDamageText(int damage, Transform parent)
    {
        GameObject damagetext = ObjectPooling.Instance.GetPoolObj(CriticalDamageTextQueue, parent, CriticalDamageTextPrefab);
        damagetext.GetComponent<TextMeshPro>().text = damage.ToString();
        damagetext.transform.position = parent.position;
        damagetext.GetComponent<CrtDamage>().Restart();
    }
    public void ReceiveCrtDamageText(GameObject CrtDamageTextObj)
    {
        ObjectPooling.Instance.ReturnPoolObj(CriticalDamageTextQueue, DamageTextPoolTransform, CrtDamageTextObj);
    }
    //타격이펙트 오브젝트 풀링
    public void SendHitEffect(Transform monster)
    {
        GameObject hiteffectprefab = ObjectPooling.Instance.GetPoolObj(HitEffectQueue, monster, HitEffectPrefab);
        hiteffectprefab.transform.position = monster.position + new Vector3(0, 0, -12);
    }
    public void ReceiveHitEffect(GameObject prefab)
    {
        ObjectPooling.Instance.ReturnPoolObj(HitEffectQueue, HitEffectPoolTransform, prefab);
    }

    //전투메세지 오브젝트 풀링
    public void SendRewardText(int value, RewardType type)
    {

        GameObject RewardObjPrefab = ObjectPooling.Instance.GetPoolObj(RewardTexttQueue, RewardTextPoolTransform.transform, RewardObj);
        RewardObjPrefab.GetComponent<RewardObj>().SendMessage(type, value);//6.22 널래퍼런스 발견.
        StartCoroutine(returnMessage(RewardObjPrefab));

    }
    
    IEnumerator returnMessage(GameObject rewardobj)
    {

        yield return new WaitForSeconds(1.5f);
        ReceiveRewardText(rewardobj);
        yield return null;
    }
    public void ReceiveRewardText(GameObject RewardTextObj)
    {
        ObjectPooling.Instance.ReturnPoolObj(RewardTexttQueue, RewardTextPoolTransform, RewardTextObj);

    }
}

