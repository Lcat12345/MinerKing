using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    Animator player;
    Animator weapon;

    public InputAction oneInputAction;
    public InputAction twoInputAction;
    public InputAction threeInputAction;
    private void Start()
    {
        GameObject goPlayer = GameObject.Find("Player");
        player = goPlayer.GetComponent<Animator>();

        GameObject goWeapon = GameObject.Find("Weapon");
        weapon = goWeapon.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (oneInputAction.WasPressedThisFrame())
        {
            player.SetTrigger("Idle");
            weapon.SetTrigger("Idle");
        }
        else if (twoInputAction.WasPressedThisFrame())
        {
            player.SetTrigger("Mining");
            weapon.SetTrigger("Mining");
        }
        else if (threeInputAction.WasPressedThisFrame())
        {
            player.SetTrigger("Walk");
            weapon.SetTrigger("Walk");
        }
    }

    void OnEnable()
    {
        oneInputAction.Enable();
        twoInputAction.Enable();
        threeInputAction.Enable();
    }


    void OnDisable()
    {
        oneInputAction.Disable();
        twoInputAction.Disable();
        threeInputAction.Disable();
    }
}
