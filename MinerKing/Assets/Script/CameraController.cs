using UnityEngine;

// temporary
using UnityEngine.InputSystem;
//

public class CameraController : MonoBehaviour
{
    public int idxMap = 0;
    public int minedBlocks = 0;
    public float yOffset = 0.14f;
    public int mapWidth = 121;

    public InputAction tmpInputProceedAction;
    public InputAction tmpInputMap1Action;
    public InputAction tmpInputMap2Action;
    public InputAction tmpInputMap3Action;
    public InputAction tmpInputMap4Action;
    public InputAction tmpInputMap5Action;

    private GameObject curMapObject;
    private int lastIdxMap;

    void Start()
    {
        lastIdxMap = idxMap;
        SyncMapObjectWithIndex();
        foreach (Transform child in curMapObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        curMapObject.GetComponent<MapController>().GenerateRocks();
    }

    void LateUpdate()
    {
        int xOffset = -(minedBlocks / mapWidth) * mapWidth;
        Vector3 curPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 targetPos = new Vector3(minedBlocks + xOffset, curPos.y, curPos.z);

        float epsilon = 0.001f;

        if (curPos.x > targetPos.x + epsilon)
        {
            curPos.x -= mapWidth;
        }
        Vector3 velocity = Vector3.zero;

        if (lastIdxMap == idxMap)
        {
            transform.position = Vector3.SmoothDamp(
                curPos, targetPos, ref velocity, 0.02f
            );
        }
        else
        {
            targetPos.y = idxMap * -15.0f + yOffset;
            transform.position = targetPos;
        }

        lastIdxMap = idxMap;
    }

    void Update()
    {
        if (tmpInputProceedAction.WasPressedThisFrame())
        {
            ++minedBlocks;
        }

        bool wasMapChanged = false;

        if (tmpInputMap1Action.WasPressedThisFrame())
        {
            idxMap = 0;
            wasMapChanged = lastIdxMap != idxMap;
        }
        else if (tmpInputMap2Action.WasPressedThisFrame())
        {
            idxMap = 1;
            wasMapChanged = lastIdxMap != idxMap;
        }
        else if (tmpInputMap3Action.WasPressedThisFrame())
        {
            idxMap = 2;
            wasMapChanged = lastIdxMap != idxMap;
        }
        else if (tmpInputMap4Action.WasPressedThisFrame())
        {
            idxMap = 3;
            wasMapChanged = lastIdxMap != idxMap;
        }
        else if (tmpInputMap5Action.WasPressedThisFrame())
        {
            idxMap = 4;
            wasMapChanged = lastIdxMap != idxMap;
        }

        if (wasMapChanged)
        {
            curMapObject.GetComponent<MapController>().ChangeMap(idxMap);
            SyncMapObjectWithIndex();
        }
    }

    void SyncMapObjectWithIndex()
    {
        curMapObject = GameObject.Find("PMap" + (idxMap + 1));
    }

    void OnEnable()
    {
        tmpInputProceedAction.Enable();
        tmpInputMap1Action.Enable();
        tmpInputMap2Action.Enable();
        tmpInputMap3Action.Enable();
        tmpInputMap4Action.Enable();
        tmpInputMap5Action.Enable();
    }


    void OnDisable()
    {
        tmpInputProceedAction.Disable();
        tmpInputMap1Action.Disable();
        tmpInputMap2Action.Disable();
        tmpInputMap3Action.Disable();
        tmpInputMap4Action.Disable();
        tmpInputMap5Action.Disable();
    }
}
