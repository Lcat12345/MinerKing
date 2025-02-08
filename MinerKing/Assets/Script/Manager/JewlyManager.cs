using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JewlyManager : MonoBehaviour
{
    public PhysicsMaterial2D physMat;
    public SFXManager sfxManager;
    public InputAction iaCollect;
    public GameObject gameUI;
    public GameObject inventoryIcon;
    public Camera cam;

    private Dictionary<string, GameObject> jewelMap;
    // 생성된 순서를 저장할 리스트
    private List<GameObject> jewelInstances = new List<GameObject>();
    private Mining mining;
    public int maxShownJewelCnt = 160;

    // 제거 코루틴 중복 실행을 막기 위한 플래그
    private bool isRemovingJewels = false;

    private bool physicalAnimation = true;      // temporary
    public int maxCollisionCnt = 8;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null)
            return;
        mining = player.GetComponent<Mining>();

        jewelMap = new Dictionary<string, GameObject>();

        jewelMap.Add("루비", Resources.Load<GameObject>("Jewels/Prefabs/Ruby"));
        jewelMap.Add("사파이어", Resources.Load<GameObject>("Jewels/Prefabs/Sapphire"));
        jewelMap.Add("에메랄드", Resources.Load<GameObject>("Jewels/Prefabs/Emerald"));
        jewelMap.Add("토파즈", Resources.Load<GameObject>("Jewels/Prefabs/Topaz"));
        jewelMap.Add("아메시스트", Resources.Load<GameObject>("Jewels/Prefabs/Amethyst"));
        jewelMap.Add("가넷", Resources.Load<GameObject>("Jewels/Prefabs/Garnet"));
        jewelMap.Add("오팔", Resources.Load<GameObject>("Jewels/Prefabs/Opal"));
        jewelMap.Add("터키석", Resources.Load<GameObject>("Jewels/Prefabs/Turquoise"));
        jewelMap.Add("페리도트", Resources.Load<GameObject>("Jewels/Prefabs/Peridot"));
        jewelMap.Add("탄자나이트", Resources.Load<GameObject>("Jewels/Prefabs/Tanzanite"));
        jewelMap.Add("스피넬", Resources.Load<GameObject>("Jewels/Prefabs/Spinel"));
        jewelMap.Add("알렉산드라이트", Resources.Load<GameObject>("Jewels/Prefabs/Alexandrite"));
        jewelMap.Add("아쿠아마린", Resources.Load<GameObject>("Jewels/Prefabs/Aquamarine"));
        jewelMap.Add("모건나이트", Resources.Load<GameObject>("Jewels/Prefabs/Morganite"));
        jewelMap.Add("로돌라이트", Resources.Load<GameObject>("Jewels/Prefabs/Rhodolite"));
        jewelMap.Add("차보라이트", Resources.Load<GameObject>("Jewels/Prefabs/Tsavorite"));
        jewelMap.Add("제다이트", Resources.Load<GameObject>("Jewels/Prefabs/Jadeite"));
        jewelMap.Add("래브라도라이트", Resources.Load<GameObject>("Jewels/Prefabs/Labradorite"));
        jewelMap.Add("문스톤", Resources.Load<GameObject>("Jewels/Prefabs/Moonstone"));
        jewelMap.Add("혈석", Resources.Load<GameObject>("Jewels/Prefabs/Bloodstone"));
        jewelMap.Add("다이아몬드", Resources.Load<GameObject>("Jewels/Prefabs/Diamond"));
        jewelMap.Add("라피스라줄리", Resources.Load<GameObject>("Jewels/Prefabs/LapisLazuli"));
        jewelMap.Add("사옥", Resources.Load<GameObject>("Jewels/Prefabs/Onyx"));
        jewelMap.Add("몰다바이트", Resources.Load<GameObject>("Jewels/Prefabs/Moldavite"));
        jewelMap.Add("아이올라이트", Resources.Load<GameObject>("Jewels/Prefabs/Iolite"));
        jewelMap.Add("시트린", Resources.Load<GameObject>("Jewels/Prefabs/Citrine"));
        jewelMap.Add("자수정", Resources.Load<GameObject>("Jewels/Prefabs/Ametrine"));
        jewelMap.Add("코랄", Resources.Load<GameObject>("Jewels/Prefabs/Coral"));
        jewelMap.Add("앰버", Resources.Load<GameObject>("Jewels/Prefabs/Amber"));
        jewelMap.Add("크리소베릴", Resources.Load<GameObject>("Jewels/Prefabs/Chrysoberyl"));
        jewelMap.Add("주홍석", Resources.Load<GameObject>("Jewels/Prefabs/Carnelian"));
        jewelMap.Add("마노", Resources.Load<GameObject>("Jewels/Prefabs/Agate"));
        jewelMap.Add("카이아나이트", Resources.Load<GameObject>("Jewels/Prefabs/Kyanite"));
        jewelMap.Add("안데신", Resources.Load<GameObject>("Jewels/Prefabs/Andesine"));
        jewelMap.Add("헴타이트", Resources.Load<GameObject>("Jewels/Prefabs/Hematite"));
        jewelMap.Add("슈가라이트", Resources.Load<GameObject>("Jewels/Prefabs/Sugilite"));
        jewelMap.Add("말라카이트", Resources.Load<GameObject>("Jewels/Prefabs/Malachite"));
        jewelMap.Add("차로이트", Resources.Load<GameObject>("Jewels/Prefabs/Charoite"));
        jewelMap.Add("재브라 재스퍼", Resources.Load<GameObject>("Jewels/Prefabs/ZebraJasper"));
        jewelMap.Add("핑크 투르말린", Resources.Load<GameObject>("Jewels/Prefabs/PinkTourmaline"));
        jewelMap.Add("블루 투르말린", Resources.Load<GameObject>("Jewels/Prefabs/BlueTourmaline"));
        jewelMap.Add("블루 재스퍼", Resources.Load<GameObject>("Jewels/Prefabs/BlueJasper"));
        jewelMap.Add("유문석", Resources.Load<GameObject>("Jewels/Prefabs/Unakite"));
        jewelMap.Add("타이거 아이", Resources.Load<GameObject>("Jewels/Prefabs/TigerEye"));
        jewelMap.Add("하울라이트", Resources.Load<GameObject>("Jewels/Prefabs/Howlite"));
        jewelMap.Add("로도크로사이트", Resources.Load<GameObject>("Jewels/Prefabs/Rhodochrosite"));
        jewelMap.Add("아주라이트", Resources.Load<GameObject>("Jewels/Prefabs/Azurite"));
        jewelMap.Add("플루오라이트", Resources.Load<GameObject>("Jewels/Prefabs/Fluorite"));
        jewelMap.Add("스쿠폴라이트", Resources.Load<GameObject>("Jewels/Prefabs/Scapolite"));
        jewelMap.Add("피닉스의 눈물", Resources.Load<GameObject>("Jewels/Prefabs/PhoenixTear"));
        jewelMap.Add("드래곤스톤", Resources.Load<GameObject>("Jewels/Prefabs/DragonStone"));
        jewelMap.Add("월광석", Resources.Load<GameObject>("Jewels/Prefabs/MoonlightGem"));
        jewelMap.Add("마나 크리스탈", Resources.Load<GameObject>("Jewels/Prefabs/ManaCrystal"));
        jewelMap.Add("인피니티 스톤", Resources.Load<GameObject>("Jewels/Prefabs/InfinityStone"));
        jewelMap.Add("오리칼쿰", Resources.Load<GameObject>("Jewels/Prefabs/Orichalcum"));
        jewelMap.Add("솔라리스의 심장", Resources.Load<GameObject>("Jewels/Prefabs/HeartOfSolaris"));
    }

    public void GenerateJewly(string name, float speed)
    {
        if (jewelInstances.Count == maxShownJewelCnt)
        {
            Destroy(jewelInstances[0]);
            jewelInstances.RemoveAt(0);
        }

        MapController mapController = mining.mapController;
        if (mapController == null)
        {
            Debug.LogWarning("mining script's map controller member is null.");
            return;
        }

        ulong minedBlocks = mining.MinedBlocks;
        int mapWidth = mapController.mapWidth;
        int idxMap = mapController.idxMap;

        int idxRock = (int)((minedBlocks + 4ul) % (ulong)(mapWidth));
        bool isOnCloneMap = (int)(minedBlocks % (ulong)(mapWidth)) > idxRock;

        GameObject go = Instantiate(jewelMap[name], this.transform);
        // 기존 애니메이션(땅 위로 올라가는) 스크립트 추가
        JewlyAnimation ja = go.AddComponent<JewlyAnimation>() as JewlyAnimation;
        ja.speed = speed;
        ja.mapController = mapController;
        ja.inventoryPosition = inventoryIcon.transform.position;
        ja.sfxManager = sfxManager;

        if (physicalAnimation)
        {
            ja.maxCollisionCnt = maxCollisionCnt;
            BoxCollider2D collider = go.AddComponent<BoxCollider2D>();

            collider.sharedMaterial = physMat;
            collider.size = new Vector2(
                collider.size.x * 0.65f,
                collider.size.y * 0.3f
            );

            Rigidbody2D rb = go.AddComponent<Rigidbody2D>() as Rigidbody2D;

            rb.gravityScale = 1.0f;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        if (!isOnCloneMap)
            go.transform.position = new Vector3(idxRock - 4.5f, -3.8f + idxMap * -15.0f, 0.0f);
        else
            go.transform.position = new Vector3(idxRock - 4.5f + mapWidth, -3.8f + idxMap * -15.0f, 0.0f);

        // 생성된 순서대로 리스트에 추가
        jewelInstances.Add(go);

        //// 만약 보석 개수가 maxDroppedJewly를 초과하면 하나씩 제거하는 코루틴을 실행합니다.
        //if (jewelInstances.Count > maxDroppedJewly && !isRemovingJewels)
        //{
        //    StartCoroutine(RemoveExcessJewels());
        //}
    }

    //private IEnumerator RemoveExcessJewels()
    //{
    //    isRemovingJewels = true;
    //    // 리스트에 남은 보석이 maxDroppedJewly보다 많은 동안 반복
    //    while (jewelInstances.Count > maxDroppedJewly)
    //    {
    //        // 리스트의 첫 번째 보석(가장 먼저 생성된 보석)을 대상으로 함
    //        GameObject jewel = jewelInstances[0];
    //        // 이미 삭제되었거나 null이면 리스트에서 제거하고 다음으로 넘어감
    //        if (jewel == null)
    //        {
    //            jewelInstances.RemoveAt(0);
    //            continue;
    //        }
    //        // 보석에 붙은 애니메이션 스크립트에 인벤토리 이동 애니메이션을 요청합니다.
    //        JewlyAnimation anim = jewel.GetComponent<JewlyAnimation>();
    //        if (anim != null)
    //        {
    //            anim.StartInventoryAnimation();
    //        }
    //        // 리스트에서 제거합니다.
    //        jewelInstances.RemoveAt(0);

    //        // 애니메이션이 진행될 시간을 기다립니다.
    //        // (예를 들어, 0.2초 대기 – 실제 애니메이션 시간에 맞게 조절)
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //    isRemovingJewels = false;
    //}

    private void Update()
    {
        if (iaCollect.WasPressedThisFrame())
        {
            CollectJewels();
        }
    }

    public void CollectJewels()
    {
        for (int i = 0; i < jewelInstances.Count; ++i)
        {
            jewelInstances[i].GetComponent<JewlyAnimation>().StartInventoryAnimation();

            Vector3 normalizedPos = cam.WorldToViewportPoint(jewelInstances[i].transform.position);

            jewelInstances[i].transform.SetParent(gameUI.transform);

            RectTransform rt = jewelInstances[i].AddComponent(typeof(RectTransform)) as RectTransform;
            rt.anchorMin = rt.anchorMax = normalizedPos;
            rt.anchoredPosition = Vector2.zero;

            RectTransform uiRect = gameUI.GetComponent<RectTransform>();
            float uiWidth = uiRect.rect.width;
            float uiHeight = uiRect.rect.height;

            float vpHeight = cam.orthographicSize * 2;
            float vpWidth = vpHeight * cam.aspect;

            float scaleX = uiWidth / vpWidth;
            float scaleY = uiHeight / vpHeight;
            rt.localScale = new Vector3(scaleX * 0.5f, scaleY * 0.5f, 1.0f);

            jewelInstances[i].AddComponent(typeof(CanvasRenderer));

            SpriteRenderer sr = jewelInstances[i].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Image img = jewelInstances[i].AddComponent<Image>();
                img.sprite = sr.sprite; // 기존 Sprite 적용

                Destroy(sr);
            }
        }

        jewelInstances.Clear();
        mining.ReflectMinedJewelsWithDelay();
    }

    private void OnEnable()
    {
        iaCollect.Enable();
    }

    private void OnDisable()
    {
        iaCollect.Disable();
    }
}
