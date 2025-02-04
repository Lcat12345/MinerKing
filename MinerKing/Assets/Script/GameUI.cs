using UnityEngine;

public class GameUI : MonoBehaviour
{
    public UpgradeStatPopup upgradeStatPopup;
    public ChangePickaxePopup changePickaxePopup;
    public CalculatePopup calculatePopup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
