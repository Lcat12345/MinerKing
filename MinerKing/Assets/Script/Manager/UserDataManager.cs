using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    /*
     * 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쌓댐옙 占쏙옙 占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쌉니댐옙.
     * 占쏙옙, 占쏙옙占쏙옙占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쌍댐옙 占쏘괭占쏙옙, 占쏙옙占쏙옙 占쌔깍옙 占쏙옙占쏙옙, 占쏘괭占쏙옙 占쌔깍옙 占쏙옙占쏙옙, 占쏙옙占쏙옙占쏙옙占쏙옙占쏙옙 占쏙옙 占쌔깍옙 占쏙옙占쏙옙, 占쏙옙占쏙옙占쏙옙 占쌍댐옙 占쏙옙占쏙옙 占쏙옙占쏙옙, 占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙 save & load 占실억옙占?占쌌니댐옙.
     */
    // save & load�뜝�룞�삕 �뜝�떗�슱�삕�뜝�룞�삕 �뜝�룞�삕�뜝�룞�삕�뜝�룞�삕 �뜝�룞�삕�뜝�룞�삕 =======================================================

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
        public float rewardCoolTime;
        public long lastConnectTime;

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
    private float rewardCoolTime;

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

    public float RewardCoolTime
    {
        get { return rewardCoolTime; }
        set { rewardCoolTime = value; }
    }

    public long LastConnectTime
    {
        get { return binaryDataBundle.lastConnectTime; }
        set { binaryDataBundle.lastConnectTime = value; }
    }

    private BinaryDataBundle binaryDataBundle;
    public Dictionary<Jewels, (ulong, bool)> JewlyInfo
    {
        get { return jewelInventory; }
        set { jewelInventory = value; }
    }


    //save & load�뜝�룞�삕 �뜝�떗�슱�삕�뜝�룞�삕 �뜝�룞�삕�뜝�룞�삕�뜝�룞�삕 �뜝�룞�삕 ========================================================================== 

    public float autoSavePeriod = 60.0f;
    private float accTimeForAutoSave;

    private float maxRewardCoolTime = 1800.0f;

    public float MaxRewardCoolTime
    {
        get { return maxRewardCoolTime; }
        set { maxRewardCoolTime = value; }
    }

    private GameObject player;
    private PlayerController pc;
    private String cryptoKey = "IsN24JOasd8F";

    [SerializeField] CalculatePopup calculatePopup;

    public bool IsUnlockedPickAxe(string name)
    {
        if (Enum.TryParse(name, out Pickaxes pickaxe))
        {
            return pickAxesUnlockInfo.TryGetValue(pickaxe, out bool isUnlocked) && isUnlocked;
        }

        Debug.LogWarning($"'{name}'�뜝�룞�삕(�뜝�룞�삕) �뜝�떆諛붾챿�삕 Pickaxes �뜝�룞�삕�뜝�룞�삕 �뜝�떍�떃�땲�뙋�삕. false�뜝�룞�삕 �뜝�룞�삕�솚�뜝�떙�땲�뙋�삕.");
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
            Debug.LogWarning("(" + idxMap + ", " + idxStage + ")�뜝�룞�삕 �뜝�떆諛붾챿�삕 �뜝�룞�삕�뜝�룞�삕�뜝�룞�삕�뜝�룞�삕 �뜝�떥�벝�삕�뜝�룞�삕 �뜝�룞�삕�뜝�룞�삕 �뜝�떍�떃�땲�뙋�삕. false�뜝�룞�삕 �뜝�룞�삕�솚�뜝�떙�땲�뙋�삕.");
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
        rewardCoolTime = maxRewardCoolTime;
        binaryDataBundle.lastConnectTime = DateTime.Now.Ticks;
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
        // �뜝�떆諛붾챿�삕 �뜝�룞�삕�뜝�룞�삕 �뜝�떛諭꾩삕�뜝�룞�삕�뜝�룞�삕 �뜝�뙐袁멸퉵�삕
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
        Save1();
    }

    public void Load()
    {
        String versionString = Load1();

        if (versionString != null)
        {
            if (versionString == "1.0")
            {
                Load1();
            }
            // else ... (for other versions)
        }
    }

    public void Save1()
    {
        String saveFileName = "UserData.bin";
        String savePath = Application.persistentDataPath + "/" + saveFileName;

        const String version = "1.0";

        binaryDataBundle.lastUsedPickAxe = pc.curPickaxe;

        binaryDataBundle.jewelInventory = jewelInventory
            .Select(kv => new JewelEntry { key = kv.Key, cnt = kv.Value.Item1, unlocked = kv.Value.Item2 }).ToList();

        binaryDataBundle.pickAxesUnlockInfo = pickAxesUnlockInfo
            .Select(kv => new PickaxeEntry { key = kv.Key, value = kv.Value }).ToList();

        binaryDataBundle.stagesUnlockInfo = stagesUnlockInfo
            .Select(kv => new StageEntry { idxMap = kv.Key.Item1, idxStage = kv.Key.Item2, value = kv.Value }).ToList();

        binaryDataBundle.lastPlayedMap = lastPlayedStage.Item1;
        binaryDataBundle.lastPlayedStage = lastPlayedStage.Item2;
        binaryDataBundle.lastConnectTime = DateTime.Now.Ticks;
        binaryDataBundle.rewardCoolTime = rewardCoolTime;

        String json = JsonUtility.ToJson(binaryDataBundle);
        String encryptedData = EncryptDecrypt(version + "\n" + json, cryptoKey);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fileStream = new FileStream(savePath + ".tmp", FileMode.Create);

        binaryFormatter.Serialize(fileStream, encryptedData);
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

    public String Load1()
    {
        String loadFileName = "UserData.bin";
        String loadPath = Application.persistentDataPath + "/" + loadFileName;

        const String version = "1.0";

        if (!File.Exists(loadPath))
        {
            Debug.Log("initalized user data as no save file has been found.");
            InitAllUserData();
            return null;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(loadPath, FileMode.Open);

        String encryptedData = binaryFormatter.Deserialize(fileStream) as String;
        String decryptedData = EncryptDecrypt(encryptedData, cryptoKey);

        StringReader reader = new StringReader(decryptedData);
        String versionString = reader.ReadLine();

        if (versionString != version)
        {
            Debug.LogWarning("Mismatched version found for save file.");
            InitAllUserData();
            return versionString;
        }

        String json = reader.ReadToEnd();

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
        rewardCoolTime = CalculateRewardCoolTime();

        Debug.Log("loaded save data.");

        return null;
    }

    private string EncryptDecrypt(string data, string key)
    {
        int keyLen = key.Length;
        char[] output = new char[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            output[i] = (char)(data[i] ^ key[i % keyLen]); // XOR �뿰�궛
        }

        return new string(output);
    }

    private float CalculateRewardCoolTime()
    {
        rewardCoolTime = binaryDataBundle.rewardCoolTime;

        DateTime time = new DateTime(binaryDataBundle.lastConnectTime);

        Debug.Log("留덉��留� �젒�냽 �떆媛�: " + time.ToString("yyyy-MM-dd HH:mm:ss"));

        Debug.Log("�쁽�옱 �젒�냽 �떆媛�: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        long elapsedTicks = DateTime.Now.Ticks - binaryDataBundle.lastConnectTime;

        Debug.Log("ElapsedTicks : " + elapsedTicks);

        long elapsedSeconds = (elapsedTicks / TimeSpan.TicksPerSecond);

        Debug.Log("ElapsedSecons : " + elapsedSeconds);

        // �궓��� 荑⑦���엫 怨꾩궛
        rewardCoolTime -= elapsedSeconds;
        if (rewardCoolTime < 0)
            rewardCoolTime = 0;

        return rewardCoolTime;
    }

    public float ReCalculateRewardCoolTime()
    {
        long currentTime = DateTime.Now.Ticks;
        long elapsedTicks = currentTime - binaryDataBundle.lastConnectTime;
        long elapsedSeconds = elapsedTicks / TimeSpan.TicksPerSecond;

        float newCoolTime = rewardCoolTime - elapsedSeconds;
        if (newCoolTime < 0)
            newCoolTime = 0;

        return newCoolTime;
    }
        

    public void AddJewelCnt(Jewels jewelKey, int jewelCnt)
    {
        jewelInventory[jewelKey] = ( jewelInventory[jewelKey].Item1 + (ulong)jewelCnt, true );
    }

    public string FormatNumber(ulong value)
    {
        string[] suffixes = { "", "K", "M", "G", "T", "P", "E", "Z", "Y" }; // 10^3 �뜝�룞�삕�뜝�룞�삕 �뜝�룞�삕�뜝�룞�삕
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

        Debug.LogWarning($"'{name}'�뜝�룞�삕(�뜝�룞�삕) �뜝�떆諛붾챿�삕 Jewels �뜝�룞�삕�뜝�룞�삕 �뜝�떍�떃�땲�뙋�삕. false�뜝�룞�삕 �뜝�룞�삕�솚�뜝�떙�땲�뙋�삕.");
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

        Debug.LogWarning($"'{name}'�뜝�룞�삕(�뜝�룞�삕) �뜝�떆諛붾챿�삕 Jewels �뜝�룞�삕�뜝�룞�삕 �뜝�떍�떃�땲�뙋�삕. false�뜝�룞�삕 �뜝�룞�삕�솚�뜝�떙�땲�뙋�삕.");
        return false;
    }

    public int GetUnlockedJewelsCount()
    {
        int unlockedCount = 0;

        foreach (var jewel in jewelInventory.Values)
        {
            if (jewel.Item2) // Item2�뒗 bool 媛믪쑝濡�, jewel�씠 �빐湲덈릺�뿀�뒗吏�瑜� �굹����깂
            {
                unlockedCount++;
            }
        }

        return unlockedCount;
    }

    public List<Jewels> GetUnlockedJewels()
    {
        return jewelInventory
            .Where(j => j.Value.Item2) // �빐湲덈맂 蹂댁꽍留� �븘�꽣留�
            .Select(j => j.Key)
            .ToList();
    }
}
