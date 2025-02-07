using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CalculatePopup : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] UserDataManager userDataManager;
    [SerializeField] GameObject contents;
    List<GameObject> jewelList;

    private void Start()
    {
        foreach (Transform child in contents.transform)
        {
            foreach (Transform item in child)
            {
                jewelList.Add(item.gameObject);
            }
        }

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
