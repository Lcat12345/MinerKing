using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    /*
     * 유저가 게임을 껐다 켰다 할 때 남는 정보들 입니다.
     * 돈, 마지막으로 쓰고 있던 곡괭이, 보석 해금 정보, 곡괭이 해금 정보, 스테이지와 맵 해금 정보, 가지고 있는 보석 정보, 스탯 정보 등이 save & load 되어야 합니다.
     */
    private ulong money;
    private Dictionary<string, ulong> jewelInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        money = 0;
        jewelInventory = new Dictionary<string, ulong>();
        jewelInventory.Add("루비", 0);
        jewelInventory.Add("사파이어", 0);
        jewelInventory.Add("에메랄드", 0);
        jewelInventory.Add("토파즈", 0);
        jewelInventory.Add("아메시스트", 0);
        jewelInventory.Add("가넷", 0);
        jewelInventory.Add("오팔", 0);
        jewelInventory.Add("터키석", 0);
        jewelInventory.Add("페리도트", 0);
        jewelInventory.Add("탄자나이트", 0);
        jewelInventory.Add("스피넬", 0);
        jewelInventory.Add("알렉산드라이트", 0);
        jewelInventory.Add("아쿠아마린", 0);
        jewelInventory.Add("모건나이트", 0);
        jewelInventory.Add("로돌라이트", 0);
        jewelInventory.Add("차보라이트", 0);
        jewelInventory.Add("제다이트", 0);
        jewelInventory.Add("래브라도라이트", 0);
        jewelInventory.Add("문스톤", 0);
        jewelInventory.Add("혈석", 0);
        jewelInventory.Add("다이아몬드", 0);
        jewelInventory.Add("라피스라줄리", 0);
        jewelInventory.Add("사옥", 0);
        jewelInventory.Add("몰다바이트", 0);
        jewelInventory.Add("아이올라이트", 0);
        jewelInventory.Add("시트린", 0);
        jewelInventory.Add("자수정", 0);
        jewelInventory.Add("코랄", 0);
        jewelInventory.Add("앰버", 0);
        jewelInventory.Add("크리소베릴", 0);
        jewelInventory.Add("주홍석", 0);
        jewelInventory.Add("마노", 0);
        jewelInventory.Add("카이아나이트", 0);
        jewelInventory.Add("안데신", 0);
        jewelInventory.Add("헴타이트", 0);
        jewelInventory.Add("슈가라이트", 0);
        jewelInventory.Add("말라카이트", 0);
        jewelInventory.Add("차로이트", 0);
        jewelInventory.Add("재브라 재스퍼", 0);
        jewelInventory.Add("핑크 투르말린", 0);
        jewelInventory.Add("블루 투르말린", 0);
        jewelInventory.Add("블루 재스퍼", 0);
        jewelInventory.Add("유문석", 0);
        jewelInventory.Add("타이거 아이", 0);
        jewelInventory.Add("하울라이트", 0);
        jewelInventory.Add("로도크로사이트", 0);
        jewelInventory.Add("아즈라이트", 0);
        jewelInventory.Add("플루오라이트", 0);
        jewelInventory.Add("스쿠폴라이트", 0);
        jewelInventory.Add("피닉스의 눈물", 0);
        jewelInventory.Add("드래곤스톤", 0);
        jewelInventory.Add("월광석", 0);
        jewelInventory.Add("마나 크리스탈", 0);
        jewelInventory.Add("인피니티 스톤", 0);
        jewelInventory.Add("오리칼쿰", 0);
        jewelInventory.Add("솔라리스의 심장", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
