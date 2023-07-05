using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffSlot : MonoBehaviour
{

    public IEnumerator BuffPrefabLife(float time)
    {
        Image thisSprite= this.GetComponent<Image>();

        thisSprite.fillAmount = 1f;//√ ±‚»≠
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(time/5));
        thisSprite.fillAmount = 0.8f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(time / 5));
        thisSprite.fillAmount = 0.6f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(time / 5));
        thisSprite.fillAmount = 0.4f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(time / 5));
        thisSprite.fillAmount = 0.2f;
        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(time / 5));
        SkillManager.Instance.ReceiveBuffSlot(this.gameObject);
        yield return null;
    }




}
