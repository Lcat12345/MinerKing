using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StageChangeUI : MonoBehaviour
{
    private TextMeshProUGUI currentStageText;

    private GameObject content;
    private GameObject[] stageLockImages;

    private GameObject stageUnlockPopupUI;
    private GameObject stageUnlockPopupUIChangeButton;
    private TextMeshProUGUI stageUnlockPopupUIText;
    private TextMeshProUGUI stageUnlockPopupUIPriceText;
    private GameObject stageUnlockPopupUIContent;

    private int currentOpenedIdxMap;
    private int currentOpenedIdxStage;
    private int currentOpenedSubPopupIdx;

    private bool IsOpened = false;
    private Animator animator;

    UserDataManager udm;
    Mining mining;
    GameUI gameUI;
    private void Awake()
    {
        udm = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        mining = GameObject.Find("Player").GetComponent<Mining>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

        currentStageText = transform.Find("StageChangeButton/TextStageInfo").GetComponent<TextMeshProUGUI>();
        UpdateCurrentStageInfoUI();

        content = transform.Find("StageChangeButton/Mask/Scroll View/Viewport/Content").gameObject;

        stageUnlockPopupUI = transform.Find("StageUnlockPopupUI").gameObject;

        stageUnlockPopupUIText = stageUnlockPopupUI.transform.Find("StageUnlockSubPopup/StageInfo").GetComponent<TextMeshProUGUI>();
        stageUnlockPopupUIChangeButton = stageUnlockPopupUI.transform.Find("StageUnlockSubPopup/ChangeBtn").gameObject;
        stageUnlockPopupUIPriceText = stageUnlockPopupUI.transform.Find("StageUnlockSubPopup/StagePrice/Price").GetComponent<TextMeshProUGUI>();
        stageUnlockPopupUIContent = stageUnlockPopupUI.transform.Find("StageUnlockSubPopup/Scroll View/Viewport/Content").gameObject;

        stageUnlockPopupUI.SetActive(false);

        animator = transform.Find("StageChangeButton").GetComponent<Animator>();

        stageLockImages = new GameObject[StageData.instance.Count()];

        List<(int, int)> stageIndexes = StageData.instance.GetStageIndex();
        GameObject stageChangeSubPopup = Resources.Load<GameObject>("UIPrefabs/StageChangeSubPopup");

        for (int i = 0; i < stageLockImages.Length; ++i)
        {
            GameObject clone = Instantiate(stageChangeSubPopup);
            TextMeshProUGUI si = clone.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI stageInfo = clone.transform.Find("Lock/StageInfo").GetComponent<TextMeshProUGUI>();

            int idxMap = stageIndexes[i].Item1;
            int idxStage = stageIndexes[i].Item2;
            int idxImage = i;

            // 스테이지 이름
            clone.name = $"{idxMap.ToString()}-{idxStage.ToString()}";
            // 부모 설정
            clone.transform.SetParent(content.transform, false);
            // 클릭하면 unlockPopup이 나오도록 이벤트 등록
            Button button = clone.GetComponent<Button>();
            button.onClick.AddListener(() => OnClickUnlockStageButton(idxMap, idxStage, idxImage));
            // 잠금 화면에 표시
            stageInfo.text = $"Stage {(idxMap + 1).ToString()}-{(idxStage + 1).ToString()}";
            // 논 잠금 화면에 표시
            si.text = $"Stage {(idxMap + 1).ToString()}-{(idxStage + 1).ToString()}";

            stageLockImages[i] = clone.transform.Find("Lock").gameObject;
            // 만약 이미 해금한 스테이지면 표시해야함
            if (udm.IsUnlockedStage(idxMap, idxStage)) 
            {
                stageLockImages[i].SetActive(false);
            }
        }

        content.SetActive(false);
    }

    public void OnClickStageChangeButton()
    {
        if (IsOpened)
        {
            animator.SetTrigger("Close");
            content.SetActive(false);
            IsOpened = !IsOpened;
        }
        else
        {
            animator.SetTrigger("Open");
            content.SetActive(true);
            IsOpened = !IsOpened;
        }
    }

    public void OnClickUnlockStageButton(int idxMap, int idxStage, int index)
    {
        SetUnlockSubPopupContent(idxMap, idxStage);
        currentOpenedIdxMap = idxMap;
        currentOpenedIdxStage = idxStage;
        currentOpenedSubPopupIdx = index;
        stageUnlockPopupUI.SetActive(true);
    }

    public void OnClickUnlockStageCancelButton()
    {
        ClearUnlockSubPopupContent();
        stageUnlockPopupUI.SetActive(false);
    }

    public void OnClickUnlockStagePurchaseButton()
    {
        ulong price = StageData.instance.GetPrice(currentOpenedIdxMap, currentOpenedIdxStage);

        if(udm.Money >= price)
        {
            if(udm.Money == price)
                udm.Money = 0;

            udm.Money -= price;

            udm.UnlockStage(currentOpenedIdxMap, currentOpenedIdxStage);
            stageLockImages[currentOpenedSubPopupIdx].SetActive(false);
            stageUnlockPopupUIChangeButton.SetActive(true);
            gameUI.UpdateMoney();
        }
    }

    public void OnClickUnlockStageChangeButton()
    {
        mining.playerController.OnChangeMap(currentOpenedIdxMap);
        mining.mapController.ChangeMap(currentOpenedIdxMap, currentOpenedIdxStage);
        UpdateCurrentStageInfoUI();
        OnClickUnlockStageCancelButton();
    }

    public void UpdateCurrentStageInfoUI()
    {
        currentStageText.text = $"Stage {(udm.LastPlayedStage.Item1 + 1).ToString()}-{(udm.LastPlayedStage.Item2 + 1).ToString()}";
    }

    private void SetUnlockSubPopupContent(int idxMap, int idxStage)
    {
        stageUnlockPopupUIText.text = $"Stage {idxMap+1}-{idxStage+1}";

        // 스테이지 가격 표시
        stageUnlockPopupUIPriceText.text = udm.FormatNumber(StageData.instance.GetPrice(idxMap, idxStage));

        // 스테이지에 어떤 보석이 나오는지...
        var stageData = StageData.instance.GetProbabilities(idxMap, idxStage);

        // 해금한 스테이지가 아니면 스테이지 바꾸는 이미지가 안보여야함
        if (!udm.IsUnlockedStage(idxMap, idxStage))
        {
            stageUnlockPopupUIChangeButton.SetActive(false);
        }
        else
        {
            stageUnlockPopupUIChangeButton.SetActive(true);
        }

        RectTransform rt = stageUnlockPopupUIContent.GetComponent<RectTransform>();

        // 나오는 보석이 3개 이하일 때는 피봇을 중앙으로 설정해야됨
        if (stageData.Count < 4)
            rt.pivot = new Vector2(0.5f, rt.pivot.y);
        else
            rt.pivot = new Vector2(0.0f, rt.pivot.y);

        foreach(var data in stageData)
        {
            GameObject stageUnlockPopupContent = Instantiate(ResourceManager.instance.GetResource<GameObject>("UIPrefabs/StageUnlockPopupContent"));

            stageUnlockPopupContent.transform.SetParent(stageUnlockPopupUIContent.transform, false);

            Image image = stageUnlockPopupContent.GetComponent<Image>();
            TextMeshProUGUI text = stageUnlockPopupContent.GetComponentInChildren<TextMeshProUGUI>();

            Jewels jewel = data.Key;
            float probability = data.Value;

            image.sprite = ResourceManager.instance.GetResource<Sprite>("Jewels/64/"+jewel.ToString());
            text.text = $"{probability}%";
        }
    }

    private void ClearUnlockSubPopupContent()
    {
        foreach(Transform child in stageUnlockPopupUIContent.transform)
            Destroy(child.gameObject);
    }
}
