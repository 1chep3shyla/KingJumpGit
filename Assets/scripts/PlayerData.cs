using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class PlayerData
{
    public int maxCountData;
    public int moneyData;
    public int mmrData;
    public bool[] whichOnData;
    public bool[] whichBuyData;
    public string[] allAnimSkinData;
    public bool[] getRewardData;
    public PlayerData(GameLogic player, SkinManager skinsData)
    {
        whichOnData = new bool[skinsData.whichOn.Length];
        whichBuyData = new bool[skinsData.whichBuy.Length];
        allAnimSkinData = new string[skinsData.allAnimSkin.Length];
        getRewardData = new bool[player.butRewardGet.Length];
        maxCountData = player.countLevelMax;
        moneyData = player.money;
        mmrData = player.mmr;
        for (int WOD = 0; WOD < whichBuyData.Length; WOD++)
        {
            whichBuyData[WOD] = skinsData.whichBuy[WOD];
        }
        for (int WOC = 0; WOC < whichOnData.Length; WOC++)
        {
            whichOnData[WOC] = skinsData.whichOn[WOC];
        }
        for (int anim = 0; anim < whichBuyData.Length; anim++)
        {
            allAnimSkinData[anim] = skinsData.allAnimSkin[anim];
        }
        for (int reward = 0; reward < player.butRewardGet.Length; reward++)
        {
            getRewardData[reward] = player.butRewardGet[reward];
        }
    }
}