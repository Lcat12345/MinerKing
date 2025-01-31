using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageData : SingletonLazy<StageData>
{
    // { (map, stage): probabilities of jewels }
    // map and stage are zero-based here
    private Dictionary<(int, int), Dictionary<Jewels, float>> stageData;

    public StageData()
    {
        stageData = new Dictionary<(int, int), Dictionary<Jewels, float>>();
        Dictionary<Jewels, float> probabilities = new Dictionary<Jewels, float>();

        // 1-1
        stageData.Add((0, 0), new Dictionary<Jewels, float>());
        stageData.TryGetValue((0, 0), out probabilities);
        probabilities.Add(Jewels.Ruby, 80.0f);
        probabilities.Add(Jewels.Sapphire, 15.0f);
        probabilities.Add(Jewels.Emerald, 5.0f);

        stageData.TryGetValue((0, 0), out probabilities);
        foreach (KeyValuePair<Jewels, float> pair in probabilities)
        {
            Debug.Log(pair.Key + ": " + pair.Value);
        }


        // 1-2
        stageData.Add((0, 1), new Dictionary<Jewels, float>());
        stageData.TryGetValue((0, 1), out probabilities);
        probabilities.Add(Jewels.Ruby, 30.0f);
        probabilities.Add(Jewels.Sapphire, 35.0f);
        probabilities.Add(Jewels.Emerald, 25.0f);
        probabilities.Add(Jewels.Topaz, 8.0f);
        probabilities.Add(Jewels.Amethyst, 2.0f);

        // 1-3
        stageData.Add((0, 2), new Dictionary<Jewels, float>());
        stageData.TryGetValue((0, 2), out probabilities);
        probabilities.Add(Jewels.Emerald, 30.0f);
        probabilities.Add(Jewels.Topaz, 35.0f);
        probabilities.Add(Jewels.Amethyst, 25.0f);
        probabilities.Add(Jewels.Garnet, 8.0f);
        probabilities.Add(Jewels.Opal, 2.0f);

        // 1-4
        stageData.Add((0, 3), new Dictionary<Jewels, float>());
        stageData.TryGetValue((0, 3), out probabilities);
        probabilities.Add(Jewels.Garnet, 45.0f);
        probabilities.Add(Jewels.Opal, 30.0f);
        probabilities.Add(Jewels.Turquoise, 18.0f);
        probabilities.Add(Jewels.Peridot, 7.0f);

        // 1-5
        stageData.Add((0, 4), new Dictionary<Jewels, float>());
        stageData.TryGetValue((0, 4), out probabilities);
        probabilities.Add(Jewels.Turquoise, 50.0f);
        probabilities.Add(Jewels.Peridot, 25.0f);
        probabilities.Add(Jewels.Tanzanite, 12.0f);
        probabilities.Add(Jewels.Spinel, 8.0f);
        probabilities.Add(Jewels.Alexandrite, 4.0f);
        probabilities.Add(Jewels.Aquamarine, 1.0f);

        // 1-6
        stageData.Add((0, 4), new Dictionary<Jewels, float>());
        stageData.TryGetValue((0, 4), out probabilities);
        probabilities.Add(Jewels.Peridot, 36.0f);
        probabilities.Add(Jewels.Tanzanite, 27.0f);
        probabilities.Add(Jewels.Spinel, 19.0f);
        probabilities.Add(Jewels.Alexandrite, 12.0f);
        probabilities.Add(Jewels.Aquamarine, 5.0f);
        probabilities.Add(Jewels.Morganite, 1.0f);

        // 2-1
        stageData.Add((1, 0), new Dictionary<Jewels, float>());
        stageData.TryGetValue((1, 0), out probabilities);
        probabilities.Add(Jewels.Alexandrite, 70.0f);
        probabilities.Add(Jewels.Aquamarine, 18.0f);
        probabilities.Add(Jewels.Morganite, 9.0f);
        probabilities.Add(Jewels.Rhodolite, 3.0f);

        // 2-2
        stageData.Add((1, 1), new Dictionary<Jewels, float>());
        stageData.TryGetValue((1, 1), out probabilities);
        probabilities.Add(Jewels.Alexandrite, 26.0f);
        probabilities.Add(Jewels.Aquamarine, 34.0f);
        probabilities.Add(Jewels.Morganite, 22.0f);
        probabilities.Add(Jewels.Rhodolite, 12.0f);
        probabilities.Add(Jewels.Tsavorite, 5.0f);
        probabilities.Add(Jewels.Jadeite, 1.0f);

        // 2-3
        stageData.Add((1, 2), new Dictionary<Jewels, float>());
        stageData.TryGetValue((1, 2), out probabilities);
        probabilities.Add(Jewels.Morganite, 35.0f);
        probabilities.Add(Jewels.Rhodolite, 25.0f);
        probabilities.Add(Jewels.Tsavorite, 20.0f);
        probabilities.Add(Jewels.Jadeite, 15.0f);
        probabilities.Add(Jewels.Labradorite, 5.0f);

        // 2-4
        stageData.Add((1, 3), new Dictionary<Jewels, float>());
        stageData.TryGetValue((1, 3), out probabilities);
        probabilities.Add(Jewels.Jadeite, 27.0f);
        probabilities.Add(Jewels.Labradorite, 33.0f);
        probabilities.Add(Jewels.Moonstone, 24.0f);
        probabilities.Add(Jewels.Bloodstone, 14.0f);
        probabilities.Add(Jewels.Diamond, 2.0f);

        // 2-5
        stageData.Add((1, 4), new Dictionary<Jewels, float>());
        stageData.TryGetValue((1, 4), out probabilities);
        probabilities.Add(Jewels.Moonstone, 32.0f);
        probabilities.Add(Jewels.Bloodstone, 26.0f);
        probabilities.Add(Jewels.Diamond, 18.0f);
        probabilities.Add(Jewels.LapisLazuli, 14.0f);
        probabilities.Add(Jewels.Onyx, 6.0f);
        probabilities.Add(Jewels.Moldavite, 4.0f);

        // 2-6
        stageData.Add((1, 5), new Dictionary<Jewels, float>());
        stageData.TryGetValue((1, 5), out probabilities);
        probabilities.Add(Jewels.Bloodstone, 22.0f);
        probabilities.Add(Jewels.Diamond, 36.0f);
        probabilities.Add(Jewels.LapisLazuli, 28.0f);
        probabilities.Add(Jewels.Onyx, 9.0f);
        probabilities.Add(Jewels.Moldavite, 4.0f);
        probabilities.Add(Jewels.Iolite, 1.0f);

        // 3-1
        stageData.Add((2, 0), new Dictionary<Jewels, float>());
        stageData.TryGetValue((2, 0), out probabilities);
        probabilities.Add(Jewels.Onyx, 85.0f);
        probabilities.Add(Jewels.Moldavite, 9.0f);
        probabilities.Add(Jewels.Iolite, 5.0f);
        probabilities.Add(Jewels.Citrine, 1.0f);

        // 3-2
        stageData.Add((2, 1), new Dictionary<Jewels, float>());
        stageData.TryGetValue((2, 1), out probabilities);
        probabilities.Add(Jewels.Onyx, 45.0f);
        probabilities.Add(Jewels.Moldavite, 26.0f);
        probabilities.Add(Jewels.Iolite, 19.0f);
        probabilities.Add(Jewels.Citrine, 6.0f);
        probabilities.Add(Jewels.Ametrine, 3.0f);
        probabilities.Add(Jewels.Coral, 1.0f);

        // 3-3
        stageData.Add((2, 2), new Dictionary<Jewels, float>());
        stageData.TryGetValue((2, 2), out probabilities);
        probabilities.Add(Jewels.Iolite, 24.0f);
        probabilities.Add(Jewels.Citrine, 28.0f);
        probabilities.Add(Jewels.Ametrine, 25.0f);
        probabilities.Add(Jewels.Coral, 15.0f);
        probabilities.Add(Jewels.Amber, 8.0f);

        // 3-4
        stageData.Add((2, 3), new Dictionary<Jewels, float>());
        stageData.TryGetValue((2, 3), out probabilities);
        probabilities.Add(Jewels.Coral, 42.0f);
        probabilities.Add(Jewels.Amber, 36.0f);
        probabilities.Add(Jewels.Chrysoberyl, 17.0f);
        probabilities.Add(Jewels.Carnelian, 5.0f);

        // 3-5
        stageData.Add((2, 4), new Dictionary<Jewels, float>());
        stageData.TryGetValue((2, 4), out probabilities);
        probabilities.Add(Jewels.Amber, 24.0f);
        probabilities.Add(Jewels.Chrysoberyl, 30.0f);
        probabilities.Add(Jewels.Carnelian, 21.0f);
        probabilities.Add(Jewels.Agate, 15.0f);
        probabilities.Add(Jewels.Kyanite, 10.0f);

        // 3-6
        stageData.Add((2, 5), new Dictionary<Jewels, float>());
        stageData.TryGetValue((2, 5), out probabilities);
        probabilities.Add(Jewels.Carnelian, 21.0f);
        probabilities.Add(Jewels.Agate, 39.0f);
        probabilities.Add(Jewels.Kyanite, 22.0f);
        probabilities.Add(Jewels.Andesine, 14.0f);
        probabilities.Add(Jewels.Hematite, 4.0f);

        // 3-7
        stageData.Add((2, 6), new Dictionary<Jewels, float>());
        stageData.TryGetValue((2, 6), out probabilities);
        probabilities.Add(Jewels.Carnelian, 4.0f);
        probabilities.Add(Jewels.Agate, 26.0f);
        probabilities.Add(Jewels.Kyanite, 35.0f);
        probabilities.Add(Jewels.Andesine, 24.0f);
        probabilities.Add(Jewels.Hematite, 8.0f);
        probabilities.Add(Jewels.Sugilite, 3.0f);

        // 4-1
        stageData.Add((3, 0), new Dictionary<Jewels, float>());
        stageData.TryGetValue((3, 0), out probabilities);
        probabilities.Add(Jewels.Kyanite, 65.0f);
        probabilities.Add(Jewels.Andesine, 18.0f);
        probabilities.Add(Jewels.Hematite, 10.0f);
        probabilities.Add(Jewels.Sugilite, 7.0f);

        // 4-2
        stageData.Add((3, 1), new Dictionary<Jewels, float>());
        stageData.TryGetValue((3, 1), out probabilities);
        probabilities.Add(Jewels.Kyanite, 25.0f);
        probabilities.Add(Jewels.Andesine, 34.0f);
        probabilities.Add(Jewels.Hematite, 21.0f);
        probabilities.Add(Jewels.Sugilite, 16.0f);
        probabilities.Add(Jewels.Malachite, 3.0f);
        probabilities.Add(Jewels.Charoite, 1.0f);

        // 4-3
        stageData.Add((3, 2), new Dictionary<Jewels, float>());
        stageData.TryGetValue((3, 2), out probabilities);
        probabilities.Add(Jewels.Sugilite, 30.0f);
        probabilities.Add(Jewels.Malachite, 25.0f);
        probabilities.Add(Jewels.Charoite, 18.0f);
        probabilities.Add(Jewels.ZebraJasper, 11.0f);
        probabilities.Add(Jewels.PinkTourmaline, 6.0f);

        // 4-4
        stageData.Add((3, 3), new Dictionary<Jewels, float>());
        stageData.TryGetValue((3, 3), out probabilities);
        probabilities.Add(Jewels.Charoite, 26.0f);
        probabilities.Add(Jewels.ZebraJasper, 20.0f);
        probabilities.Add(Jewels.PinkTourmaline, 24.0f);
        probabilities.Add(Jewels.BlueTourmaline, 16.0f);
        probabilities.Add(Jewels.BlueJasper, 14.0f);

        // 4-5
        stageData.Add((3, 4), new Dictionary<Jewels, float>());
        stageData.TryGetValue((3, 4), out probabilities);
        probabilities.Add(Jewels.BlueTourmaline, 38.0f);
        probabilities.Add(Jewels.BlueJasper, 26.0f);
        probabilities.Add(Jewels.Unakite, 22.0f);
        probabilities.Add(Jewels.TigerEye, 14.0f);

        // 4-6
        stageData.Add((3, 5), new Dictionary<Jewels, float>());
        stageData.TryGetValue((3, 5), out probabilities);
        probabilities.Add(Jewels.Unakite, 58.0f);
        probabilities.Add(Jewels.TigerEye, 39.0f);
        probabilities.Add(Jewels.Howlite, 3.0f);

        // 4-7
        stageData.Add((3, 6), new Dictionary<Jewels, float>());
        stageData.TryGetValue((3, 6), out probabilities);
        probabilities.Add(Jewels.Unakite, 22.0f);
        probabilities.Add(Jewels.TigerEye, 56.0f);
        probabilities.Add(Jewels.Howlite, 22.0f);

        // 5-1
        stageData.Add((4, 0), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 0), out probabilities);
        probabilities.Add(Jewels.Unakite, 11.0f);
        probabilities.Add(Jewels.TigerEye, 84.0f);
        probabilities.Add(Jewels.Howlite, 4.0f);
        probabilities.Add(Jewels.Rhodochrosite, 1.0f);

        // 5-2
        stageData.Add((4, 1), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 1), out probabilities);
        probabilities.Add(Jewels.TigerEye, 28.0f);
        probabilities.Add(Jewels.Howlite, 36.0f);
        probabilities.Add(Jewels.Rhodochrosite, 21.0f);
        probabilities.Add(Jewels.Azurite, 15.0f);

        // 5-3
        stageData.Add((4, 2), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 2), out probabilities);
        probabilities.Add(Jewels.Howlite, 18.0f);
        probabilities.Add(Jewels.Rhodochrosite, 34.0f);
        probabilities.Add(Jewels.Azurite, 27.0f);
        probabilities.Add(Jewels.Fluorite, 18.0f);
        probabilities.Add(Jewels.Scapolite, 3.0f);

        // 5-4
        stageData.Add((4, 3), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 3), out probabilities);
        probabilities.Add(Jewels.Rhodochrosite, 20.0f);
        probabilities.Add(Jewels.Azurite, 26.0f);
        probabilities.Add(Jewels.Fluorite, 35.0f);
        probabilities.Add(Jewels.Scapolite, 18.0f);
        probabilities.Add(Jewels.PhoenixTear, 1.0f);

        // 5-5
        stageData.Add((4, 4), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 4), out probabilities);
        probabilities.Add(Jewels.Fluorite, 14.0f);
        probabilities.Add(Jewels.Scapolite, 32.0f);
        probabilities.Add(Jewels.PhoenixTear, 29.0f);
        probabilities.Add(Jewels.DragonStone, 25.0f);

        // 5-6
        stageData.Add((4, 5), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 5), out probabilities);
        probabilities.Add(Jewels.PhoenixTear, 21.0f);
        probabilities.Add(Jewels.DragonStone, 34.0f);
        probabilities.Add(Jewels.MoonlightGem, 26.0f);
        probabilities.Add(Jewels.ManaCrystal, 12.0f);
        probabilities.Add(Jewels.InfinityStone, 7.0f);

        // 5-7
        stageData.Add((4, 6), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 6), out probabilities);
        probabilities.Add(Jewels.MoonlightGem, 38.0f);
        probabilities.Add(Jewels.ManaCrystal, 24.0f);
        probabilities.Add(Jewels.InfinityStone, 18.0f);
        probabilities.Add(Jewels.Orichalcum, 15.0f);
        probabilities.Add(Jewels.HeartOfSolaris, 5.0f);

        // 5-8
        stageData.Add((4, 7), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 7), out probabilities);
        probabilities.Add(Jewels.ManaCrystal, 16.0f);
        probabilities.Add(Jewels.InfinityStone, 33.0f);
        probabilities.Add(Jewels.Orichalcum, 31.0f);
        probabilities.Add(Jewels.HeartOfSolaris, 20.0f);

        // 5-9
        stageData.Add((4, 8), new Dictionary<Jewels, float>());
        stageData.TryGetValue((4, 8), out probabilities);
        probabilities.Add(Jewels.InfinityStone, 12.0f);
        probabilities.Add(Jewels.Orichalcum, 36.0f);
        probabilities.Add(Jewels.HeartOfSolaris, 52.0f);
    }
}

public class MapController : MonoBehaviour
{
    public int idxMap;
    public CameraController cc;
    public GameObject rockPrefab; // 바위 프리팹 연결용

    private List<Jewel> jewels;


    public void ChangeMap(int idx)
    {
        GameObject mapParent = GameObject.Find("PMap" + (idx + 1));

        foreach (Transform child in mapParent.transform)
        {
            child.gameObject.SetActive(true);
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        mapParent.GetComponent<MapController>().GenerateRocks();

        cc.minedBlocks = 0;
        cc.idxMap = idx;
    }

    public void GenerateRocks()
    {
        // 현재 맵과 그 클론을 찾음
        GameObject map = GameObject.Find("Map" + (idxMap + 1));
        GameObject mapClone = GameObject.Find("Map" + (idxMap + 1) + "Clone");

        // 맵들을 저장
        List<GameObject> maps = new List<GameObject> { map, mapClone };

        // 각 맵에 대해 바위 생성
        foreach (GameObject targetMap in maps)
        {
            if (targetMap == null) continue;

            // Rocks 오브젝트 찾기
            Transform rocksParent = targetMap.transform.Find("Rocks");
            if (rocksParent == null)
            {
                Debug.LogWarning($"Rocks object not found in {targetMap.name}");
                continue;
            }

            // 범위 지정
            Vector2 leftTop = new Vector2(-5, -3);
            Vector2 rightBottom = new Vector2(115, -5);

            // 바위 배치
            for (int x = Mathf.FloorToInt(leftTop.x); x <= Mathf.FloorToInt(rightBottom.x); x++)
            {
                for (int y = Mathf.FloorToInt(leftTop.y); y >= Mathf.FloorToInt(rightBottom.y); y--)
                {
                    // 바위 생성 및 부모 설정
                    GameObject rock = Instantiate(rockPrefab, rocksParent);
                    rock.transform.position = new Vector3(x, y - idxMap * 15.0f, 0);
                }
            }
        }
    }
}