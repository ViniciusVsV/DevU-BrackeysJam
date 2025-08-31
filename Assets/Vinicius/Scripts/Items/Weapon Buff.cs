using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponBuff : MonoBehaviour
{
    private PlayerWeapon playerWeapon;

    [SerializeField] private GameObject newProjectile;
    [SerializeField] private int damageIncrease;
    [SerializeField] private float cooldownReduction;
    [SerializeField] private int numberOfRounds;

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            col.enabled = false;
            AudioController.Instance.PlayPowerUpSound();

            playerWeapon = other.GetComponent<PlayerController>().GetWeapon();

            if (playerWeapon != null)
                playerWeapon.BuffWeapon(newProjectile, damageIncrease, cooldownReduction, numberOfRounds);

            Destroy(gameObject);
        }
    }
}