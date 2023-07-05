using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    
   

   
    [Header("�⺻ ���� ��������Ʈ"), SerializeField]
    public Sprite SlotBasicSprite;
    [Header("�⺻ ����  ������������Ʈ"), SerializeField]
    public Sprite SlotBasicCircleSprite;
    [Header("����"), SerializeField]
    GameObject Shop;
    [Header("�κ��丮"), SerializeField]
    public GameObject Inventory;
    [Header("���"), SerializeField]
    public GameObject Equip;
    [Header("��ųƮ��"), SerializeField]
    public GameObject SkillTree;
    [Header("���� ����"), SerializeField]
    public HotKey PortionSlot;
    [SerializeField]
    public GameObject saveCheck;
    List<Slot> Allslots;
    public List<RectTransform> AllSlotsRect;
    public static UiManager uimanager;

   
  
    public enum ItemType
    {
        None,
        Equip,
        Etc
    }
    public ItemType itemtype;
    public enum ItemDetailType
    {
        Armor,
        Weapon,
        Hat,
        Boots,
        Acc,
        Use
    }
  



    private void Awake()
    {
        if (uimanager == null)
        {
            uimanager = this;
      
        }
        else
        {
            if (uimanager != this) 
                Destroy(this.gameObject); 
        }
    }
    void Start()
    {
        InitSetting(); 
    }

    public void ExitShop()
    {
        Shop.SetActive(false);
        GameManager.Instance.IsUi = false;
    }
    public void OpenInventory()
    {
        PlayerManager.Instance.ReloadCoin();
        Inventory.GetComponent<RectTransform>().anchoredPosition = new Vector2(-386.8f, 0);
        Inventory.SetActive(true);
        Inventory.transform.SetAsLastSibling();
        GameManager.Instance.IsUi = true;
    }
    public void ExitInventory()
    {
        Inventory.SetActive(false);
        
        GameManager.Instance.IsUi = false;
    }
    public void ExiEquip()
    {
        Equip.SetActive(false);
        GameManager.Instance.IsUi = false;
    }
    public void OpenSKill()
    {
        SkillTree.GetComponent<RectTransform>().anchoredPosition = new Vector2(17.85f, 0);
        SkillTree.SetActive(true);
        SkillTree.transform.SetAsLastSibling();

        GameManager.Instance.IsUi = true;
        PlayerManager.Instance.NotifySkillSlot();
    }
    public void ExitSKill()
    {
        SkillTree.SetActive(false);
        GameManager.Instance.IsUi = false;
    }
    public void OepnEquip()
    {
        Equip.GetComponent<RectTransform>().anchoredPosition = new Vector2(333.61f,0);
        Equip.SetActive(true);
        Equip.transform.SetAsLastSibling();
        Status.Instance.RefreshStats();
        GameManager.Instance.IsUi = true;
    }
    void InitSetting()
    {
        saveCheck.SetActive(false);
        Shop.SetActive(false);
        Inventory.SetActive(false);
        Equip.SetActive(false);
        SkillTree.SetActive(false);
     
    }
    public void Save()
    {
        DataManager.Instance.JsonSave();
    }
  
}
