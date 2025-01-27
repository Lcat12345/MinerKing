using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
    public int idxMap;
    public CameraController cc;
    public GameObject rockPrefab; // 바위 프리팹 연결용


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