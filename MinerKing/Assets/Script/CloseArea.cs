using UnityEngine;

public class CloseArea : MonoBehaviour
{
    public CameraController cameraController;
    public PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickScreen()
    {
        cameraController.start = true;
        playerController.start = true;

        gameObject.SetActive(false);
    }
}
