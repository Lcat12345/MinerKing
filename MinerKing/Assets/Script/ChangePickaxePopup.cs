using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ChangePickaxePopup : MonoBehaviour
{
    public ScrollRect scrollRect;

    [SerializeField]
    private GameObject pickAxeContent;

    private List<GameObject> pickAxeObjects;
    private GameObject pickAxeSubPopups;

    private UserDataManager userInfo;

    string lastOpenedPopup = "not";

    GameObject player;
    PlayerController pc;
    Animator weapon;

    GameUI gameUI;

    void Start()
    {
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        
        weapon = player.transform.Find("Weapon").gameObject.GetComponent<Animator>();

        GameObject go = GameObject.Find("UserDataManager");
        userInfo = go.GetComponent<UserDataManager>();

        Transform popup = transform.Find("Popup");
        pickAxeSubPopups = popup.Find("PickaxeSubPopup").gameObject;

        pickAxeObjects = new List<GameObject>();

        OnEquipmentBtn(userInfo.LastUsedPickAxe);

        // 곡괭이 UI 오브젝트들을 들고 있게 한다.
        foreach (Transform items in pickAxeContent.transform)
        {
            foreach (Transform item in items.transform)
            {
                pickAxeObjects.Add(item.gameObject);
            }
        }

        // 해금되어 있다면 잠금 해제해야한다.
        foreach (GameObject pickAxe in pickAxeObjects)
        {
            Transform lockUI = pickAxe.transform.Find("Lock");

            if (userInfo.IsUnlockedPickAxe(pickAxe.name))
            {
                lockUI.gameObject.SetActive(false);
            }
        }

        // 서브 팝업 설정
        for (int i = 0; i < PickaxeData.instance.DataCount(); ++i)
        {
            GameObject subPopup = Instantiate(Resources.Load<GameObject>("UIPrefabs/PickAxeSubPopup"));

            GameObject Pickaxe = subPopup.transform.Find("Pickaxe").gameObject;
            GameObject CancelBtn = subPopup.transform.Find("CancelBtn").gameObject;
            GameObject EquipmentBtn = subPopup.transform.Find("EquipmentBtn").gameObject;
            GameObject MiningSpeed = subPopup.transform.Find("MiningSpeed").gameObject;
            GameObject PurchaseBtn = subPopup.transform.Find("PurchaseBtn").gameObject;

            // i 번째 곡괭이 정보
            Pickaxes pickaxeEnum = (Pickaxes)Enum.GetValues(typeof(Pickaxes)).GetValue(i);
            Pickaxe data = PickaxeData.instance.Get(pickaxeEnum);

            // 서브 팝업 이름
            subPopup.name = pickaxeEnum.ToString() + "SubPopup";
            // 서브 팝업의 이미지
            GameObject SubPopupIcon = Pickaxe.transform.Find("Icon").gameObject;
            Image image = SubPopupIcon.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("UISprite/PickAxe/" + pickaxeEnum.ToString());
            // 서브 팝업은 처음에 안보임
            subPopup.SetActive(false);
            subPopup.transform.SetParent(pickAxeSubPopups.transform, false);
            // 만일 해금된 곡괭이면 곡괭이 이미지가 보여야함
            if (userInfo.IsUnlockedPickAxe(pickaxeEnum.ToString()))
            {
                GameObject SubPopupLockIcon = Pickaxe.transform.Find("Lock").gameObject;
                SubPopupLockIcon.SetActive(false);

                EquipmentBtn.SetActive(true);
            }
            // 해금 안됬으면 장착 버튼이 안보여야 함.
            else
            {
                EquipmentBtn.SetActive(false);
            }

            // 곡괭이의 속도 텍스트 설정
            TextMeshProUGUI BonusAbility = MiningSpeed.transform.Find("BonusAbility").GetComponent<TextMeshProUGUI>();
            BonusAbility.text = $"+{data.miningVelocity}%p";

            // 곡괭이의 가격 텍스트 설정
            TextMeshProUGUI Amount = PurchaseBtn.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            Amount.text = userInfo.FormatNumber(data.value);

            // 서브 팝업 취소 버튼 이벤트 등록
            Button button = CancelBtn.GetComponent<Button>();
            button.onClick.AddListener(() => CloseSubPopupBtn(subPopup.name));

            // 서브 팝업 장착 버튼 이벤트 등록
            button = EquipmentBtn.GetComponent<Button>();
            button.onClick.AddListener(() => OnEquipmentBtn(pickaxeEnum));

            // 서브 팝업 구매 버튼 이벤트 등록
            button = PurchaseBtn.GetComponent<Button>();
            button.onClick.AddListener(() => OnPurchaseBtn(pickaxeEnum));
        }


    }

    void OnEnable()
    {
        ResetScrollPosition();
    }

    private void ResetScrollPosition()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void PickaxePopupCancelBtn(GameObject popup)
    {
        popup.SetActive(false);
    }

    public void OpenSubPopupBtn(string name)
    {
        if(lastOpenedPopup != "not")
        {
            pickAxeSubPopups.transform.Find(lastOpenedPopup).gameObject.SetActive(false);
        }

        lastOpenedPopup = name;

        pickAxeSubPopups.transform.Find(name).gameObject.SetActive(true);
    }

    public void CloseSubPopupBtn(string name) 
    {
        if (lastOpenedPopup == "not")
            return;

        if(name == "close")
        {
            pickAxeSubPopups.transform.Find(lastOpenedPopup).gameObject.SetActive(false);
        }
        else
        {
            pickAxeSubPopups.transform.Find(name).gameObject.SetActive(false);
        }

        lastOpenedPopup = "not";
    }

    public void OnEquipmentBtn(Pickaxes pickaxe)
    {
        pc.curPickaxe = pickaxe;

        AnimatorStateInfo currentState = weapon.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = currentState.normalizedTime;

        string path = "Animator/Weapon/"+ pickaxe.ToString();
        weapon.runtimeAnimatorController = ResourceManager.instance.GetResource<RuntimeAnimatorController>(path);

        weapon.Play(currentState.fullPathHash, 0, normalizedTime);
    }

    public void OnPurchaseBtn(Pickaxes pickaxe)
    {
        Pickaxe data = PickaxeData.instance.Get(pickaxe);
        ulong price = data.value;

        if(userInfo.Money >= price)
        {
            if (userInfo.Money == price)
                userInfo.Money = 0;

            userInfo.Money = userInfo.Money - price;

            userInfo.UnlockPickaxe(pickaxe);

            // 서브 팝업의 정보를 업데이트
            GameObject subPopup = pickAxeSubPopups.transform.Find(pickaxe.ToString() + "SubPopup").gameObject;

            GameObject Pickaxe = subPopup.transform.Find("Pickaxe").gameObject;
            GameObject EquipmentBtn = subPopup.transform.Find("EquipmentBtn").gameObject;

            GameObject SubPopupLockIcon = Pickaxe.transform.Find("Lock").gameObject;
            SubPopupLockIcon.SetActive(false);

            EquipmentBtn.SetActive(true);

            // 팝업에 있는 무기 이미지가 보여야 함.
            foreach(GameObject obj in pickAxeObjects)
            {
                if(obj.name == pickaxe.ToString())
                {
                    Transform lockUI = obj.transform.Find("Lock");
                    lockUI.gameObject.SetActive(false);
                }
            }

            

            gameUI.UpdateMoney();
        }
        else
        {
            Debug.Log("돈없어");
        }
    }
}
