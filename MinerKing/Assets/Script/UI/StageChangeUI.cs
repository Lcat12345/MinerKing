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
    private GameObject[] contents;

    private GameObject stageUnlockPopupUI;
    private TextMeshProUGUI stageUnlockPopupUIText;
    private GameObject stageUnlockPopupUIContent;

    private bool IsOpened = false;
    private Animator animator;

    private void Awake()
    {
        currentStageText = transform.Find("StageChangeButton/TextStageInfo").GetComponent<TextMeshProUGUI>();
        content = transform.Find("StageChangeButton/Mask/Scroll View/Viewport/Content").gameObject;
        stageUnlockPopupUI = transform.Find("StageUnlockPopupUI").gameObject;
        stageUnlockPopupUIText = stageUnlockPopupUI.transform.Find("StageUnlockSubPopup/StageInfo").GetComponent<TextMeshProUGUI>();
        stageUnlockPopupUIContent = stageUnlockPopupUI.transform.Find("StageUnlockSubPopup/Scroll View/Viewport/Content").gameObject;

        stageUnlockPopupUI.SetActive(false);

        animator = transform.Find("StageChangeButton").GetComponent<Animator>();

        contents = new GameObject[StageData.instance.Count()];

        List<(int, int)> stageIndexes = StageData.instance.GetStageIndex();
        GameObject stageChangeSubPopup = Resources.Load<GameObject>("UIPrefabs/StageChangeSubPopup");

        for (int i = 0; i < contents.Length; ++i)
        {
            GameObject clone = Instantiate(stageChangeSubPopup);
            TextMeshProUGUI stageInfo = clone.transform.Find("Lock/StageInfo").GetComponent<TextMeshProUGUI>();

            int idxMap = stageIndexes[i].Item1;
            int idxStage = stageIndexes[i].Item2;

            // 스테이지 이름
            clone.name = $"{idxMap.ToString()}-{idxStage.ToString()}";
            // 부모 설정
            clone.transform.SetParent(content.transform, false);
            // 클릭하면 unlockPopup이 나오도록 이벤트 등록
            Button button = clone.GetComponent<Button>();
            button.onClick.AddListener(() => OnClickUnlockStageButton(idxMap, idxStage));
            // 잠금 화면에 표시
            stageInfo.text = $"Stage {idxMap.ToString()}-{idxStage.ToString()}";
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

    public void OnClickUnlockStageButton(int idxMap, int idxStage)
    {
        SetUnlockSubPopupContent(idxMap, idxStage);
        stageUnlockPopupUI.SetActive(true);
    }

    public void OnClickUnlockStageCancelButton()
    {
        ClearUnlockSubPopupContent();
        stageUnlockPopupUI.SetActive(false);
    }

    private void SetUnlockSubPopupContent(int idxMap, int idxStage)
    {
        stageUnlockPopupUIText.text = $"Stage {idxMap}-{idxStage}";

        // 스테이지에 어떤 보석이 나오는지...
        var stageData = StageData.instance.GetProbabilities(idxMap, idxStage);

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
