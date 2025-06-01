using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemCode; // ?�이??코드
    public string name; // ?�름
    public string desc;//������ �߰�, ������ ����
    public long gold; // 가�?
}

public class FishingRob : Item
{
    public static string[] robNames = {
        "Odunsu olta kamışı",
        "Karbon olta kamışı",
        "Bronz olta kamışı",
        "Gümüş olta kamışı",
        "Altın olta kamışı", 
    };
    public static string[] robDesc = {
        "Bu, dükkan sahibi amcanın attığı bir tahta olta kamışı.",
        "Karbondan yapılmış bir olta kamışı. Çok sert ama, Hafif ağır",
        "Bronzdan yapılmış bir olta. Çok hafif ama güçlü değil",
        "Gümüşten yapılmış bir olta kamışı. Çok sert ve hafif",
        "Altın Olta size efsanevi balıkları yakalamanızı sağlayabilir",
    };
    public static long[] gold_datas = { 1, 2, 3, 4, 5 };
    public static float[] probalility_datas = { 0.1f, 0.2f, 0.5f, 1f, 2f };
    public static float[] power_datas = { 20f, 50f, 100f, 250f, 500f };
    public static int fishingRobNum = 5; // 게임 ??존재?�는 ?�시?�??개수

    public float probability; // ?�시 ?�률
    public float power; // 강도

    public FishingRob(int _itemCode)
    {
        itemCode = _itemCode;
        name = robNames[itemCode];
        desc = robDesc[itemCode];
        gold = gold_datas[itemCode];
        probability = probalility_datas[itemCode];
        power = power_datas[itemCode];
    }
};

public class Bait : Item
{
    public static string[] baitNames = {
        "Yem ",
        "Solucan",
        "Hamburger",
        "Karides",
        "Kabire",
        "İnci",
        "Biftek",
    };
    public static string[] baitDesc = {
        "Yakındaki süpermarkette satılan bir Macun Yemi.",
        "Bu taze solucan",
        "McDonald's'tan hamburger..",
        "Karides. Ton balığının, Balinanın favorisi",
        "Orta boy balıkların vaz geçilmezi.",
        "İnci Tanesi",
        "Dana Biftek",
    };
    public static long[] gold_datas = { 1, 2, 3, 4, 5, 6, 7 };
    public static float[] probalility_datas = { 0.1f, 0.25f, 0.5f, 0.7f, 1f, 1.5f, 2f };
    public static float[] power_datas = { 5f, 10f, 15f, 20f, 25f, 30f, 50f };
    public static int BaitNum = 7; 

    public float probability; 
    public float power; 

    public Bait(int _itemCode)
    {
        itemCode = _itemCode;
        name = baitNames[itemCode];
        desc = baitDesc[itemCode];
        gold = gold_datas[itemCode];
        probability = probalility_datas[itemCode];
        power = power_datas[itemCode];
    }
};
