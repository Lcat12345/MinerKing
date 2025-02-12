using UnityEngine;

public class QuitPopup : MonoBehaviour
{
    public GameObject subPopup;

    void Awake()
    {
        subPopup.SetActive(false);
    }

    public void OnClick()
    {
        subPopup.SetActive(true);
    }

    public void OnClickCloseArea()
    {
        subPopup.SetActive(false);
    }
}
