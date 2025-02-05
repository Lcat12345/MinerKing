using UnityEngine;

public class CloseArea : MonoBehaviour
{
    public CameraController cameraController;
    public PlayerController playerController;
    public GameUI gameUI;

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
        gameUI.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }
}
