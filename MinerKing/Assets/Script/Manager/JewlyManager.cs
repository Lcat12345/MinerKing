using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewlyManager : MonoBehaviour
{
    private Dictionary<string, GameObject> minedJewels;
    // 생성된 순서를 저장할 리스트
    private List<GameObject> jewelInstances = new List<GameObject>();
    private Mining mining;
    private MapController mc;
    int maxDroppedJewly = 100000;

    // 제거 코루틴 중복 실행을 막기 위한 플래그
    private bool isRemovingJewels = false;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null)
            return;
        mining = player.GetComponent<Mining>();
        mc = mining.mapController;

        minedJewels = new Dictionary<string, GameObject>();

        {
            GameObject go = Resources.Load<GameObject>("Jewels/Prefabs/Ruby");
            minedJewels.Add("루비", go);
        }
        {
            GameObject go = Resources.Load<GameObject>("Jewels/Prefabs/Sapphire");
            minedJewels.Add("사파이어", go);
        }
        {
            GameObject go = Resources.Load<GameObject>("Jewels/Prefabs/Emerald");
            minedJewels.Add("에메랄드", go);
        }
    }

    public void GenerateJewly(string name)
    {
        ulong minedBlocks = mining.MinedBlocks;
        int mapWidth = mc.mapWidth;
        int idxMap = mc.idxMap;

        int idxRock = (int)((minedBlocks + 4ul) % (ulong)(mapWidth));
        bool isOnCloneMap = (int)(minedBlocks % (ulong)(mapWidth)) > idxRock;

        GameObject go = Instantiate(minedJewels[name], this.transform);
        // 기존 애니메이션(땅 위로 올라가는) 스크립트 추가
        go.AddComponent<JewlyAnimation>();

        if (!isOnCloneMap)
            go.transform.position = new Vector3(idxRock - 4.5f, -3.8f, 0.0f);
        else
            go.transform.position = new Vector3(idxRock - 4.5f + mapWidth, -3.8f, 0.0f);

        // 생성된 순서대로 리스트에 추가
        jewelInstances.Add(go);

        // 만약 보석 개수가 maxDroppedJewly를 초과하면 하나씩 제거하는 코루틴을 실행합니다.
        if (jewelInstances.Count > maxDroppedJewly && !isRemovingJewels)
        {
            StartCoroutine(RemoveExcessJewels());
        }
    }

    private IEnumerator RemoveExcessJewels()
    {
        isRemovingJewels = true;
        // 리스트에 남은 보석이 maxDroppedJewly보다 많은 동안 반복
        while (jewelInstances.Count > maxDroppedJewly)
        {
            // 리스트의 첫 번째 보석(가장 먼저 생성된 보석)을 대상으로 함
            GameObject jewel = jewelInstances[0];
            // 이미 삭제되었거나 null이면 리스트에서 제거하고 다음으로 넘어감
            if (jewel == null)
            {
                jewelInstances.RemoveAt(0);
                continue;
            }
            // 보석에 붙은 애니메이션 스크립트에 인벤토리 이동 애니메이션을 요청합니다.
            JewlyAnimation anim = jewel.GetComponent<JewlyAnimation>();
            if (anim != null)
            {
                anim.StartInventoryAnimation();
            }
            // 리스트에서 제거합니다.
            jewelInstances.RemoveAt(0);

            // 애니메이션이 진행될 시간을 기다립니다.
            // (예를 들어, 0.2초 대기 – 실제 애니메이션 시간에 맞게 조절)
            yield return new WaitForSeconds(0.2f);
        }
        isRemovingJewels = false;
    }
}
