using UnityEngine;

public class IntroImage : MonoBehaviour
{
    RectTransform imageTransform;
    float speed = 0.5f;
    float minScale = 1.0f;
    float maxScale = 1.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float scale = Mathf.PingPong(Time.time * speed, maxScale - minScale) + minScale;        
        imageTransform.localScale = new Vector2(scale, scale);
    }
}
