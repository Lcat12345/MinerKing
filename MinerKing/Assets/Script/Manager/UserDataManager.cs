using System;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    /*
     * 유저가 게임을 껐다 켰다 할 때 남는 정보들 입니다.
     * 돈, 마지막으로 쓰고 있던 곡괭이, 보석 해금 정보, 곡괭이 해금 정보, 스테이지와 맵 해금 정보, 가지고 있는 보석 정보, 스탯 정보 등이 save & load 되어야 합니다.
     */
    // save & load가 필요한 데이터 시작 =======================================================
    private ulong money;
    public ulong Money
    {
        get { return money; }
        set { money = value; }
    }

    private Dictionary<Jewels, ulong> jewelInventory;
    private Dictionary<Pickaxes, bool> pickAxesUnlockInfo;

    private Pickaxes lastUsedPickAxe;

    public Pickaxes LastUsedPickAxe
    {
        get { return lastUsedPickAxe; }
        set { lastUsedPickAxe = value; }
    }

    private uint statMining;
    public uint StatMining
    {
        get { return statMining; }
        set { statMining = value; }
    }

    private uint statMoving;
    public uint StatMoving
    {
        get { return statMoving; }
        set { statMoving = value; }
    }

    public Dictionary<Jewels, ulong> JewlyInfo
    {
        get { return jewelInventory; }
        set { jewelInventory = value; }
    }

    //save & load가 필요한 데이터 끝 ========================================================================== 

    private GameObject player;
    private PlayerController pc;
    public bool IsUnlockedPickAxe(string name)
    {
        if (Enum.TryParse(name, out Pickaxes pickaxe))
        {
            return pickAxesUnlockInfo.TryGetValue(pickaxe, out bool isUnlocked) && isUnlocked;
        }

        Debug.LogWarning($"'{name}'은(는) 올바른 Pickaxes 값이 아닙니다. false가 반환됩니다.");
        return false;
    }

    public void UnlockPickaxe(Pickaxes pickaxe)
    {
        pickAxesUnlockInfo[pickaxe] = true;
    }

    void Awake()
    {
        money = ulong.MaxValue;
        statMining = 0;
        statMoving = 0;

        jewelInventory = new Dictionary<Jewels, ulong>();
        jewelInventory.Add(Jewels.Ruby, 0);
        jewelInventory.Add(Jewels.Sapphire, 0);
        jewelInventory.Add(Jewels.Emerald, 0);
        jewelInventory.Add(Jewels.Topaz, 0);
        jewelInventory.Add(Jewels.Amethyst, 0);
        jewelInventory.Add(Jewels.Garnet, 0);
        jewelInventory.Add(Jewels.Opal, 0);
        jewelInventory.Add(Jewels.Turquoise, 0);
        jewelInventory.Add(Jewels.Peridot, 0);
        jewelInventory.Add(Jewels.Tanzanite, 0);
        jewelInventory.Add(Jewels.Spinel, 0);
        jewelInventory.Add(Jewels.Alexandrite, 0);
        jewelInventory.Add(Jewels.Aquamarine, 0);
        jewelInventory.Add(Jewels.Morganite, 0);
        jewelInventory.Add(Jewels.Rhodolite, 0);
        jewelInventory.Add(Jewels.Tsavorite, 0);
        jewelInventory.Add(Jewels.Jadeite, 0);
        jewelInventory.Add(Jewels.Labradorite, 0);
        jewelInventory.Add(Jewels.Moonstone, 0);
        jewelInventory.Add(Jewels.Bloodstone, 0);
        jewelInventory.Add(Jewels.Diamond, 0);
        jewelInventory.Add(Jewels.LapisLazuli, 0);
        jewelInventory.Add(Jewels.Onyx, 0);
        jewelInventory.Add(Jewels.Moldavite, 0);
        jewelInventory.Add(Jewels.Iolite, 0);
        jewelInventory.Add(Jewels.Citrine, 0);
        jewelInventory.Add(Jewels.Ametrine, 0);
        jewelInventory.Add(Jewels.Coral, 0);
        jewelInventory.Add(Jewels.Amber, 0);
        jewelInventory.Add(Jewels.Chrysoberyl, 0);
        jewelInventory.Add(Jewels.Carnelian, 0);
        jewelInventory.Add(Jewels.Agate, 0);
        jewelInventory.Add(Jewels.Kyanite, 0);
        jewelInventory.Add(Jewels.Andesine, 0);
        jewelInventory.Add(Jewels.Hematite, 0);
        jewelInventory.Add(Jewels.Sugilite, 0);
        jewelInventory.Add(Jewels.Malachite, 0);
        jewelInventory.Add(Jewels.Charoite, 0);
        jewelInventory.Add(Jewels.ZebraJasper, 0);
        jewelInventory.Add(Jewels.PinkTourmaline, 0);
        jewelInventory.Add(Jewels.BlueTourmaline, 0);
        jewelInventory.Add(Jewels.BlueJasper, 0);
        jewelInventory.Add(Jewels.Unakite, 0);
        jewelInventory.Add(Jewels.TigerEye, 0);
        jewelInventory.Add(Jewels.Howlite, 0);
        jewelInventory.Add(Jewels.Rhodochrosite, 0);
        jewelInventory.Add(Jewels.Azurite, 0);
        jewelInventory.Add(Jewels.Fluorite, 0);
        jewelInventory.Add(Jewels.Scapolite, 0);
        jewelInventory.Add(Jewels.PhoenixTear, 0);
        jewelInventory.Add(Jewels.DragonStone, 0);
        jewelInventory.Add(Jewels.MoonlightGem, 0);
        jewelInventory.Add(Jewels.ManaCrystal, 0);
        jewelInventory.Add(Jewels.InfinityStone, 0);
        jewelInventory.Add(Jewels.Orichalcum, 0);
        jewelInventory.Add(Jewels.HeartOfSolaris, 0);

        pickAxesUnlockInfo = new Dictionary<Pickaxes, bool>();
        pickAxesUnlockInfo.Add(Pickaxes.HonedPickaxe, true);
        pickAxesUnlockInfo.Add(Pickaxes.SteelPickaxe, false);
        pickAxesUnlockInfo.Add(Pickaxes.StoneSplitter, false);
        pickAxesUnlockInfo.Add(Pickaxes.FangOfTheEarth, false);
        pickAxesUnlockInfo.Add(Pickaxes.MagmaCleaver, false);
        pickAxesUnlockInfo.Add(Pickaxes.DiamondPickaxe, false);
        pickAxesUnlockInfo.Add(Pickaxes.WaterjetPickaxe, false);
        pickAxesUnlockInfo.Add(Pickaxes.WhisperOfJewels, false);
        pickAxesUnlockInfo.Add(Pickaxes.MithrilBreaker, false);
        pickAxesUnlockInfo.Add(Pickaxes.StarshardPickaxe, false);
        pickAxesUnlockInfo.Add(Pickaxes.ForceOfNature, false);
        pickAxesUnlockInfo.Add(Pickaxes.PickaxeOfMana, false);
        pickAxesUnlockInfo.Add(Pickaxes.VisionCleaver, false);
        pickAxesUnlockInfo.Add(Pickaxes.FantasticPickaxe, false);

        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        pc.curPickaxe = lastUsedPickAxe;
    }

    private void Start()
    {
        // 올바른 무기 이미지로 바꾸기
        Animator weapon = player.transform.Find("Weapon").gameObject.GetComponent<Animator>();
        string path = "Animator/Weapon/" + pc.curPickaxe.ToString();
        weapon.runtimeAnimatorController = ResourceManager.instance.GetResource<RuntimeAnimatorController>(path);
    }

    public void AddJewelCnt(Jewels jewelKey, int jewelCnt)
    {
        jewelInventory[jewelKey] += (ulong)jewelCnt;
    }

    public string FormatNumber(ulong value)
    {
        string[] suffixes = { "", "K", "M", "G", "T", "P", "E", "Z", "Y" }; // 10^3 단위 증가
        int suffixIndex = 0;
        double doubleValue = value;

        while (doubleValue >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            doubleValue /= 1000;
            suffixIndex++;
        }

        return $"{doubleValue:0.#}{suffixes[suffixIndex]}";
    }
}
