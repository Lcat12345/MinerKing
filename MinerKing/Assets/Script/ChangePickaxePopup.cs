using UnityEngine;
using UnityEngine.UI;

public class ChangePickaxePopup : MonoBehaviour
{
    public ScrollRect scrollRect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        ResetScrollPosition();
    }

    private void ResetScrollPosition()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
