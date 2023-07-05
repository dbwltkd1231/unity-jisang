using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowInfo : MonoBehaviour
{
    [Header("������ ����â"), SerializeField]
    GameObject ItemInspector;
    [Header("�̸�"), SerializeField]
    TextMeshProUGUI name;
    [Header("����"), SerializeField]
    TextMeshProUGUI info;
    [Header("����"), SerializeField]
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
            showinfo = this; //���ڽ��� instance�� �־��ݴϴ�.
         
        }
        else
        {
            if (showinfo != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
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
