using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollowBehaviour : MonoBehaviour
{
    private PlayerController playerController;

    [Header("-----Behaviour-----")]
    [SerializeField] private float maxDistance;
    [SerializeField] private float multiplier;
    private Vector2 mousePosition;
    private Vector2 mouseDirection;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();

        transform.SetParent(transform.parent, true);
    }

    private void Update()
    {
        if (playerController.isOnController)
            transform.position = Vector2.zero + maxDistance * multiplier * playerController.lookDirection;

        else
        {
            mousePosition = Mouse.current.position.ReadValue();
            mouseDirection = Camera.main.ScreenToWorldPoint(new Vector2(mousePosition.x, mousePosition.y)).normalized;

            transform.position = maxDistance * multiplier * mouseDirection;
        }
    }


}