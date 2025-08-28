using UnityEngine;

public class InfiniteRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f; // graus por segundo

    private void Update()
    {
        // Rotaciona no eixo Z (2D)
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
