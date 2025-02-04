using UnityEngine;
using UnityEngine.UI;

public class TouchScreen : MonoBehaviour
{
    Image imageRenderer;
    float blinkSpeed = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageRenderer = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

        Color color = imageRenderer.color;
        color.a = alpha;

        imageRenderer.color = color;
    }
}
