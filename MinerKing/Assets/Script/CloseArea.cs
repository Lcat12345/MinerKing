using UnityEngine;

public class CloseArea : MonoBehaviour
{
    CameraController cc;

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
        cc = GameObject.Find("Main Camera").GetComponent<CameraController>();
        cc.start = true;

        gameObject.SetActive(false);
    }
}
