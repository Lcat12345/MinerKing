using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class GameUI : MonoBehaviour
{
    public UpgradeStatPopup upgradeStatPopup;
    public ChangePickaxePopup changePickaxePopup;
    public CalculatePopup calculatePopup;

    private UserDataManager userInfo;

    private Animator adBtnAnimator;
    private TextMeshProUGUI adBtnText;
    private float timeElapsed = 0.0f;

    private GameObject adBtnPopupUI;
    private GameObject adBtnPopupTimer;
    private TextMeshProUGUI adPopupTimerText;

    TextMeshProUGUI CurrencySumText;

    void Start()
    {
        adBtnPopupUI = transform.Find("AdBtnPopupUI").gameObject;
        adBtnPopupTimer = adBtnPopupUI.transform.Find("AdBtnPopup/Timer").gameObject;
        adPopupTimerText = adBtnPopupTimer.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
        adBtnPopupUI.SetActive(false);
        upgradeStatPopup.gameObject.SetActive(false);
        changePickaxePopup.gameObject.SetActive(false);
        calculatePopup.gameObject.SetActive(false);

        userInfo = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();

        // 돈 설정
        GameObject Currency = transform.Find("Currency").gameObject;
        GameObject Sum = Currency.transform.Find("Sum").gameObject;
        CurrencySumText = Sum.GetComponent<TextMeshProUGUI>();
        CurrencySumText.text = userInfo.FormatNumber(userInfo.Money);

        // 타이머 설정
        adBtnText = transform.Find("AdBtn/TimerText").GetComponent<TextMeshProUGUI>();
        adBtnText.text = FormatTime(userInfo.RewardCoolTime);
        // 광고 보상 이미지
        adBtnAnimator = transform.Find("AdBtn/BtnImage").gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (userInfo.RewardCoolTime >= 0.0f && timeElapsed >= 1.0f)
        {
            timeElapsed = 0.0f;

            // 1초씩 차감
            userInfo.RewardCoolTime -= 1.0f;

            if (userInfo.RewardCoolTime <= 0.0f)
            {
                userInfo.RewardCoolTime = 0.0f;
                adBtnAnimator.SetBool("IsActived", true);
                adBtnPopupTimer.SetActive(false);
            }

            // 텍스트 업데이트
            string time = FormatTime(userInfo.RewardCoolTime);
            adBtnText.text = time;
            adPopupTimerText.text = time;
        }
    }

    // 시간을 "HH:MM:SS" 형식으로 포맷하는 함수
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:D2}:{seconds:D2}";
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

    public void OnClickOpenAdPopup()
    {
        adBtnPopupUI.SetActive(true);
    }

    public void OnClickCloseAdPopup()
    {
        adBtnPopupUI.SetActive(false);
    }

    public void OnClickCloseAd()
    {
        adBtnPopupUI.SetActive(false);
        GenerateReward();
        ResetAdTimer();
    }

    private void ResetAdTimer()
    {
        // 쿨타임을 다시 최대로
        userInfo.RewardCoolTime = userInfo.MaxRewardCoolTime;
        //  UI 애니메이션 없음
        adBtnAnimator.SetBool("IsActived", false);
        // 타이머를 다 기다릴 때까지 광고를 볼 수 없음.
        adBtnPopupTimer.SetActive(true);

        string time = FormatTime(userInfo.RewardCoolTime);
        // 쿨타임 글자 다시 설정
        adBtnText.text = time;
        // 팝업 쿨타임 글자 다시 설정
        adPopupTimerText.text = time;
    }

    private void GenerateReward()
    {
        int count = userInfo.GetUnlockedJewelsCount();

        count = 80 + (count * 20);

        // 해금된 보석 리스트 가져오기
        List<Jewels> unlockedJewels = userInfo.GetUnlockedJewels();

        if (unlockedJewels.Count == 0)
        {
            Debug.Log("해금된 보석이 없습니다.");
            return;
        }

        // 보상 개수 분배
        Dictionary<Jewels, int> rewardJewels = new Dictionary<Jewels, int>();

        for (int i = 0; i < count; i++)
        {
            // 랜덤한 해금된 보석 선택
            Jewels selectedJewel = unlockedJewels[UnityEngine.Random.Range(0, unlockedJewels.Count)];

            // 보석 개수 증가
            if (!rewardJewels.ContainsKey(selectedJewel))
            {
                rewardJewels[selectedJewel] = 0;
            }
            rewardJewels[selectedJewel]++;

            // 실제 인벤토리에 반영
            userInfo.AddJewelCnt(selectedJewel, 1);
        }

        // 보상 결과 출력
        foreach (var reward in rewardJewels)
        {
            Debug.Log($"보석 {reward.Key} x {reward.Value} 개 생성됨");
        }
    }
}
