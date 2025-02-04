using UnityEngine;

public class CloseArea : MonoBehaviour
{
    public GameUI gameUI;
    public CameraController cameraController;

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
        gameUI.gameObject.SetActive(true);
        cameraController.start = true;

        gameObject.SetActive(false);
    }
}
