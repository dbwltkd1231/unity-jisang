using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
   
    private void FixedUpdate()
    {
        transform.position += Vector3.up * Time.unscaledDeltaTime*2;
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void Restart()
    {
        StartCoroutine(DamageTextRemove());
    }
    IEnumerator DamageTextRemove()
    {
        yield return new WaitForSeconds(1f);
        BattleManger.Instance.ReceiveDamageText(this.gameObject);
        yield return null;
    }
}
