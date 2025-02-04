using TMPro;
using UnityEngine;

public class JewlyAnimation : MonoBehaviour
{
    public MapController mapController;
    public float speed;         // 이동 속도
    public float randomRange = 2.8f;    // 랜덤 위치 범위

    private Vector3 targetPosition;
    // Used For Inventory Animations ==========
    // 인벤토리 UI 위치 (예시 값, 실제 UI 위치에 맞게 설정)
    public Vector3 inventoryPosition;
    private float acceleration;
    private float maxSpeed;
    private float angularAcceleration;
    private float angularSpeed;
    private float maxAngularSpeed;
    // ========================================
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

        acceleration = 3500.0f;
        maxSpeed = 3000.0f;
        angularAcceleration = 200.0f;
        angularSpeed = 80.0f;
        maxAngularSpeed = 500.0f;
    }

    private void FixedUpdate()
    {
        if (!isInventoryAnimation)
        {
            UpdateGroundMovement();
        }
        else
        {
            UpdateInventoryMovement();
        }
    }

    private void UpdateGroundMovement()
    {
        Transform cameraTransform = Camera.main.transform;
        // 카메라 기준으로 목표 위치 갱신
        targetPosition = new Vector3(cameraTransform.position.x + randomX, cameraTransform.position.y - 1, 0);

        if (isMoving)
        {
            float epsilon = 10.0f;
            if (lastCameraPosition.x > cameraTransform.position.x + epsilon)
            {
                transform.position = new Vector3(
                    transform.position.x - mapController.mapWidth,
                    transform.position.y,
                    transform.position.z
                );
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }

        // 카메라 이동에 따른 보정
        float cameraDelta = cameraTransform.position.x - lastCameraPosition.x;
        transform.position = new Vector3(
            transform.position.x + cameraDelta,
            transform.position.y,
            transform.position.z
        );
        lastCameraPosition = cameraTransform.position;
    }

    private void UpdateInventoryMovement()
    {
        Vector3 direction = (inventoryPosition - transform.position).normalized;

        // 속도 증가 (가속 적용)
        speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
        angularSpeed = Mathf.Min(angularSpeed + angularAcceleration * Time.deltaTime, maxAngularSpeed);

        // 이동 적용
        transform.position += direction * speed * Time.deltaTime;
        transform.Rotate(0, 0, angularSpeed * Time.deltaTime);

        // 목표에 가까워질수록 감속
        float distance = Vector3.Distance(transform.position, inventoryPosition);
        if (distance < 270.0f)
        {
            speed = Mathf.Max(speed * (1.0f - 6.5f * Time.deltaTime), 50.0f);
            angularSpeed = Mathf.Max(angularSpeed * (1.0f - 0.5f * Time.deltaTime), 12.5f);
            if (distance < 50.0f)
            {
                transform.position = inventoryPosition;
                isInventoryAnimation = false; // 애니메이션 종료
                Destroy(gameObject);
            }
        }
    }

    // 외부에서 인벤토리 애니메이션 시작을 요청할 때 호출되는 메서드
    public void StartInventoryAnimation()
    {
        speed = 200.0f;
        isInventoryAnimation = true;
    }
}