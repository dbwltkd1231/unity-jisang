using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SkillSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler,IObserver
{
    
    
    public Skill_Base SkillObj;
    
    [Header("요구 레벨 텍스트"), SerializeField]
    TextMeshProUGUI RqLvText;
    [Header("스킬 슬롯 아이콘"),SerializeField]
    Image SlotImg;
    [Header("ID"), SerializeField]
    public int ID;

    public HotKey ConnectedHotKey;


    bool SkillActive;
    int PlayerLv;
    GameObject SlotPrefab;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SkillObj!=null)
        {
            ShowInfo.showinfo.Active(SkillObj, this.transform);
        }
      
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ShowInfo.showinfo.NoActive();
    }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(SkillObj!=null&& SkillActive==true)
        {
            SlotPrefab = Instantiate(SkillTree.skilltree.MousePrefab);
            SlotPrefab.GetComponent<Image>().sprite = SkillObj.GetComponent<Skill_Base>().SkillImg;
            SlotPrefab.transform.SetParent(SkillTree.skilltree.canvas.transform);
            //Debug.Log(SlotPrefab.transform.localScale);
            SlotPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        


    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (SkillObj != null && SkillActive == true)
        {
            SlotPrefab.transform.position = eventData.position;
        }
           
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (SkillObj != null && SkillActive == true)
        {
            float min = (Vector2.Distance(Camera.main.ScreenToViewportPoint(SkillTree.skilltree.SkiiHotKeys[0].position), Camera.main.ScreenToViewportPoint(Input.mousePosition)));
            int index = 0;
            for (int i = 1; i < SkillTree.skilltree.SkiiHotKeys.Count; i++)
            {
                if (Vector2.Distance(Camera.main.ScreenToViewportPoint(SkillTree.skilltree.SkiiHotKeys[i].position), Camera.main.ScreenToViewportPoint(Input.mousePosition)) < min)
                {
                    index = i;
                    min = Vector2.Distance(Camera.main.ScreenToViewportPoint(SkillTree.skilltree.SkiiHotKeys[i].position), Camera.main.ScreenToViewportPoint(Input.mousePosition));

                }
            }
            if (Vector2.Distance(Camera.main.ScreenToViewportPoint(SkillTree.skilltree.SkiiHotKeys[index].position), Camera.main.ScreenToViewportPoint(Input.mousePosition)) < 0.2f)
            {
                if(ConnectedHotKey!=null)
                {
                    ConnectedHotKey.GetComponent<HotKey>().RemoveSKill();
                }
                SkillTree.skilltree.SkiiHotKeys[index].GetComponent<HotKey>().SetKey(SkillObj,this);
                ConnectedHotKey = SkillTree.skilltree.SkiiHotKeys[index].GetComponent<HotKey>();
                

                //스킬장착
            }

            Destroy(SlotPrefab);
        }
    }

    

 
  
    public void UpdateData(int lv)
    {
       
        if (SkillObj)
        {
            PlayerLv = lv;
            Color color;
            color = SlotImg.color;

            if (PlayerLv >= SkillObj.GetComponent<Skill_Base>().ReqLv)
            {
                color.a = 1f;
                SkillActive = true;
            }
            else
            {
                color.a = 0.06f;
                SkillActive = false;
            }

            SlotImg.color = color;
        }
       

    }
    public void UpdateData(bool tmp)
    {
   

    }
    public void GetSkill(Skill_Base skill)
    {
        SkillObj = skill;
        RqLvText.text = "Req Lv." + SkillObj.GetComponent<Skill_Base>().ReqLv.ToString();
        SlotImg.sprite = SkillObj.GetComponent<Skill_Base>().SkillImg;
    }


}


//스킬등록방법 : 드래그 
//스킬획득방법 : 요구레벨
//
//달성시 자동획득 / 구분 :알파값조절+스킬등록활성화

/*
 *  if(SkillActive == false)
            {
                Color color;
                color = Skillimg.color;
                color.a = 0.06f;
            }
            else if(SkillActive == true)
            {
                Color color;
                color = Skillimg.color;
                color.a = 1f;
            }
 * 
 * */