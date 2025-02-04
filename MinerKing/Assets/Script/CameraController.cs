using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset = new Vector3(1.7f, 3.4f + 0.14f, -10.0f);

    private GameObject attachedObject;
    private MapController mapController;
    private bool isShaking = false;
    private float shakeIntensity = 8.0f;
    private float shakeElapsed = 0.0f;
    private float shakeDuration = 0.2f;
    private Vector3 velocity;

    public bool start = false;
    private bool hasChangedMap = false;
    private bool hasWrappedAround = false;

    private void Start()
    {
        velocity = Vector3.zero;
    }

    void LateUpdate()
    {
        if (hasChangedMap)
        {
            transform.position = attachedObject.transform.position + offset;
            hasChangedMap = false;
        }
        else
        {
            if (hasWrappedAround)
            {
                transform.position -= new Vector3(mapController.mapWidth, 0, 0);
                hasWrappedAround = false;
            }

            transform.position = Vector3.SmoothDamp(
                transform.position,
                attachedObject.transform.position + offset,
                ref velocity,
                0.1f
            );
        }
        

        if (isShaking)
        {
            shakeElapsed += Time.deltaTime;
            if (shakeElapsed > shakeDuration)
            {
                shakeElapsed = 0.0f;
                isShaking = false;
                return;
            }

            Vector2 rand = Random.insideUnitSphere * Time.deltaTime * shakeIntensity;
            transform.position = new Vector3(
                transform.position.x + rand.x,
                transform.position.y + rand.y,
                transform.position.z
            );
        }
    }

    public void Shake()
    {
        isShaking = true;
    }

    public void Attach(GameObject obj)
    {
        attachedObject = obj;
    }

    public void SetMapController(MapController controller)
    {
        mapController = controller;
        hasChangedMap = true;
    }

    public void ReportMapWrapAround()
    {
        hasWrappedAround = true;
    }
}
