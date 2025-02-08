using UnityEngine;

public class CloseArea : MonoBehaviour
{
    public CameraController cameraController;
    public PlayerController playerController;
    public BGMManager bgmManager;
    public UserDataManager userDataManager;
    public GameUI gameUI;

    public MapController tmpLastPlayedMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioClip[] newBGMSet = { tmpLastPlayedMap.bgm1, tmpLastPlayedMap.bgm2, tmpLastPlayedMap.bgm3 };
        bgmManager.PlayNewBGMSet(newBGMSet);
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
