using UnityEngine;
using UnityEngine.UI;

public class CalculatePopup : MonoBehaviour
{
    public ScrollRect scrollRect;

    void OnEnable()
    {
        ResetScrollPosition();
    }

    private void ResetScrollPosition()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
