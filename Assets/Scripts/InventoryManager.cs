using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    public GameObject goldNum; 
    void Start()
    {
        
        
        goldNum.GetComponent<Text>().text = SaveCtrl.instance.myData.gold.ToString();

        
    }

    
}
