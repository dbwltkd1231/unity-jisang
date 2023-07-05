using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text;
using UnityEngine.EventSystems;

[Serializable]
public class HotKey : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]
    TextMeshProUGUI SkillKeyText;
    [SerializeField]
    public Image SkillImage;
    GameObject SkillObj;
    public Skill_Base Skill;
    [Header("Hot KeyCode"),SerializeField]
    public string keyCode;
    [SerializeField]
    bool PortionSlot;
    [Header("ID"), SerializeField]
    public int ID;

    public SkillSlot ConnectedSkillSlot;
    StringBuilder tmp;
    private void Start()
    {
        SetKeycode();
        /*
        if (SkillObj == null)
        {
            if (PortionSlot == false)
            {
                SkillImage.sprite = UiManager.uimanager.SlotBasicSprite;
            }

            else
            {
                SkillImage.sprite = UiManager.uimanager.SlotBasicCircleSprite;
            }

        }
        */
    }
    public void SetKeycode()
    {
        tmp = new StringBuilder(keyCode);
        tmp.Replace("Skill_", "");
        SkillKeyText.text = tmp.ToString();
       
    }
    public void SetKey(Skill_Base skill, SkillSlot slot)
    {
        if (SkillObj != null)
        {
            RemoveSKill();
        }
        ConnectedSkillSlot = slot;
        Skill = skill;

        SkillObj = Instantiate(skill.gameObject);

        SkillObj.GetComponent<Skill_Base>().keycode = keyCode;
        SkillObj.GetComponent<Skill_Base>().ConnectedHotKey = this;
        SkillImage.sprite = Resources.Load<Sprite>(skill.SKillImgPath);


    }
    public void RemoveSKill()
    {
        ConnectedSkillSlot.ConnectedHotKey = null;
        SkillObj.GetComponent<Skill_Base>().keycode = null;
        SkillObj.GetComponent<Skill_Base>().ConnectedHotKey = null;
        Destroy(SkillObj);
        Skill = null;
        SkillObj = null;
        SkillImage.sprite = UiManager.uimanager.SlotBasicSprite;
    }
    public void KeyCodeChange() { }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButton(1))
        {
            RemoveSKill();
        }
    }

    //스킬 그대로하고... 스크립트온오프만 해도될듯?
}
