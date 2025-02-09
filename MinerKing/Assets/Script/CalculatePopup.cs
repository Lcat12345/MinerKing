using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class CalculatePopup : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] UserDataManager userDataManager;
    [SerializeField] GameObject contents;
    List<GameObject> jewelList;
    List<GameObject> jewelSubList;
    List<GameObject> lockList;

    [SerializeField] GameObject jewelSubPopup;
    string lastOpenedPopup = "not";

    [SerializeField] GameUI gameUI;

    private void Awake()
    {
        jewelList = new List<GameObject>();
        jewelSubList = new List<GameObject>();
        lockList = new List<GameObject>();

        foreach (Transform child in contents.transform)
        {
            foreach (Transform item in child)
            {
                jewelList.Add(item.gameObject);
                lockList.Add(item.Find("Lock").gameObject);

                Button btn = item.gameObject.GetComponent<Button>();
                btn.onClick.AddListener(() => OpenSubPopupBtn(item.name + "SubPopup"));
            }
        }

        foreach (GameObject jewel in jewelList)
        {
            if (IsJewelUnlocked(jewel))
            {
                jewel.transform.Find("Lock").gameObject.SetActive(false);
            }
        }

        // 서브 팝업 설정
        for(int i = 0; i < JewelData.instance.DataCount(); ++i)
        {
            GameObject subPopup = Instantiate(Resources.Load<GameObject>("UIPrefabs/JewelSubPopup"));

            GameObject jewel = subPopup.transform.Find("Jewel").gameObject;
            GameObject cancelBtn = subPopup.transform.Find("CancelBtn").gameObject;
            GameObject sellBtn = subPopup.transform.Find("SellBtn").gameObject;
            TextMeshProUGUI cnt = jewel.transform.Find("Count").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI coin = subPopup.transform.Find("Currency").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI jewelCnt = subPopup.transform.Find("JewelCount").gameObject.GetComponent<TextMeshProUGUI>();

            Jewels jewelEnum = (Jewels)Enum.GetValues(typeof(Jewels)).GetValue(i);
            Jewel obj = JewelData.instance.Get(jewelEnum);

            subPopup.name = jewelEnum.ToString() + "SubPopup";
            
            GameObject jewelIcon = jewel.transform.Find("Icon").gameObject;
            Image image = jewelIcon.GetComponent<Image>();
            image.sprite = ResourceManager.instance.GetResource<Sprite>("Jewels/64/" + jewelEnum.ToString());

            subPopup.SetActive(false);
            subPopup.transform.SetParent(jewelSubPopup.transform, false);

            if (userDataManager.IsUnlockedJewel(jewelEnum.ToString()))
            {
                GameObject lockIcon = jewel.transform.Find("Lock").gameObject;
                lockIcon.SetActive(false);

                cnt.text = userDataManager.JewlyInfo[jewelEnum].Item1.ToString();
                jewelCnt.text = userDataManager.JewlyInfo[jewelEnum].Item1.ToString();
                coin.text = "0";
            }

            Button btn = cancelBtn.GetComponent<Button>();
            btn.onClick.AddListener(() => CloseSubPopupBtn(subPopup.name));

            btn = sellBtn.GetComponent<Button>();
            btn.onClick.AddListener(() => SellJewelsBtn(jewelEnum));

            jewelSubList.Add(subPopup);
        }
    }

    private void Update()
    {
       updateUI();
    }

    private bool IsJewelUnlocked(GameObject go)
    {
        if (Enum.TryParse(go.name, out Jewels jewel))
        {
            var info = userDataManager.JewlyInfo[jewel];

            TextMeshProUGUI tmp = go.transform.Find("Count").gameObject.GetComponent<TextMeshProUGUI>();
            tmp.text = info.Item1.ToString();

            return info.Item2;
        }

        Debug.LogError("Jewel name is not valid");
        return false;
    }

    void OnEnable()
    {
        ResetScrollPosition();
    }

    private void ResetScrollPosition()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void initUI(Jewels jewel)
    {
        lockList[(int)jewel].SetActive(false);
        jewelSubList[(int)jewel].transform.Find("Jewel").Find("Lock").gameObject.SetActive(false);
    }

    public void updateUI()
    {
        foreach(GameObject jewel in jewelList)
        {
            if (Enum.TryParse(jewel.name, out Jewels j))
            {
                var info = userDataManager.JewlyInfo[j];

                TextMeshProUGUI tmp = jewel.transform.Find("Count").gameObject.GetComponent<TextMeshProUGUI>();
                tmp.text = info.Item1.ToString();
            }
        }

        foreach (GameObject subPopup in jewelSubList)
        {
            string name = subPopup.name.Replace("SubPopup", "");
            if(Enum.TryParse(name, out Jewels j))
            {
                var info = userDataManager.JewlyInfo[j];
                TextMeshProUGUI cnt = subPopup.transform.Find("Jewel").Find("Count").gameObject.GetComponent<TextMeshProUGUI>();
                cnt.text = info.Item1.ToString();
                TextMeshProUGUI tmp = subPopup.transform.Find("JewelCount").gameObject.GetComponent<TextMeshProUGUI>();
                tmp.text = info.Item1.ToString();
            }
        }
    }

    public void OpenSubPopupBtn(string name)
    {
        if(lastOpenedPopup != "not")
        {
            jewelSubPopup.transform.Find(lastOpenedPopup).gameObject.SetActive(false);
            ResetSlider(lastOpenedPopup);
        }

        lastOpenedPopup = name;

        jewelSubPopup.transform.Find(name).gameObject.SetActive(true);
    }

    public void CloseSubPopupBtn(string name)
    {
        if (lastOpenedPopup == "not")
            return;

        if(name == "close")
        {
            jewelSubPopup.transform.Find(lastOpenedPopup).gameObject.SetActive(false);
            ResetSlider(lastOpenedPopup);
        }
        else
        {
            jewelSubPopup.transform.Find(name).gameObject.SetActive(false);
            ResetSlider(name);
        }

        lastOpenedPopup = "not";
    }

    public void ResetSlider(string name)
    {
        Slider s = jewelSubPopup.transform.Find(name + "/Slider02_HandleType").gameObject.GetComponent<Slider>();
        s.value = 0;
    }

    public void SellJewelsBtn(Jewels jewel)
    {
        TextMeshProUGUI sellCnt = transform.Find("Popup/JewelSubPopup/" + jewel.ToString() + "SubPopup/SellCount").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI jewelCnt = jewelList[(int)jewel].transform.Find("Count").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI subPopupJewelCnt = jewelSubPopup.transform.Find(jewel.ToString() + "SubPopup/Jewel/Count").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI currency = jewelSubPopup.transform.Find(jewel.ToString() + "SubPopup/Currency").GetComponent<TextMeshProUGUI>();
        Slider slider = transform.Find("Popup/JewelSubPopup/" + jewel.ToString() + "SubPopup/Slider02_HandleType").GetComponent<Slider>();

        if (ulong.TryParse(sellCnt.text, out ulong result))
        {
            if(result == 0)
            {
                Debug.Log("판매 수량이 0입니다.");
                return;
            }

            ulong jewelCntValue = ulong.Parse(jewelCnt.text);
            ulong diff = jewelCntValue - result;

            jewelCnt.text = diff.ToString();
            subPopupJewelCnt.text = diff.ToString();

            ulong total = result * (JewelData.instance.Get(jewel).value);
            userDataManager.Money += total;

            sellCnt.text = "0";
            currency.text = "0";
            
            ResetSlider(jewel.ToString() + "SubPopup");
            slider.maxValue = diff;

            userDataManager.JewlyInfo[jewel] = (diff, userDataManager.JewlyInfo[jewel].Item2);

            updateUI();
            gameUI.UpdateMoney();
        }
    }
}