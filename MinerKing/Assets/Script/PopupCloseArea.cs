using UnityEngine;
using UnityEngine.InputSystem;

public class PopupCloseArea : MonoBehaviour
{
    public InputAction inputAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
