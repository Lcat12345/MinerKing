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
    public InputAction tmpInputMapChangeAction;
    private int lastIdxMap;

    void Start()
    {
        lastIdxMap = idxMap;
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

        if (tmpInputMapChangeAction.WasPressedThisFrame())
        {
            ++idxMap;
        }
    }

    void OnEnable()
    {
        tmpInputProceedAction.Enable();
        tmpInputMapChangeAction.Enable();
    }


    void OnDisable()
    {
        tmpInputProceedAction.Disable();
        tmpInputMapChangeAction.Disable();
    }
}
