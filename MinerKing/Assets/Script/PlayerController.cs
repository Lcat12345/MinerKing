using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public uint statBonus = 0;
    public Pickaxes curPickaxe;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curPickaxe = Pickaxes.FantasticPickaxe;    // temporary
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
