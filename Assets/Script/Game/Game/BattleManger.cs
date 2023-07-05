using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleManger : MonoBehaviour
{
    public static BattleManger Instance;

    [Header("�������ؽ�Ʈ ������"), SerializeField]
    GameObject DamageTextPrefab;
    [Header("ũ��Ƽ�õ������ؽ�Ʈ ������"), SerializeField]
    GameObject CriticalDamageTextPrefab;
    [Header("�������ؽ�Ʈ Ǯ"), SerializeField]
    Transform DamageTextPoolTransform;
    [Header("Ÿ������Ʈ Ǯ"), SerializeField]
    Transform HitEffectPoolTransform;
    [Header("Ÿ������Ʈ ������"), SerializeField]
    GameObject HitEffectPrefab;
    [Header("������ ������"), SerializeField]
    GameObject RewardObj;
    [Header("�������ؽ�Ʈ Ǯ"), SerializeField]
    Transform RewardTextPoolTransform;

    [Header("�������ؽ�Ʈ �ʱ� ����"), SerializeField]
    int damageTextPrefabCount;
    [Header("Ÿ������Ʈ �ʱ� ����"), SerializeField]
    int HitEffectPrefabCount;
    [Header("���������� �ؽ�Ʈ������ �ʱ� ����"), SerializeField]
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
    //�������ؽ�Ʈ ������Ʈ Ǯ��
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
    //Ÿ������Ʈ ������Ʈ Ǯ��
    public void SendHitEffect(Transform monster)
    {
        GameObject hiteffectprefab = ObjectPooling.Instance.GetPoolObj(HitEffectQueue, monster, HitEffectPrefab);
        hiteffectprefab.transform.position = monster.position + new Vector3(0, 0, -12);
    }
    public void ReceiveHitEffect(GameObject prefab)
    {
        ObjectPooling.Instance.ReturnPoolObj(HitEffectQueue, HitEffectPoolTransform, prefab);
    }

    //�����޼��� ������Ʈ Ǯ��
    public void SendRewardText(int value, RewardType type)
    {

        GameObject RewardObjPrefab = ObjectPooling.Instance.GetPoolObj(RewardTexttQueue, RewardTextPoolTransform.transform, RewardObj);
        RewardObjPrefab.GetComponent<RewardObj>().SendMessage(type, value);//6.22 �η��۷��� �߰�.
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

