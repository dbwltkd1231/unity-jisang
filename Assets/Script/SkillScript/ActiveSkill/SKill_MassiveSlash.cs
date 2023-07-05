using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKill_MassiveSlash : Skill_Base
{
    [SerializeField]
    GameObject massiveSlashPrefab;

   
    

    public override void Execute()
    {
        StartCoroutine(SKillOn());
    }
    IEnumerator SKillOn()
    {
        PlayerManager.Instance.SkillIng = true;
        this.transform.SetParent(PlayerManager.Instance.transform,false);
        massiveSlashPrefab.SetActive(true);
        PlayerManager.Instance.anim.SetTrigger("MSlash");
        massiveSlashPrefab.transform.localPosition = new Vector3(0, 1, -6);//Player.player.transform.position+new Vector3(-5,2,-1);
        massiveSlashPrefab.transform.rotation = PlayerManager.Instance.transform.rotation;
        massiveSlashPrefab.GetComponent<MssiveSlashCtrl>().SkillOn(SkillMag);

        yield return StartCoroutine(GameManager.Instance.WaitForRealSeconds(2f));
        massiveSlashPrefab.SetActive(false);
        skilling = false;
        this.transform.parent = null;
        this.transform.position = new Vector3(0, 0, 0);
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        PlayerManager.Instance.SkillIng = false;
        yield return null;
    }    

}
