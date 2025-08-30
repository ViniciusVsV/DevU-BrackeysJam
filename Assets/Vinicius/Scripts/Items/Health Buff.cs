using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthBuff : MonoBehaviour
{
    [SerializeField] private int hpToHeal;
    private PlayerController playerController;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();

            playerController.BuffAddHealth(hpToHeal);

            Destroy(gameObject);
        }
    }
}