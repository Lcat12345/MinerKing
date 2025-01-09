using UnityEngine;

public class BackGround : MonoBehaviour
{
    private float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if(transform.position.x < -7f)
        {
            transform.position += new Vector3(14f, 0, 0);
        }
    }
}
