using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private Vector2 mousePosition;
    private Vector2 newPosition;

    [Header("-----Controller Behaviour-----")]
    [SerializeField] private Transform idlePosition;
    [SerializeField] private float maxDistance;

    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();

        transform.SetParent(transform.parent, true);
    }

    private void Update()
    {
        if (playerController.isOnController)
            transform.position = (Vector2)idlePosition.position + playerController.lookDirection * maxDistance;

        else
        {
            mousePosition = Mouse.current.position.ReadValue();
            newPosition = Camera.main.ScreenToWorldPoint(new Vector2(mousePosition.x, mousePosition.y));

            transform.position = newPosition;
        }
    }
}