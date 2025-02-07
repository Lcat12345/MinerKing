using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    /*
     * 유저가 게임을 껐다 켰다 할 때 남는 정보들 입니다.
     * 돈, 마지막으로 쓰고 있던 곡괭이, 보석 해금 정보, 곡괭이 해금 정보, 스테이지와 맵 해금 정보, 가지고 있는 보석 정보, 스탯 정보 등이 save & load 되어야 합니다.
     */
    // save & load가 필요한 데이터 시작 =======================================================

    [System.Serializable]
    public class JewelEntry
    {
        public Jewels key;
        public ulong value;
    }

    [System.Serializable]
    public class PickaxeEntry
    {
        public Pickaxes key;
        public bool value;
    }

    [System.Serializable]
    public class BinaryDataBundle
    {
        public List<JewelEntry> jewelInventory;
        public List<PickaxeEntry> pickAxesUnlockInfo;
        public ulong money;
        public Pickaxes lastUsedPickAxe;
        public uint statMining;
        public uint statMoving;

        public BinaryDataBundle()
        {
            jewelInventory = new List<JewelEntry>();
            pickAxesUnlockInfo = new List<PickaxeEntry>();
        }
    }

    private Dictionary<Jewels, ulong> jewelInventory;
    private Dictionary<Pickaxes, bool> pickAxesUnlockInfo;


    public ulong Money
    {
        get { return binaryDataBundle.money; }
        set { binaryDataBundle.money = value; }
    }

    public Pickaxes LastUsedPickAxe
    {
        get { return binaryDataBundle.lastUsedPickAxe; }
        set { binaryDataBundle.lastUsedPickAxe = value; }
    }
    public uint StatMining
    {
        get { return binaryDataBundle.statMining; }
        set { binaryDataBundle.statMining = value; }
    }

    public uint StatMoving
    {
        get { return binaryDataBundle.statMoving; }
        set { binaryDataBundle.statMoving = value; }
    }

    private BinaryDataBundle binaryDataBundle;

    //save & load가 필요한 데이터 끝 ========================================================================== 

    public float autoSavePeriod = 60.0f;
    private float accTimeForAutoSave;

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
        binaryDataBundle = new BinaryDataBundle();
        jewelInventory = new Dictionary<Jewels, ulong>();
        pickAxesUnlockInfo = new Dictionary<Pickaxes, bool>();

        Load();

        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        pc.curPickaxe = binaryDataBundle.lastUsedPickAxe;
    }

    private void InitAllUserData()
    {
        binaryDataBundle.money = ulong.MaxValue;
        binaryDataBundle.statMining = 0;
        binaryDataBundle.statMoving = 0;

        InitJewelInventory();
        InitPickaxeUnlockInfo();
    }

    private void InitJewelInventory()
    {
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
    }

    private void InitPickaxeUnlockInfo()
    {
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
    }

    private void Start()
    {
        // 올바른 무기 이미지로 바꾸기
        Animator weapon = player.transform.Find("Weapon").gameObject.GetComponent<Animator>();
        string path = "Animator/Weapon/" + pc.curPickaxe.ToString();
        weapon.runtimeAnimatorController = ResourceManager.instance.GetResource<RuntimeAnimatorController>(path);

        accTimeForAutoSave = 0.0f;
    }

    private void Update()
    {
        accTimeForAutoSave += Time.deltaTime;

        if (accTimeForAutoSave > autoSavePeriod)
        {
            Save();
            Debug.Log("Auto Save Completed");

            accTimeForAutoSave = 0.0f;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        String saveFileName = "UserData.bin";
        String savePath = Application.persistentDataPath + "/" + saveFileName;

        binaryDataBundle.lastUsedPickAxe = pc.curPickaxe;

        binaryDataBundle.jewelInventory = jewelInventory
            .Select(kv => new JewelEntry { key = kv.Key, value = kv.Value }).ToList();

        binaryDataBundle.pickAxesUnlockInfo = pickAxesUnlockInfo
            .Select(kv => new PickaxeEntry { key = kv.Key, value = kv.Value }).ToList();

        String json = JsonUtility.ToJson(binaryDataBundle);

        BinaryFormatter binaryFormatter = new BinaryFormatter();


        FileStream fileStream = new FileStream(savePath + ".tmp", FileMode.Create);

        binaryFormatter.Serialize(fileStream, json);
        fileStream.Close();

        if (File.Exists(savePath))
        {
            File.Replace(savePath + ".tmp", savePath, savePath + ".bak");
        }
        else
        {
            File.Move(savePath + ".tmp", savePath);
        }

        Debug.Log("Game saved successfully.");
    }

    public void Load()
    {
        String loadFileName = "UserData.bin";
        String loadPath = Application.persistentDataPath + "/" + loadFileName;

        if (!File.Exists(loadPath))
        {
            InitAllUserData();
            Debug.Log("initalized user data as no save file has been found.");
            return;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(loadPath, FileMode.Open);

        String json = binaryFormatter.Deserialize(fileStream) as String;

        fileStream.Close();

        binaryDataBundle = JsonUtility.FromJson<BinaryDataBundle>(json);

        jewelInventory = binaryDataBundle.jewelInventory
            .ToDictionary(entry => entry.key, entry => entry.value);
        pickAxesUnlockInfo = binaryDataBundle.pickAxesUnlockInfo
            .ToDictionary(entry => entry.key, entry => entry.value);

        Debug.Log("loaded save data.");
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
