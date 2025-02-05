using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    IdleState, MiningState, MovingState
}

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator weaponAnimator;
    public GameObject mining;
    public CameraController cameraController;
    public InputAction tmpInputMap1Action;
    public InputAction tmpInputMap2Action;
    public InputAction tmpInputMap3Action;
    public InputAction tmpInputMap4Action;
    public InputAction tmpInputMap5Action;

    public Pickaxes curPickaxe;
    private Vector3 velocity;
    public int idxMap = 0;
    public Vector2 offset = new Vector2(-1.6f, -3.4f);
    public bool start = false;

    private GameObject curMapObject;
    private int lastIdxMap;

    private uint statMining = 0;
    private uint statMoving = 200;
    private PlayerState curState;

    public PlayerState State { get { return curState; } }

    void Start()
    {
        curState = PlayerState.IdleState;
        lastIdxMap = idxMap;
        SyncMapObjectWithIndex();
        foreach (Transform child in curMapObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        cameraController.Attach(gameObject);
        cameraController.SetMapController( curMapObject.GetComponent<MapController>() );
        velocity = Vector3.zero;
        transform.position = new Vector3(offset.x, offset.y, 0.0f);
    }

    private void Update()
    {
        if (!start)
        {
            return;
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
            transform.position = new Vector3(offset.x, offset.y - idxMap * 15.0f, 0.0f);
            velocity = Vector3.zero;

            curMapObject = curMapObject.GetComponent<MapController>().ChangeMap(idxMap);
            SyncMapObjectWithIndex();
            ChangeState(PlayerState.IdleState);

            cameraController.SetMapController(curMapObject.GetComponent<MapController>());

            lastIdxMap = idxMap;
            return;
        }


        if (curState == PlayerState.MovingState)
        {
            int mapWidth = curMapObject.GetComponent<MapController>().mapWidth;

            ulong minedBlocks = mining.GetComponent<Mining>().MinedBlocks;
            int xWrapAround = -(int)(minedBlocks / (ulong)mapWidth) * mapWidth;
            Vector3 curPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 targetPos = new Vector3(((int)minedBlocks + xWrapAround) + offset.x, curPos.y, curPos.z);

            float epsilon = 10.0f;

            if (curPos.x > targetPos.x + epsilon)
            {
                curPos.x -= mapWidth;
                cameraController.ReportMapWrapAround();
            }

            if (lastIdxMap == idxMap)
            {
                velocity.x = Mathf.Clamp(velocity.x, 0.25f, 5.0f);

                transform.position = Vector3.SmoothDamp(
                    curPos, targetPos, ref velocity, calcMovingTime()
                );

                float endurance = 0.05f;

                // moving -> idle
                if (Mathf.Abs(curPos.x - targetPos.x) < endurance)
                {
                    ChangeState(PlayerState.IdleState);
                }
            }
        }
    }

    private void SyncMapObjectWithIndex()
    {
        curMapObject = GameObject.Find("PMap" + (idxMap + 1));
        mining.GetComponent<Mining>().mapController = curMapObject.GetComponent<MapController>();
    }


    public float calcMiningBonus()
    {
        return (float)statMining / 100.0f;
    }

    public float calcMovingBonus()
    {
        return (float)statMoving / 100.0f;
    }

    private float calcMovingTime()
    {
        return 0.25f / (1.0f + calcMovingBonus());
    }

    public void ChangeState(PlayerState newState)
    {
        if (curState == newState)
        {
            return;
        }

        switch (curState)
        {
        case PlayerState.IdleState:
            break;
        case PlayerState.MiningState:
            break;
        case PlayerState.MovingState:
            break;
        }

        switch (newState)
        {
        case PlayerState.IdleState:
            playerAnimator.SetTrigger("Idle");
            weaponAnimator.SetTrigger("Idle");
            break;

        case PlayerState.MiningState:
            playerAnimator.SetTrigger("Mining");
            weaponAnimator.SetTrigger("Mining");
            break;

        case PlayerState.MovingState:
            playerAnimator.SetTrigger("Walk");
            weaponAnimator.SetTrigger("Walk");
            break;
        }

        curState = newState;
    }

    void OnEnable()
    {
        tmpInputMap1Action.Enable();
        tmpInputMap2Action.Enable();
        tmpInputMap3Action.Enable();
        tmpInputMap4Action.Enable();
        tmpInputMap5Action.Enable();
    }


    void OnDisable()
    {
        tmpInputMap1Action.Disable();
        tmpInputMap2Action.Disable();
        tmpInputMap3Action.Disable();
        tmpInputMap4Action.Disable();
        tmpInputMap5Action.Disable();
    }
}
