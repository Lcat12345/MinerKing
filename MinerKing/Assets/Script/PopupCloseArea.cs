using UnityEngine;
using UnityEngine.InputSystem;

public class PopupCloseArea : MonoBehaviour
{
    public InputAction inputAction;

    // Update is called once per frame
    void Update()
    {
        if (inputAction.WasPressedThisFrame())
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        inputAction.Enable();    
    }

    void OnDisable()
    {
        inputAction.Disable();
    }
}
