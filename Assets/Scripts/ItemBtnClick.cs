using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemBtnClick : MonoBehaviour
{
    public Button dlgOpenBtn;
    public Button fishingRodBtn;
    public Button baitBtn;
    public GameObject itemInfo;
    public GameObject slotParent;

    public Button onButton;
    public GameObject audioManagerObj;
    AudioManager audioManager;

    private GameObject slotPrefab;
    private GameObject newSelectedSlot, oldSelectedSlot;
    private GameObject equippedSlot;

    public bool isRod;
    private Sprite[] rodSprites;
    private Sprite[] baitSprites;

    
    

    void Awake(){
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        gameObject.SetActive(false);
        
    }
    
    void Start()
    {
        slotPrefab = Resources.Load("Prefabs/Slot") as GameObject;
        
        rodSprites = Resources.LoadAll<Sprite>("FishingRod");
        baitSprites = Resources.LoadAll<Sprite>("Bait");

        if (System.Object.ReferenceEquals(dlgOpenBtn, fishingRodBtn))
        {
            InitRod();
        }
        else
        {
            InitBait();
        }

        

    }

    public void OpenDlg()
    {
        audioManager.ClickBtn();
        gameObject.SetActive(true);
    }
    public void CloseDlg(){
        audioManager.ClickBtn();
        gameObject.SetActive(false);
    }


    private void InitRod()
    {
        
        int curEquippedRodIdx = SaveCtrl.instance.myData.equipFishingRod;
        int rodTotalNum = SaveCtrl.instance.myData.hasFishingRod.Length;
        for (int i = 0; i < rodTotalNum; i++)
        {
            if (SaveCtrl.instance.myData.hasFishingRod[i])
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "RodSlot " + i; 
                slot.transform.SetParent(slotParent.transform);

                
                Vector3 pos = slot.GetComponent<RectTransform>().anchoredPosition3D;
                slot.GetComponent<RectTransform>().localPosition = new Vector3(pos.x,pos.y,1);
                slot.transform.Find("Num").gameObject.SetActive(false);
                slot.transform.GetComponent<Button>().onClick.AddListener(OnSlotClick);
                
                slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite = rodSprites[i];
                slot.GetComponent<RectTransform>().localScale = Vector3.one;

                
                if (oldSelectedSlot == null && curEquippedRodIdx == i)
                {
                    oldSelectedSlot = slot;
                    oldSelectedSlot.transform.Find("Check").gameObject.SetActive(true);
                    SetItemInfo(oldSelectedSlot, curEquippedRodIdx);
                    oldSelectedSlot.GetComponent<Outline>().enabled = true;
                    equippedSlot = slot;
                }
            }
        }

    }

    private void InitBait()
    {
        int[] fishBaits = SaveCtrl.instance.myData.fishBaits;
        int baitTypeNum = fishBaits.Length;

        for (int i = 0; i < baitTypeNum; i++)
        {
        
            if ((i!=0 && fishBaits[i] > 0)|| i ==0)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "BaitSlot " + i; 
                slot.transform.SetParent(slotParent.transform);

                
                Vector3 pos = slot.GetComponent<RectTransform>().anchoredPosition3D;
                slot.GetComponent<RectTransform>().localPosition = new Vector3(pos.x,pos.y,1);
                slot.GetComponent<RectTransform>().localScale = Vector3.one;
                slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite = baitSprites[i];
                slot.transform.GetComponent<Button>().onClick.AddListener(OnSlotClick);
                if( i !=0){
                    slot.transform.Find("Num").GetComponent<TextMeshProUGUI>().text = fishBaits[i].ToString();
                }else{
                    slot.transform.Find("Num").gameObject.SetActive(false);
                }

               
                if (oldSelectedSlot == null)
                {
                    oldSelectedSlot = slot;
                    oldSelectedSlot.transform.Find("Check").gameObject.SetActive(true);
                    SetItemInfo(oldSelectedSlot, SaveCtrl.instance.myData.equipBaits);
                    oldSelectedSlot.GetComponent<Outline>().enabled = true;
                    equippedSlot = slot;
                }
            }
        }
    }

    public void SetItemInfo(GameObject slot, int itemCode)
    {
        bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        string name = "", desc = "", prob = "", power = "";


        if (isRod){
            name = FishingRob.robNames[itemCode];
            prob = "Prob : " + FishingRob.probalility_datas[itemCode].ToString();
            power = "Power : " + FishingRob.power_datas[itemCode].ToString();
            desc = FishingRob.robDesc[itemCode];
            itemInfo.transform.Find("ItemImage").GetComponent<Image>().sprite = rodSprites[itemCode];
        }else{
             name = Bait.baitNames[itemCode];
            prob = "Prob : " + Bait.probalility_datas[itemCode].ToString();
            power = "Power : " + Bait.power_datas[itemCode].ToString();
            desc = Bait.baitDesc[itemCode];
            itemInfo.transform.Find("ItemImage").GetComponent<Image>().sprite = baitSprites[itemCode];
        }

        (itemInfo.transform.Find("ItemName").GetComponent<Text>()).text = name;
        (itemInfo.transform.Find("Probability").GetComponent<Text>()).text = prob;
        (itemInfo.transform.Find("Power").GetComponent<Text>()).text = power;
        (itemInfo.transform.Find("Description").GetComponent<Text>()).text = desc;

    }

    public void OnSlotClick()
    {
        
        audioManager.SelectSlot();
        if (newSelectedSlot != null)
            oldSelectedSlot = newSelectedSlot;
        
        
        newSelectedSlot = EventSystem.current.currentSelectedGameObject;

        if(newSelectedSlot.Equals(oldSelectedSlot)) 
            return;
        
        
        int itemCode = GetItemCodeFromName();
        SetItemInfo(newSelectedSlot,itemCode);
        showOutline();
        
            
    }

    public void showOutline()
    {
        
        newSelectedSlot.GetComponent<Outline>().enabled = true;
        oldSelectedSlot.GetComponent<Outline>().enabled = false;
    }

    
    public void OnUseItemClick()
    {
        
        audioManager.EquipItem();
        if (newSelectedSlot == null || newSelectedSlot.Equals(equippedSlot))
            return;

        int itemCode = GetItemCodeFromName();

        
        equippedSlot.transform.Find("Check").gameObject.SetActive(false);
        newSelectedSlot.transform.Find("Check").gameObject.SetActive(true);
        equippedSlot = newSelectedSlot;

        
        if(isRod){
            SaveCtrl.instance.myData.equipFishingRod = itemCode;
        }else{
            SaveCtrl.instance.myData.equipBaits = itemCode;
        }
        
        SaveCtrl.instance.SaveData();
        
    }

    private int GetItemCodeFromName(){
        string[] split_data = newSelectedSlot.name.Split(' ');
        int itemCode =  int.Parse(split_data[1]);
        return itemCode;
    }

    
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
