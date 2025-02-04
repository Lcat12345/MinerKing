using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public enum Jewels
{
    Ruby, Sapphire, Emerald, Topaz, Amethyst,
    Garnet, Opal, Turquoise, Peridot, Tanzanite,
    Spinel, Alexandrite, Aquamarine, Morganite, Rhodolite,
    Tsavorite, Jadeite, Labradorite, Moonstone, Bloodstone,
    Diamond, LapisLazuli, Onyx, Moldavite, Iolite,
    Citrine, Ametrine, Coral, Amber, Chrysoberyl,
    Carnelian, Agate, Kyanite, Andesine, Hematite,
    Sugilite, Malachite, Charoite, ZebraJasper, PinkTourmaline,
    BlueTourmaline, BlueJasper, Unakite, TigerEye, Howlite,
    Rhodochrosite, Azurite, Fluorite, Scapolite, PhoenixTear,
    DragonStone, MoonlightGem, ManaCrystal, InfinityStone, Orichalcum,
    HeartOfSolaris
}

public class Jewel
{
    public Jewel(String aName, ulong aValue, uint aRegistance)
    {
        name = aName;
        value = aValue;
        registance = aRegistance;
    }

    public String name;
    public ulong value;
    public uint registance;
}

public class JewelData : SingletonLazy<JewelData>
{
    public JewelData()
    {
        data = new List<Jewel>();

        data.Add(new Jewel("루비", 100, 10));
        data.Add(new Jewel("사파이어", 300, 16));
        data.Add(new Jewel("에메랄드", 600, 21));
        data.Add(new Jewel("토파즈", 1000, 25));
        data.Add(new Jewel("아메시스트", 1800, 32));
        data.Add(new Jewel("가넷", 2500, 40));
        data.Add(new Jewel("오팔", 4000, 52));
        data.Add(new Jewel("터키석", 7200, 58));
        data.Add(new Jewel("페리도트", 10000, 65));
        data.Add(new Jewel("탄자나이트", 15000, 70));
        data.Add(new Jewel("스피넬", 18000, 74));
        data.Add(new Jewel("알렉산드라이트", 25000, 81));
        data.Add(new Jewel("아쿠아마린", 40000, 88));
        data.Add(new Jewel("모건나이트", 50000, 90));
        data.Add(new Jewel("로돌라이트", 72000, 95));
        data.Add(new Jewel("차보라이트", 100000, 102));
        data.Add(new Jewel("제다이트", 135000, 108));
        data.Add(new Jewel("래브라도라이트", 180000, 114));
        data.Add(new Jewel("문스톤", 240000, 120));
        data.Add(new Jewel("혈석", 350000, 125));
        data.Add(new Jewel("다이아몬드", 450000, 135));
        data.Add(new Jewel("라피스라줄리", 520000, 140));
        data.Add(new Jewel("사옥", 700000, 144));
        data.Add(new Jewel("몰다바이트", 1000000, 148));
        data.Add(new Jewel("아이올라이트", 1300000, 153));
        data.Add(new Jewel("시트린", 2000000, 160));
        data.Add(new Jewel("자수정", 3200000, 165));
        data.Add(new Jewel("코랄", 4000000, 168));
        data.Add(new Jewel("앰버", 6300000, 174));
        data.Add(new Jewel("크리소베릴", 8000000, 180));
        data.Add(new Jewel("주홍석", 10000000, 184));
        data.Add(new Jewel("마노", 15000000, 188));
        data.Add(new Jewel("카이아나이트", 18500000, 193));
        data.Add(new Jewel("안데신", 23000000, 197));
        data.Add(new Jewel("헴타이트", 32000000, 202));
        data.Add(new Jewel("슈가라이트", 40000000, 206));
        data.Add(new Jewel("말라카이트", 56000000, 212));
        data.Add(new Jewel("차로이트", 80000000, 217));
        data.Add(new Jewel("재브라 재스퍼", 100000000, 223));
        data.Add(new Jewel("핑크 투르말린", 125000000, 228));
        data.Add(new Jewel("블루 투르말린", 138000000, 232));
        data.Add(new Jewel("블루 재스퍼", 154000000, 238));
        data.Add(new Jewel("유문석", 200000000, 244));
        data.Add(new Jewel("타이거 아이", 245000000, 249));
        data.Add(new Jewel("하울라이트", 300000000, 256));
        data.Add(new Jewel("로도크로사이트", 460000000, 270));
        data.Add(new Jewel("아즈라이트", 535000000, 278));
        data.Add(new Jewel("플루오라이트", 640000000, 291));
        data.Add(new Jewel("스쿠폴라이트", 850000000, 305));
        data.Add(new Jewel("피닉스의 눈물", 1200000000, 314));
        data.Add(new Jewel("드래곤스톤", 3300000000, 332));
        data.Add(new Jewel("월광석", 4000000000, 340));
        data.Add(new Jewel("마나 크리스탈", 6400000000, 350));
        data.Add(new Jewel("인피니티 스톤", 7500000000, 365));
        data.Add(new Jewel("오리칼쿰", 11000000000, 386));
        data.Add(new Jewel("솔라리스의 심장", 13500000000, 400));
    }

    public Jewel Get(Jewels eJewel)
    {
        return data[(int)eJewel];
    }

    private List<Jewel> data;
}

public enum Pickaxes
{
    HonedPickaxe, SteelPickaxe, StoneSplitter, FangOfTheEarth, MagmaCleaver,
    DiamondPickaxe, WaterjetPickaxe, WhisperOfJewels, MithrilBreaker, StarshardPickaxe,
    ForceOfNature, PickaxeOfMana, VisionCleaver, FantasticPickaxe
}

public class Pickaxe
{
    public Pickaxe(String aName, ulong aValue, uint aMiningVelocity)
    {
        name = aName;
        value = aValue;
        miningVelocity = aMiningVelocity;
    }

    public String name;
    public ulong value;
    public uint miningVelocity;
}

public class PickaxeData : SingletonLazy<PickaxeData>
{
    public PickaxeData()
    {
        data = new List<Pickaxe>();

        data.Add(new Pickaxe("연마된 곡괭이", 6000, 60));
        data.Add(new Pickaxe("강철 곡괭이", 80000, 150));
        data.Add(new Pickaxe("바위 쪼개기", 950000, 210));
        data.Add(new Pickaxe("대지의 이빨", 7200000, 350));
        data.Add(new Pickaxe("마그마 클리버", 45000000, 560));
        data.Add(new Pickaxe("다이아몬드 곡괭이", 800000000, 800));
        data.Add(new Pickaxe("워터젯 곡괭이", 3400000000, 1020));
        data.Add(new Pickaxe("보석의 속삭임", 18500000000, 1250));
        data.Add(new Pickaxe("미스릴 파괴자", 72000000000, 1500));
        data.Add(new Pickaxe("별빛 파편 곡괭이", 360000000000, 1820));
        data.Add(new Pickaxe("대자연의 힘", 2500000000000, 2100));
        data.Add(new Pickaxe("마나의 곡괭이", 12000000000000, 2350));
        data.Add(new Pickaxe("비전 클리버", 80000000000000, 2600));
        data.Add(new Pickaxe("환상의 곡괭이", 200000000000000, 3000));
    }

    public Pickaxe Get(Pickaxes ePickaxe)
    {
        return data[(int)ePickaxe];
    }

    private List<Pickaxe> data;
}

public class Mining : MonoBehaviour
{
    public CameraController cameraController;
    public PlayerController playerController;
    public MapController mapController;
    public InputAction iaMine;

    private float elapsedTime = 0.0f;
    private float targetTime = 0.0f;
    private int shownParticleCnt = 0;
    private ulong minedBlocks = 0;

    public ulong MinedBlocks { get { return minedBlocks; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        int idxJewel = (int)(minedBlocks % (ulong)mapController.mapWidth);

        Jewel jewel = mapController.GetJewelData(idxJewel);

        // idle -> mining
        if (iaMine.WasPressedThisFrame() && playerController.State == PlayerState.IdleState)
        {
            if (jewel == null)
            {
                Debug.LogWarning("jewel at " + idxJewel + " is null!");
            }

            //targetTime = jewel.registance / 30;
            targetTime = Mathf.Pow(2,
                (jewel.registance - 10.0f *
                    (1.0f + playerController.calcMiningBonus() / 100.0f + PickaxeData.instance.Get(playerController.curPickaxe).miningVelocity / 100.0f)
                ) / 10.0f
            );
            elapsedTime = 0;

            mapController.OnMiningStart(minedBlocks, 8.0f / Mathf.Sqrt(targetTime));
            playerController.ChangeState(PlayerState.MiningState);

            return;
        }

        if (playerController.State == PlayerState.MiningState)
        {
            int particleCnt = (int)(
                ( Mathf.Sqrt((float)jewel.registance - 9.0f) + 5.0f )
                * elapsedTime / targetTime
            ) - shownParticleCnt;
            mapController.GenerateParticles(particleCnt, minedBlocks);
            shownParticleCnt += particleCnt;

            Debug.Log("Mining... elapsed: " + elapsedTime + ", target time: " + targetTime);
            elapsedTime += Time.deltaTime;

            // mining -> moving
            if (elapsedTime > targetTime)
            {
                mapController.GenerateParticles(10, minedBlocks);    // when finishing mining, show 10 additional particles.
                shownParticleCnt = 0;

                Debug.Log("Mined " + jewel.name + "!");
                mapController.UpdateRocks(minedBlocks);
                mapController.UpdateJewel(idxJewel);
                ++minedBlocks;

                elapsedTime = 0;
                mapController.OnMiningEnd();
                playerController.ChangeState(PlayerState.MovingState);
                cameraController.Shake();
            }
        }
    }

    public void ClearMining()
    {
        minedBlocks = 0;
        elapsedTime = 0;
        targetTime = 0;
        playerController.ChangeState(PlayerState.IdleState);
    }

    private void OnEnable()
    {
        iaMine.Enable();
    }

    private void OnDisable()
    {
        iaMine.Disable();
    }
}
