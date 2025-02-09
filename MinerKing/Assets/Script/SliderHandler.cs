using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sellCnt;
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] GameObject go;
    Slider slider;
    UserDataManager userDataManager;

    private void Awake()
    {
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        slider = go.GetComponent<Slider>();
        slider.value = 0;
    }

    private void Update()
    {
        string jewelName = transform.name.Replace("SubPopup", "");
        Enum.TryParse(jewelName, out Jewels jewel);

        ulong maxCnt = userDataManager.JewlyInfo[jewel].Item1;
        slider.maxValue = maxCnt;
    }

    public void OnSliderEvent(float value)
    {
        string jewelName = transform.name.Replace("SubPopup", "");
        int sc = Mathf.FloorToInt(value);

        Enum.TryParse(jewelName, out Jewels jewel);
        ulong price = JewelData.instance.Get(jewel).value;
        currency.text = userDataManager.FormatNumber(price * (ulong)sc);

        sellCnt.text = $"{sc}";
    }
}
