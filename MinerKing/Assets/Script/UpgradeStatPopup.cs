using TMPro;
using UnityEngine;

public class UpgradeStatPopup : MonoBehaviour
{
    UserDataManager userInfo;

    GameObject MiningSpeedUpArea;
    GameObject MoveSpeedUpArea;
    GameObject CurrentStat;

    TextMeshProUGUI MiningSpeedLevelText;
    TextMeshProUGUI MiningSpeedRateText;
    TextMeshProUGUI MiningSpeedPriceText;

    TextMeshProUGUI MovingSpeedLevelText;
    TextMeshProUGUI MovingSpeedRateText;
    TextMeshProUGUI MovingSpeedPriceText;

    TextMeshProUGUI currentMiningStat;
    TextMeshProUGUI currentMovingStat;

    GameObject player;
    PlayerController pc;

    private void Start()
    {
        userInfo = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();

        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        GameObject Popup = transform.Find("Popup").gameObject;

        MiningSpeedUpArea = Popup.transform.Find("MiningSpeedUpArea").gameObject;
        MoveSpeedUpArea = Popup.transform.Find("MoveSpeedUpArea").gameObject;
        CurrentStat = Popup.transform.Find("CurrentStat").gameObject;

        Transform StatField = MiningSpeedUpArea.transform.Find("StatField");
        Transform UpBtn = MiningSpeedUpArea.transform.GetChild(2);

        MiningSpeedLevelText = StatField.Find("LevelText").GetComponent<TextMeshProUGUI>();
        MiningSpeedRateText = StatField.Find("RateText").GetComponent<TextMeshProUGUI>();
        MiningSpeedPriceText = UpBtn.GetComponentInChildren<TextMeshProUGUI>();

        StatField = MoveSpeedUpArea.transform.Find("StatField");
        UpBtn = MoveSpeedUpArea.transform.GetChild(2);

        MovingSpeedLevelText = StatField.Find("LevelText").GetComponent<TextMeshProUGUI>();
        MovingSpeedRateText = StatField.Find("RateText").GetComponent<TextMeshProUGUI>();
        MovingSpeedPriceText = UpBtn.GetComponentInChildren<TextMeshProUGUI>();

        Transform miningStat = CurrentStat.transform.GetChild(2);
        Transform movingStat = CurrentStat.transform.GetChild(3);

        currentMiningStat = miningStat.GetComponent<TextMeshProUGUI>();
        currentMovingStat = movingStat.GetComponent<TextMeshProUGUI>();

        UpdateMiningStatUI();
        UpdateMovingStatUI();
        UpdateCurrentStatUI();
    }

    public void UpdateMiningStatUI()
    {
        uint level = userInfo.StatMining;

        MiningSpeedLevelText.text = $"{level} LV";
        MiningSpeedRateText.text = $"+ {level} %p";

        ulong price = CalculateLevelPrice(level);
        MiningSpeedPriceText.text = userInfo.FormatNumber(price);
    }
    public void UpdateMovingStatUI()
    {
        uint level = userInfo.StatMoving;

        MovingSpeedLevelText.text = $"{level} LV";
        MovingSpeedRateText.text = $"+ {level} %p";

        ulong price = CalculateLevelPrice(level);
        MovingSpeedPriceText.text = userInfo.FormatNumber(price);
    }

    public void UpdateCurrentStatUI()
    {
        Pickaxe pickaxe = PickaxeData.instance.Get(pc.curPickaxe);
        
        currentMiningStat.text = $"+{userInfo.StatMining + pickaxe.miningVelocity}%p <color=#FF0000>(+{pickaxe.miningVelocity}%p)</color>";
        currentMovingStat.text = $"+{userInfo.StatMoving}%p";
    }

    public void OnClickMiningSpeedLevelUp()
    {
        ulong price = CalculateLevelPrice(userInfo.StatMining);

        if (userInfo.Money >= price) 
        {
            if (userInfo.Money == price)
                userInfo.Money = 0;

            userInfo.Money = userInfo.Money - price;

            userInfo.StatMining += 1;

            UpdateMiningStatUI();
            UpdateCurrentStatUI();
        }
    }

    public void OnClickMovingSpeedLevelUp()
    {
        ulong price = CalculateLevelPrice(userInfo.StatMoving);

        if (userInfo.Money >= price)
        {
            if (userInfo.Money == price)
                userInfo.Money = 0;

            userInfo.Money = userInfo.Money - price;

            userInfo.StatMoving += 1;

            UpdateMovingStatUI();
            UpdateCurrentStatUI();
        }
    }

    private ulong CalculateLevelPrice(uint level)
    {
        return 100UL * ((ulong)level * level * level + 1);
    }
}
