using TMPro;
using UnityEngine;

public class JewlyAnimation : MonoBehaviour
{
    public float speed = 15.0f;         // 이동 속도
    public float randomRange = 2.8f;    // 랜덤 위치 범위
    // 인벤토리 UI 위치 (예시 값, 실제 UI 위치에 맞게 설정)
    public Vector3 inventoryPosition;

    private Vector3 targetPosition;
    private bool isMoving = true;       // 땅 위 애니메이션 진행 여부
    private Vector3 lastCameraPosition;
    float randomX;

    // 인벤토리 애니메이션 여부 플래그
    private bool isInventoryAnimation = false;

    void Start()
    {
        // 땅 위로 올라가는 초기 애니메이션 설정
        Transform cameraTransform = Camera.main.transform;
        randomX = Random.Range(-randomRange, randomRange);
        targetPosition = cameraTransform.position + new Vector3(randomX, -1, 0);

        // 2D 환경이므로 Z값은 유지
        targetPosition.z = transform.position.z;

        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        if (!isInventoryAnimation)
        {
            // 기존의 땅 위 애니메이션 처리
            Transform cameraTransform = Camera.main.transform;
            // 카메라 기준으로 목표 위치 갱신 (예시)
            targetPosition = cameraTransform.position + new Vector3(randomX, -1, 3);

            if (isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
                {
                    transform.position = targetPosition;
                    isMoving = false;
                }
            }

            // 카메라 이동에 따른 보정
            Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;
            transform.position += cameraDelta;
            lastCameraPosition = cameraTransform.position;
        }
        else
        {
            // 기존의 땅 위 애니메이션 처리
            Transform cameraTransform = Camera.main.transform;
            // 카메라 기준으로 목표 위치 갱신 (예시)
            inventoryPosition = cameraTransform.position + new Vector3(-2.5f, 0.8f, 3);

            // 인벤토리 이동 애니메이션 처리
            transform.position = Vector3.MoveTowards(transform.position, inventoryPosition, 5.0f * Time.deltaTime);
            if (Vector3.Distance(transform.position, inventoryPosition) < 0.05f)
            {
                transform.position = inventoryPosition;
                // 애니메이션 완료 후 오브젝트 삭제
                Destroy(gameObject);
            }
        }
    }

    // 외부에서 인벤토리 애니메이션 시작을 요청할 때 호출되는 메서드
    public void StartInventoryAnimation()
    {
        isInventoryAnimation = true;
    }
}

