using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPosition;

    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxInsentity;

    private void Awake()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxInsentity);
        float distance = cam.transform.position.x * parallaxInsentity;

        transform.position = new Vector2(startPosition + distance, transform.position.y);

        if (temp > startPosition + length)
            startPosition += length;
        else if (temp < startPosition - length)
            startPosition -= length;
    }
}