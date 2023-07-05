using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowInfo : MonoBehaviour
{
    [Header("아이템 정보창"), SerializeField]
    GameObject ItemInspector;
    [Header("이름"), SerializeField]
    TextMeshProUGUI name;
    [Header("정보"), SerializeField]
    TextMeshProUGUI info;
    [Header("가격"), SerializeField]
    TextMeshProUGUI cost;

    //RectTransform rect;
    public static ShowInfo showinfo;
   



    private void Awake()
    {
        ItemInspector.SetActive(false);

    }
    void Start()
    {
        if (showinfo == null)
        {
            showinfo = this; //내자신을 instance로 넣어줍니다.
         
        }
        else
        {
            if (showinfo != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }



    }

  public void Active(Skill_Base skill,Transform pos)
    {
        ItemInspector.transform.SetAsLastSibling();
        ItemInspector.SetActive(true);
        ItemInspector.transform.position = pos.position + new Vector3(432, 0, 0);

        name.text = skill.SkillName;
        info.text = skill.SkillDes;
        cost.text = "";
    }

    public void Active(Item item, Transform pos)
    {
        if(item!=null)
        {
            ItemInspector.transform.SetAsLastSibling();

            ItemInspector.SetActive(true);
            ItemInspector.transform.position = pos.position + new Vector3(432, 0, 0);
            if(item.Starpos==0)
            {
                name.text = item.itemName;
            }
            else
            {
                name.text = item.itemName + " (+" + item.Starpos + ")";
            }
           
            info.text = item.itemDes;
            cost.text = item.Cost.ToString();
        }
       

    }
    public void NoActive()
    {
        ItemInspector.SetActive(false);

        name.text = string.Empty;
        info.text = string.Empty;
        cost.text = string.Empty;
    }
}
