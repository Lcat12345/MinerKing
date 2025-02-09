using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public UpgradeStatPopup upgradeStatPopup;
    public ChangePickaxePopup changePickaxePopup;
    public CalculatePopup calculatePopup;

    private UserDataManager userInfo;

    TextMeshProUGUI CurrencySumText;

    void Start()
    {
        gameObject.SetActive(false);
        upgradeStatPopup.gameObject.SetActive(false);
        changePickaxePopup.gameObject.SetActive(false);
        calculatePopup.gameObject.SetActive(false);

        userInfo = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();

        // µ· ¼³Á¤
        GameObject Currency = transform.Find("Currency").gameObject;
        GameObject Sum = Currency.transform.Find("Sum").gameObject;
        CurrencySumText = Sum.GetComponent<TextMeshProUGUI>();
        CurrencySumText.text = userInfo.FormatNumber(userInfo.Money);

    }
    public void UpdateMoney()
    {
        CurrencySumText.text = userInfo.FormatNumber(userInfo.Money);
    }

    public void OnClickUpgradeStat()
    {
        upgradeStatPopup.gameObject.SetActive(true);
        changePickaxePopup.gameObject.SetActive(false);
        calculatePopup.gameObject.SetActive(false);
    }

    public void OnClickChangePickaxe()
    {
        changePickaxePopup.gameObject.SetActive(true);
        upgradeStatPopup.gameObject.SetActive(false);
        calculatePopup.gameObject.SetActive(false);
    }

    public void OnClickCalculatePopup()
    {
        calculatePopup.gameObject.SetActive(true);
        upgradeStatPopup.gameObject.SetActive(false);
        changePickaxePopup.gameObject.SetActive(false);
    }

    public void OnClickUSCloseArea()
    {
        upgradeStatPopup.gameObject.SetActive(false);
    }

    public void OnClickCPCloseArea()
    {
        changePickaxePopup.gameObject.SetActive(false);
    }

    public void OnClickCCloseArea()
    {
        calculatePopup.gameObject.SetActive(false);

    }
}
