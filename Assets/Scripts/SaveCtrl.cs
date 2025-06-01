using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;
using System.Text;


public class UserData
{
    
    public string public_inDate, private_inDate;
   
    public string ID;
    
    public long gold;
    
    public int[] fishNums;
    
    public int[] fishBaits;
    
    public bool[] hasFishingRod;
   
    public int equipBaits;

    public int equipFishingRod;

    public bool[] fish_collections;
    
    public int rank_score;
    
    public int rank;

    public UserData()
    {
        fishNums = new int[Fish.totalNum];
        fishBaits = new int[Bait.BaitNum];
        hasFishingRod = new bool[FishingRob.fishingRobNum];
        fish_collections = new bool[Fish.totalNum];
        hasFishingRod[0] = true;
    }

    
    public int GetRankScore()
    {
        int score = 0;
        Fish fish;

        for (int i = 0; i < fish_collections.Length; i++)
        {
            if (fish_collections[i]) 
            {
                fish = Fish.GetFish(i);
                score += SaveCtrl.instance.scoreAsQuality[fish.quality];
            }
        }

        return score;
    }
};

public class SaveCtrl : MonoBehaviour
{
    public static SaveCtrl instance = null;
    private readonly string rank_uuid = "3d41f510-d995-11ec-a5ca-df7f97b8d87a";
    public readonly int[] scoreAsQuality = { 1, 10, 100, 1000, 10000 };
    public readonly int maxRankNum = 7; 

    public UserData myData; 
    public List<UserData> userDatas = new List<UserData>();
    public List<FishingRob> fishingRobs = new List<FishingRob>();
    public List<Bait> baits = new List<Bait>();
    public List<Shark> sharks = new List<Shark>();
    public List<NormalFish> normalFish = new List<NormalFish>();

  
    public static SaveCtrl Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            myData = new UserData();
            DontDestroyOnLoad(this.gameObject);

            var bro = Backend.Initialize(true);
            if (bro.IsSuccess())
            {
                Login();
                Debug.Log(Backend.Utils.GetGoogleHash());
            }
            else
            {
                Debug.Log("Yüklenme hatası. <error> : " + bro.GetErrorCode());
            }

            for (int i = 0; i < FishingRob.fishingRobNum; i++)
                fishingRobs.Add(new FishingRob(i));
            for (int i = 0; i < Bait.BaitNum; i++)
                baits.Add(new Bait(i));
            for (int i = 0; i < Shark.totalNum; i++)
                sharks.Add(new Shark(i));
            for (int i = 0; i < NormalFish.totalNum; i++)
                normalFish.Add(new NormalFish(i));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
        Application.Quit();
    }

    
    public void Login()
    {
        if(Backend.BMember.GetGuestID().Equals(""))
        {
            
            Debug.Log("Başla.");
            BackendReturnObject bro = Backend.BMember.GuestLogin();
            if (bro.IsSuccess())
            {
                InsertData();
            }
            else
            {
                Debug.Log("Hata : " + bro.GetMessage());
            }
        }
        else
        {
            string id = Backend.BMember.GetGuestID();
            BackendReturnObject bro = Backend.BMember.GuestLogin();

            Debug.Log("Kullanıcı Adı :" + id);
            if (bro.IsSuccess())
            {
                LoadData();
            }
            else
            {
                Debug.Log("\nHata. <error> : " + bro.GetMessage());
                if (bro.GetErrorCode().Equals("BadUnauthorizedException"))
                {
                    Debug.Log("nHata.");
                    Backend.BMember.DeleteGuestInfo();
                    Login();
                }
            }
        }
    }


    
    public void SaveData()
    {
        if (!Backend.IsInitialized)
        {
            Debug.LogError("nHatat.");
            return;
        }

        BackendReturnObject BRO = Backend.URank.User.GetMyRank(rank_uuid);
        myData.rank_score = myData.GetRankScore();
        myData.rank = int.Parse(BRO.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString());

        Param param = new Param();
        SettingPublicParam(param);
        BRO = Backend.GameData.Update("userData_public", myData.public_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("nHata = " + BRO.GetMessage());

        param = new Param();
        SettingPrivateParam(param);
        BRO = Backend.GameData.Update("userData_private", myData.private_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("nHata = " + BRO.GetMessage());

        // 랭킹 업데이트
        param = new Param();
        param.Add("rank_score", myData.rank_score);
        BRO = Backend.URank.User.UpdateUserScore(rank_uuid, "userData_private", myData.private_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("nHata. <error> : " + BRO.GetMessage());
    }

    
    public void LoadData()
    {
        
        if (!Backend.IsInitialized)
        {
            Debug.LogError("Hata.");
            return;
        }

        
        BackendReturnObject BRO = Backend.GameData.GetMyData("userData_public", new Where());
        if (BRO.IsSuccess())
        {
            JsonData json = BRO.GetReturnValuetoJSON()["rows"][0];
            PublicDataParsing(json);
        }
        else
        {
            Debug.LogError("Hata = " + BRO.GetMessage());
        }

        
        BRO = Backend.GameData.GetMyData("userData_private", new Where());
        if (BRO.IsSuccess())
        {
            JsonData json = BRO.GetReturnValuetoJSON()["rows"][0];
            PrivateDataParsing(json);
        }
        else
        {
            Debug.LogError("Hata = " + BRO.GetMessage());
        }

        
        SetRankData();
    }

    
    private void SetRankData()
    {
        BackendReturnObject BRO = Backend.URank.User.GetRankList(rank_uuid, maxRankNum, 0);

        for (int i = 0; i < BRO.Rows().Count; i++)
        {
            JsonData jsonData = BRO.GetReturnValuetoJSON()["rows"][i];
            UserData userData = new UserData();

            userData.ID = jsonData["nickname"]["S"].ToString();
            userData.rank_score = int.Parse(jsonData["score"]["N"].ToString());
            userData.rank = int.Parse(jsonData["rank"]["N"].ToString());
            userDatas.Add(userData);
        }
    }

    
    private void InsertData()
    {
        // Nickname Setting (Temp)
        string[] splits = Backend.BMember.GetGuestID().Split('-');
        myData.ID = splits[splits.Length - 1];
        BackendReturnObject BRO = Backend.BMember.CreateNickname(splits[splits.Length - 1]);
        if (!BRO.IsSuccess())
            Debug.LogError("Hata = " + BRO.GetMessage());

        // Public Data Setting
        Param param = new Param();
        SettingPublicParam(param);
        BRO = Backend.GameData.Insert("userData_public", param);
        myData.public_inDate = BRO.GetInDate();
        if(!BRO.IsSuccess())
            Debug.LogError("Hata. <error> : " + BRO.GetMessage());

        // Private Data Setting
        param = new Param();
        SettingPrivateParam(param);
        BRO = Backend.GameData.Insert("userData_private", param);
        myData.private_inDate = BRO.GetInDate();
        if (!BRO.IsSuccess())
            Debug.LogError("Hata. <error> : " + BRO.GetMessage());

        // 랭킹 등록
        param = new Param();
        param.Add("rank_score", myData.rank_score);
        BRO = Backend.URank.User.UpdateUserScore(rank_uuid, "userData_private", myData.private_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("Hata. <error> : " + BRO.GetMessage());

        SaveData();
    }

   
    private void SettingPublicParam(Param param)
    {
        param.Add("public_inDate", myData.public_inDate);
        param.Add("private_inDate", myData.private_inDate);
        param.Add("ID", myData.ID);
        param.Add("gold", myData.gold);
        param.Add("fishNums", myData.fishNums);
        param.Add("fishBaits", myData.fishBaits);
        param.Add("hasFishingRod", myData.hasFishingRod);
        param.Add("equipFishingRod", myData.equipFishingRod);
        param.Add("equipBaits", myData.equipBaits);
        param.Add("fish_collections", myData.fish_collections);
    }

   
    private void SettingPrivateParam(Param param)
    {
        param.Add("rank_score", myData.rank_score);
        param.Add("rank", myData.rank);
    }

    
    private void PublicDataParsing(JsonData json)
    {
        myData.public_inDate = json["public_inDate"][0].ToString();
        myData.private_inDate = json["private_inDate"][0].ToString();
        myData.ID = json["ID"][0].ToString();
        myData.gold = long.Parse(json["gold"][0].ToString());
        for (int i = 0; i < json["fishNums"]["L"].Count; i++)
            myData.fishNums[i] = int.Parse(json["fishNums"]["L"][i][0].ToString());
        for (int i = 0; i < json["fishBaits"]["L"].Count; i++)
            myData.fishBaits[i] = int.Parse(json["fishBaits"]["L"][i][0].ToString());
        for (int i = 0; i < json["hasFishingRod"]["L"].Count; i++)
            myData.hasFishingRod[i] = bool.Parse(json["hasFishingRod"]["L"][i][0].ToString());
        myData.equipFishingRod = int.Parse(json["equipFishingRod"][0].ToString());
        myData.equipBaits = int.Parse(json["equipBaits"][0].ToString());
        for (int i = 0; i < json["fish_collections"]["L"].Count; i++)
            myData.fish_collections[i] = bool.Parse(json["fish_collections"]["L"][i][0].ToString());
    }

    
    private void PrivateDataParsing(JsonData json)
    {
        myData.rank_score = int.Parse(json["rank_score"][0].ToString());
        myData.rank = int.Parse(json["rank"][0].ToString());
    }

    
    public void ResetData(int _gold)
    {
        myData = new UserData();
        myData.gold = _gold;
        SaveData();
    }

    
    public void ResetData()
    {
        ResetData(0);
    }
}
