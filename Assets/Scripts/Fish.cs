using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    public static int typeNum = 2;
    public static int totalNum = 10;
    public static int[] fishNumAsType = { 5, 5 };

    public int itemCode;
    public int hp; 
    public float power; 
    public float probability; 
    public int gold; 
    public int quality; 
    public float weight; 
    public float width; 
    public float height; 
    public string name;
    public string info;
    public float collection_powerup;
    public float collection_percentup;

   
    static public Fish GetFish(int _itemType, int _itemCode)
    {
        Fish fish = null;

        switch (_itemType)
        {
            case 0: fish = SaveCtrl.instance.normalFish[_itemCode]; break;
            case 1: fish = SaveCtrl.instance.sharks[_itemCode]; break;
        }

        return fish;
    }

    
    static public int GetItemType(Fish fish)
    {
        int type = -1;

        if (fish is NormalFish) type = 0;
        else if (fish is Shark) type = 1;

        return type;
    }

    
    static public int GetFishIndex(int _itemType, int _itemCode)
    {
        int index = 0;
        for (int i = 0; i < _itemType; i++)
            index += fishNumAsType[i];
        index += _itemCode;

        return index;
    }

    
    static public int GetFishIndex(Fish fish)
    {
        int _itemType = GetItemType(fish);
        int _itemCode = fish.itemCode;

        return GetFishIndex(_itemType, _itemCode);
    }

    
    static public Fish GetFish(int fishIndex)
    {
        Fish fish;
        int itemCode;
        int itemType = GetItemType(fishIndex, out itemCode);
        fish = GetFish(itemType, itemCode);

        return fish;
    }

    
    static public int GetItemType(int fishIndex, out int fishCode)
    {
        int totalNum = fishIndex;
        int itemType = 0;
        
        while(totalNum >= fishNumAsType[itemType])
            totalNum -= fishNumAsType[itemType++];
        fishCode = totalNum;

        return itemType;
    }
};



public class Shark : Fish
{
    public static int[] hps = { 300, 1500, 5000, 10000, 20000 };
    public static float[] powers = { 15f, 30f, 100f, 250f, 500f };
    public static float[] probalilities = { 1f, 0.5f, 0.3f, 0.1f, 0f };
    public static int[] golds = { 8, 10, 12, 14, 16 };
    public static int[] qualities = { 2, 2, 3, 3, 4 };
    public static float[] weights = { 0f, 0f, 0f, 0f, 0f };
    public static float[] widths = { 0f, 0f, 0f, 0f, 0f };
    public static float[] heights = { 0f, 0f, 0f, 0f, 0f };
    public static string[] names = { "Mavi köpekbalığı", "Kırmızı köpek balığı", "Büyük Beyaz Köpekbalığı", "Kara köpekbalığı", "Altın köpekbalığı" };
    public static string[] infos =
    {
        "Mavi ışık saçan bir köpek balığı",
        "Kırmızı ışık saçan bir köpek balığıdır.",
        "Parıldayan bir köpek balığı.",
        "Siyah renkte bir köpek balığıdır.",
        "Altın rengi parıltılı bir köpek balığıdır.."
    };
    public static int totalNum = 5;

    public static float[] collection_powerups = {10f, 20f, 40f, 80f, 200f};
    public static float[] collection_percentups = {0.1f, 0.2f, 0.4f, 1f, 2f};

    public Shark(int _itemCode)
    {
        itemCode = _itemCode;
        hp = hps[itemCode];
        power = powers[itemCode];
        probability = probalilities[itemCode];
        gold = golds[itemCode];
        quality = qualities[itemCode];
        weight = weights[itemCode];
        width = widths[itemCode];
        height = heights[itemCode];
        name = names[itemCode];
        info = infos[itemCode];
        collection_powerup = collection_powerups[itemCode];
        collection_percentup = collection_percentups[itemCode];
    }
};

public class NormalFish : Fish
{
    public static int[] hps = { 300, 1000, 3500, 7000, 17000 };
    public static float[] powers = { 10f, 20f, 50f, 150f, 400f };
    public static float[] probalilities = { 1f, 0.5f, 0.3f, 0.1f, 0.01f };
    public static int[] golds = { 1, 2, 3, 4, 5 };
    public static int[] qualities = { 0, 0, 0, 1, 2 };
    public static float[] weights = { 0f, 0f, 0f, 0f, 0f };
    public static float[] widths = { 0f, 0f, 0f, 0f, 0f };
    public static float[] heights = { 0f, 0f, 0f, 0f, 0f };
    public static string[] names = { "Sazan", "Hamsi", "Kaya çupra", "Taş Çupra", "Cavari" };
    public static string[] infos =
    {
        "Perciformes takımının Perciformes familyasına ait bir balık türüdür. Yaz mevsiminin en lezzetli yemeğidir..",
        "Balık takımının Tourvalidae familyasına ait bir balık türüdür. Büyük bir balıktır.",
        "Siyah kaya balığı ailesine ait bir balık türüdür. Yemek ve balıkçılık için popülerdir..",
        "Levrek takımından çipura familyasına ait bir balık türüdür. Baharatlı güveç olarak en iyisidir..",
        "Perciformes takımında Barracuda familyasına ait bir balık türüdür. Yüksek kaliteli bir gıda maddesi olarak popülerdir."
    };
    public static int totalNum = 5;

    public static float[] collection_powerups = {5f, 10f, 20f, 40f, 80f};
    public static float[] collection_percentups = {0.05f, 0.1f, 0.2f, 0.4f, 1f};

    public NormalFish(int _itemCode)
    {
        itemCode = _itemCode;
        hp = hps[itemCode];
        power = powers[itemCode];
        probability = probalilities[itemCode];
        gold = golds[itemCode];
        quality = qualities[itemCode];
        weight = weights[itemCode];
        width = widths[itemCode];
        height = heights[itemCode];
        name = names[itemCode];
        info = infos[itemCode];
        collection_powerup = collection_powerups[itemCode];
        collection_percentup = collection_percentups[itemCode];
    }
};