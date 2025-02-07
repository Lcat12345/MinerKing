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

        currentMiningStat = CurrentStat.GetComponentInChildren

        UpdateMiningStat();
        UpdateMovingStat();
        UpdateCurrentStat();
    }

    public void UpdateMiningStat()
    {
        uint level = userInfo.StatMining;

        MiningSpeedLevelText.text = $"{level} LV";
        MiningSpeedRateText.text = $"+ {level} %p";

        ulong price = 100UL * ((ulong)level * level * level + 1);
        MiningSpeedPriceText.text = userInfo.FormatNumber(price);
    }
    public void UpdateMovingStat()
    {

    }

    public void UpdateCurrentStat()
    {

    }
}
