using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    [SerializeField] private GameObject rankContent;
    [SerializeField] private GameObject topRankObject;
    [SerializeField] private GameObject normalRankObject;
    [SerializeField] private GameObject myRankObject;

    public GameObject audioManagerObj;
    private AudioManager audioManager;

    private bool isOnOff;

    
    UIBox[] uIBoxs;
    UIBox uIBox;

    
    void Start()
    {
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        rankContent.SetActive(false);
    }

    public void OnOffRankUI()
    {
        audioManager.ClickBtn();
        isOnOff = !isOnOff;
        rankContent.SetActive(isOnOff);
        if (isOnOff)
        {
            SetContent();
        }
    }

    private void SetContent()
    {
        
        List<UserData> userDatas = SaveCtrl.instance.userDatas;
        Debug.Log("랭킹 수 : " + userDatas.Count);

        uIBoxs = topRankObject.GetComponentsInChildren<UIBox>();
        for (int i = 0; i < uIBoxs.Length; i++)
        {
            if (i < userDatas.Count)
            {
                uIBoxs[i].texts[0].text = userDatas[i].ID;
                uIBoxs[i].texts[1].text = userDatas[i].rank_score + " .";
            }
            else
            {
                uIBoxs[i].gameObject.SetActive(false);
            }
        }

        
        uIBoxs = normalRankObject.GetComponentsInChildren<UIBox>();
        for (int i = 0; i < uIBoxs.Length; i++)
        {
            if (i < userDatas.Count - 3)
            {
                uIBoxs[i].texts[0].text = userDatas[i + 3].rank + ".";
                uIBoxs[i].texts[1].text = userDatas[i + 3].ID;
                uIBoxs[i].texts[2].text = userDatas[i + 3].rank_score + " .";
            }
            else
            {
                uIBoxs[i].gameObject.SetActive(false);
            }
        }

        
        uIBox = myRankObject.GetComponentInChildren<UIBox>();
        uIBox.texts[0].text = SaveCtrl.instance.myData.rank + ".";
        uIBox.texts[1].text = "(.) " + SaveCtrl.instance.myData.ID;
        uIBox.texts[2].text = SaveCtrl.instance.myData.rank_score + " .";
    }
}
