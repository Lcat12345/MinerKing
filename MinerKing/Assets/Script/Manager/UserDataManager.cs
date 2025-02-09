using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    /*
     * ������ ������ ���� �״� �� �� ���� ������ �Դϴ�.
     * ��, ���������� ���� �ִ� ���, ���� �ر� ����, ��� �ر� ����, ���������� �� �ر� ����, ������ �ִ� ���� ����, ���� ���� ���� save & load �Ǿ�� �մϴ�.
     */
    // save & load�� �ʿ��� ������ ���� =======================================================

    [System.Serializable]
    public class JewelEntry
    {
        public Jewels key;
        public ulong cnt;
        public bool unlocked;
    }

    [System.Serializable]
    public class PickaxeEntry
    {
        public Pickaxes key;
        public bool value;
    }

    [System.Serializable]
    public class StageEntry
    {
        public int idxMap;
        public int idxStage;
        public bool value;
    }

    [System.Serializable]
    public class BinaryDataBundle
    {
        public List<JewelEntry> jewelInventory;
        public List<PickaxeEntry> pickAxesUnlockInfo;
        public List<StageEntry> stagesUnlockInfo;
        public int lastPlayedMap;
        public int lastPlayedStage;
        public ulong money;
        public Pickaxes lastUsedPickAxe;
        public uint statMining;
        public uint statMoving;

        public BinaryDataBundle()
        {
            jewelInventory = new List<JewelEntry>();
            pickAxesUnlockInfo = new List<PickaxeEntry>();
            stagesUnlockInfo = new List<StageEntry>();
        }
    }

    private Dictionary<Jewels, (ulong, bool)> jewelInventory;
    private Dictionary<Pickaxes, bool> pickAxesUnlockInfo;
    private Dictionary<(int, int), bool> stagesUnlockInfo;
    private (int, int) lastPlayedStage;

    public (int, int) LastPlayedStage
    {
        get { return lastPlayedStage; }
    }

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
    public Dictionary<Jewels, (ulong, bool)> JewlyInfo
    {
        get { return jewelInventory; }
        set { jewelInventory = value; }
    }

    //save & load�� �ʿ��� ������ �� ========================================================================== 

    public float autoSavePeriod = 60.0f;
    private float accTimeForAutoSave;

    private GameObject player;
    private PlayerController pc;

    [SerializeField] CalculatePopup calculatePopup;

    public bool IsUnlockedPickAxe(string name)
    {
        if (Enum.TryParse(name, out Pickaxes pickaxe))
        {
            return pickAxesUnlockInfo.TryGetValue(pickaxe, out bool isUnlocked) && isUnlocked;
        }

        Debug.LogWarning($"'{name}'��(��) �ùٸ� Pickaxes ���� �ƴմϴ�. false�� ��ȯ�˴ϴ�.");
        return false;
    }

    public void UnlockPickaxe(Pickaxes pickaxe)
    {
        pickAxesUnlockInfo[pickaxe] = true;
    }

    public bool IsUnlockedStage(int idxMap, int idxStage)
    {
        bool isUnlocked = false;
        if (!stagesUnlockInfo.TryGetValue((idxMap, idxStage), out isUnlocked))
        {
            Debug.LogWarning("(" + idxMap + ", " + idxStage + ")�� �ùٸ� �������� �ε��� ���� �ƴմϴ�. false�� ��ȯ�˴ϴ�.");
            return false;
        }
        return isUnlocked;
    }

    public void UnlockStage(int idxMap, int idxStage)
    {
        stagesUnlockInfo[(idxMap, idxStage)] = true;
    }

    public void OnChangeMap(int idxMap, int idxStage)
    {
        lastPlayedStage = (idxMap, idxStage);
    }

    void Awake()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        binaryDataBundle = new BinaryDataBundle();
        jewelInventory = new Dictionary<Jewels, (ulong, bool)>();
        pickAxesUnlockInfo = new Dictionary<Pickaxes, bool>();
        stagesUnlockInfo = new Dictionary<(int, int), bool>();

        Load();

        pc.curPickaxe = binaryDataBundle.lastUsedPickAxe;
    }

    private void InitAllUserData()
    {
        binaryDataBundle.money = 0;
        binaryDataBundle.statMining = 0;
        binaryDataBundle.statMoving = 0;
        lastPlayedStage = (0, 0);
        pc.SetStartingStage(0, 0);

        InitJewelInventory();
        InitPickaxeUnlockInfo();
        InitStageUnlockInfo();
    }

    private void InitJewelInventory()
    {
        jewelInventory.Add(Jewels.Ruby, (0, false));
        jewelInventory.Add(Jewels.Sapphire, (0, false));
        jewelInventory.Add(Jewels.Emerald, (0, false));
        jewelInventory.Add(Jewels.Topaz, (0, false));
        jewelInventory.Add(Jewels.Amethyst, (0, false));
        jewelInventory.Add(Jewels.Garnet, (0, false));
        jewelInventory.Add(Jewels.Opal, (0, false));
        jewelInventory.Add(Jewels.Turquoise, (0, false));
        jewelInventory.Add(Jewels.Peridot, (0, false));
        jewelInventory.Add(Jewels.Tanzanite, (0, false));
        jewelInventory.Add(Jewels.Spinel, (0, false));
        jewelInventory.Add(Jewels.Alexandrite, (0, false));
        jewelInventory.Add(Jewels.Aquamarine, (0, false));
        jewelInventory.Add(Jewels.Morganite, (0, false));
        jewelInventory.Add(Jewels.Rhodolite, (0, false));
        jewelInventory.Add(Jewels.Tsavorite, (0, false));
        jewelInventory.Add(Jewels.Jadeite, (0, false));
        jewelInventory.Add(Jewels.Labradorite, (0, false));
        jewelInventory.Add(Jewels.Moonstone, (0, false));
        jewelInventory.Add(Jewels.Bloodstone, (0, false));
        jewelInventory.Add(Jewels.Diamond, (0, false));
        jewelInventory.Add(Jewels.LapisLazuli, (0, false));
        jewelInventory.Add(Jewels.Onyx, (0, false));
        jewelInventory.Add(Jewels.Moldavite, (0, false));
        jewelInventory.Add(Jewels.Iolite, (0, false));
        jewelInventory.Add(Jewels.Citrine, (0, false));
        jewelInventory.Add(Jewels.Ametrine, (0, false));
        jewelInventory.Add(Jewels.Coral, (0, false));
        jewelInventory.Add(Jewels.Amber, (0, false));
        jewelInventory.Add(Jewels.Chrysoberyl, (0, false));
        jewelInventory.Add(Jewels.Carnelian, (0, false));
        jewelInventory.Add(Jewels.Agate, (0, false));
        jewelInventory.Add(Jewels.Kyanite, (0, false));
        jewelInventory.Add(Jewels.Andesine, (0, false));
        jewelInventory.Add(Jewels.Hematite, (0, false));
        jewelInventory.Add(Jewels.Sugilite, (0, false));
        jewelInventory.Add(Jewels.Malachite, (0, false));
        jewelInventory.Add(Jewels.Charoite, (0, false));
        jewelInventory.Add(Jewels.ZebraJasper, (0, false));
        jewelInventory.Add(Jewels.PinkTourmaline, (0, false));
        jewelInventory.Add(Jewels.BlueTourmaline, (0, false));
        jewelInventory.Add(Jewels.BlueJasper, (0, false));
        jewelInventory.Add(Jewels.Unakite, (0, false));
        jewelInventory.Add(Jewels.TigerEye, (0, false));
        jewelInventory.Add(Jewels.Howlite, (0, false));
        jewelInventory.Add(Jewels.Rhodochrosite, (0, false));
        jewelInventory.Add(Jewels.Azurite, (0, false));
        jewelInventory.Add(Jewels.Fluorite, (0, false));
        jewelInventory.Add(Jewels.Scapolite, (0, false));
        jewelInventory.Add(Jewels.PhoenixTear, (0, false));
        jewelInventory.Add(Jewels.DragonStone, (0, false));
        jewelInventory.Add(Jewels.MoonlightGem, (0, false));
        jewelInventory.Add(Jewels.ManaCrystal, (0, false));
        jewelInventory.Add(Jewels.InfinityStone, (0, false));
        jewelInventory.Add(Jewels.Orichalcum, (0, false));
        jewelInventory.Add(Jewels.HeartOfSolaris, (0, false));
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

    private void InitStageUnlockInfo()
    {
        stagesUnlockInfo.Add((0, 0), true);
        stagesUnlockInfo.Add((0, 1), false);
        stagesUnlockInfo.Add((0, 2), false);
        stagesUnlockInfo.Add((0, 3), false);
        stagesUnlockInfo.Add((0, 4), false);
        stagesUnlockInfo.Add((0, 5), false);
        stagesUnlockInfo.Add((1, 0), false);
        stagesUnlockInfo.Add((1, 1), false);
        stagesUnlockInfo.Add((1, 2), false);
        stagesUnlockInfo.Add((1, 3), false);
        stagesUnlockInfo.Add((1, 4), false);
        stagesUnlockInfo.Add((1, 5), false);
        stagesUnlockInfo.Add((2, 0), false);
        stagesUnlockInfo.Add((2, 1), false);
        stagesUnlockInfo.Add((2, 2), false);
        stagesUnlockInfo.Add((2, 3), false);
        stagesUnlockInfo.Add((2, 4), false);
        stagesUnlockInfo.Add((2, 5), false);
        stagesUnlockInfo.Add((2, 6), false);
        stagesUnlockInfo.Add((3, 0), false);
        stagesUnlockInfo.Add((3, 1), false);
        stagesUnlockInfo.Add((3, 2), false);
        stagesUnlockInfo.Add((3, 3), false);
        stagesUnlockInfo.Add((3, 4), false);
        stagesUnlockInfo.Add((3, 5), false);
        stagesUnlockInfo.Add((3, 6), false);
        stagesUnlockInfo.Add((4, 0), false);
        stagesUnlockInfo.Add((4, 1), false);
        stagesUnlockInfo.Add((4, 2), false);
        stagesUnlockInfo.Add((4, 3), false);
        stagesUnlockInfo.Add((4, 4), false);
        stagesUnlockInfo.Add((4, 5), false);
        stagesUnlockInfo.Add((4, 6), false);
        stagesUnlockInfo.Add((4, 7), false);
        stagesUnlockInfo.Add((4, 8), false);
    }

    private void Start()
    {
        // �ùٸ� ���� �̹����� �ٲٱ�
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
            .Select(kv => new JewelEntry { key = kv.Key, cnt = kv.Value.Item1, unlocked = kv.Value.Item2 }).ToList();

        binaryDataBundle.pickAxesUnlockInfo = pickAxesUnlockInfo
            .Select(kv => new PickaxeEntry { key = kv.Key, value = kv.Value }).ToList();

        binaryDataBundle.stagesUnlockInfo = stagesUnlockInfo
            .Select(kv => new StageEntry { idxMap = kv.Key.Item1, idxStage = kv.Key.Item2, value = kv.Value }).ToList();

        binaryDataBundle.lastPlayedMap = lastPlayedStage.Item1;
        binaryDataBundle.lastPlayedStage = lastPlayedStage.Item2;

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
            .ToDictionary(entry => entry.key, entry => (entry.cnt, entry.unlocked));
        pickAxesUnlockInfo = binaryDataBundle.pickAxesUnlockInfo
            .ToDictionary(entry => entry.key, entry => entry.value);
        stagesUnlockInfo = binaryDataBundle.stagesUnlockInfo
            .ToDictionary(entry => (entry.idxMap, entry.idxStage), entry => entry.value);
        lastPlayedStage = (binaryDataBundle.lastPlayedMap, binaryDataBundle.lastPlayedStage);
        pc.SetStartingStage(lastPlayedStage.Item1, lastPlayedStage.Item2);

        Debug.Log("loaded save data.");
    }

    public void AddJewelCnt(Jewels jewelKey, int jewelCnt)
    {
        jewelInventory[jewelKey] = ( jewelInventory[jewelKey].Item1 + (ulong)jewelCnt, true );
    }

    public string FormatNumber(ulong value)
    {
        string[] suffixes = { "", "K", "M", "G", "T", "P", "E", "Z", "Y" }; // 10^3 ���� ����
        int suffixIndex = 0;
        double doubleValue = value;

        while (doubleValue >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            doubleValue /= 1000;
            suffixIndex++;
        }

        return $"{doubleValue:0.###}{suffixes[suffixIndex]}";
    }

    public bool CheckFirst(string name)
    {
        if (Enum.TryParse(name, out Jewels jewel))
        {
            if(jewelInventory[jewel].Item2)
            {
                return false;
            }
            else
            {
                jewelInventory[jewel] = (0, true);
                return true;
            }
        }

        Debug.LogWarning($"'{name}'��(��) �ùٸ� Jewels ���� �ƴմϴ�. false�� ��ȯ�˴ϴ�.");
        return false;
    }

    public void UnlockJewel(string name)
    {
        if (Enum.TryParse(name, out Jewels jewel))
        {
            calculatePopup.initUI(jewel);
        }
    }

    public bool IsUnlockedJewel(string name)
    {
        if (Enum.TryParse(name, out Jewels jewel))
        {
            return jewelInventory[jewel].Item2;
        }

        Debug.LogWarning($"'{name}'��(��) �ùٸ� Jewels ���� �ƴմϴ�. false�� ��ȯ�˴ϴ�.");
        return false;
    }
}
