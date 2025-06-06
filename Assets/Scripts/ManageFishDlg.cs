using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class ManageFishDlg : MonoBehaviour
{

    private int fishGold ;
    private GameObject clickedFish;
    private int clickedFishIdx;
    private GameObject fishUI;
    public GameObject goldUI;
    public GameObject FishUIContainer;
    public int fishNumToSell;
    public Slider slider;

    private GameObject[] normalFishUIs;
    private GameObject[] sharkUIs;
    public GameObject audioManagerObj;
    public Text fishNumText;
    AudioManager audioManager;

    private int minValue =1;
    private int maxValue =1;
      

    
    // Start is called before the first frame update
    void Awake()
    {
        normalFishUIs = Resources.LoadAll<GameObject>("Prefabs/Fishes/NormalFish/UI");
        sharkUIs = Resources.LoadAll<GameObject>("Prefabs/Fishes/Shark/UI");
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        gameObject.SetActive(false);
        slider.minValue = 1;
    }


    public void SetItemInfo(int itemType,int itemCode)
    {
        
        if(fishUI!=null){
            Destroy(fishUI);
        }

        Debug.Log(itemType);
        Debug.Log(itemCode);
        
        string name = "", info = "", prob = "", power = "", goldStr="";

        Fish fish = Fish.GetFish(itemType,itemCode);
        switch(itemType){
            case 0:
                name = NormalFish.names[itemCode];
                prob = "Prob : " + NormalFish.probalilities[itemCode].ToString();
                power = "Power : " + NormalFish.powers[itemCode].ToString();
                info = NormalFish.infos[itemCode];
                goldStr= NormalFish.golds[itemCode].ToString();
                fishGold = NormalFish.golds[itemCode];
                break;
            case 1:
                name = Shark.names[itemCode];
                prob = "Prob : " + Shark.probalilities[itemCode].ToString();
                power = "Power : " + Shark.powers[itemCode].ToString();
                info = Shark.infos[itemCode];
                goldStr = Shark.golds[itemCode].ToString();
                fishGold =  Shark.golds[itemCode];
                break;
        }
    

        transform.Find("FishName").GetComponent<Text>().text = name;
        transform.Find("Probability").GetComponent<Text>().text = prob;
        transform.Find("Power").GetComponent<Text>().text = power;
        transform.Find("Description").GetComponent<Text>().text = info;
        transform.Find("GoldContainer").Find("GoldNum").GetComponent<Text>().text = goldStr;

        
        if(itemType ==0){
            fishUI=Instantiate(normalFishUIs[itemCode]);
            fishUI.transform.SetParent(FishUIContainer.transform);
            fishUI.transform.localPosition = Vector3.zero;
        }else{
            fishUI = Instantiate(sharkUIs[itemCode]);
            fishUI.transform.SetParent(FishUIContainer.transform);
            fishUI.transform.localPosition = new Vector3(4,0,0);
        }
        

      
    }    
    
    public void OpenDlg(){
        audioManager.ClickBtn();
       

        
        gameObject.SetActive(true);
    }
   
    public void CloseDlg(){
        gameObject.SetActive(false);
        Destroy(fishUI);
    }

    public void SellFish(){
         
        SaveCtrl.instance.myData.gold += (long)(fishGold*slider.value);
        goldUI.GetComponent<Text>().text = SaveCtrl.instance.myData.gold.ToString();

         
        
        SaveCtrl.instance.myData.fishNums[clickedFishIdx]-=(int)slider.value;
        if(SaveCtrl.instance.myData.fishNums[clickedFishIdx]<=0)
            clickedFish.GetComponent<MoveFish>().RemoveFish();

        
        SaveCtrl.instance.SaveData();
        audioManager.Coin();
        CloseDlg();
    }

    public void SetFishNumToSell(){
        
        fishNumToSell =(int)slider.value;
        fishNumText.text= fishNumToSell.ToString()+"/"+maxValue;
    }

    public void GetClickedFish(GameObject obj){
      
        clickedFish = obj;
        clickedFishIdx =  clickedFish.GetComponent<MoveFish>().GetItemIndexFromName();

        maxValue= SaveCtrl.instance.myData.fishNums[clickedFishIdx];
        slider.maxValue=maxValue;
        slider.minValue=1;
        slider.value=1;
        fishNumText.text = minValue.ToString()+"/"+maxValue.ToString();
        
    }

}
