using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFishes : MonoBehaviour
{
    

    private GameObject[] normalFishPrefabs;
    private GameObject[] sharkPrefabs;

    void Start()
    {
        
        normalFishPrefabs = Resources.LoadAll<GameObject>("Prefabs/Fishes/NormalFish");
        sharkPrefabs = Resources.LoadAll<GameObject>("Prefabs/Fishes/Shark");
         
        int[] fishNum = SaveCtrl.instance.myData.fishNums;
        int len = fishNum.Length;

        
        for(int i=0;i<len;i++){
           
            
            int itemCode;
            int itemType;
            itemType = Fish.GetItemType(i,out itemCode);
            
            
            GameObject fishPrefab ;
            switch(itemType){
                case 0:
                    if(fishNum[i]>0){
                        fishPrefab = Instantiate(normalFishPrefabs[itemCode]);
                        fishPrefab.name = "NormalFish " + i ; 
                    }
                 
                    break;
                case 1:
                    
                    if(fishNum[i]>0) {
                        fishPrefab = Instantiate(sharkPrefabs[itemCode]); 
                        fishPrefab.name = "Shark " + i; 
                    }
                    break;
            }
           
        }
    }


}
