using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float baseCooldown;
    [SerializeField] private float cooldownLowerLimit;
    private float currentCooldown;
    private float shootCooldownTimer;

    [SerializeField] private GameObject baseProjectile;
    private GameObject currentProjectile;

    [SerializeField] private Transform crosshairPosition;
    private Vector2 shootDirection;
    private Quaternion shootRotation;

    private Coroutine cooldownRoutine;
    private Coroutine projectileRoutine;

    private void Awake()
    {
        currentCooldown = baseCooldown;
        currentProjectile = baseProjectile;
    }

    private void Update()
    {
        if (shootCooldownTimer > Mathf.Epsilon)
            shootCooldownTimer -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (shootCooldownTimer > Mathf.Epsilon)
            return;

        shootDirection = (crosshairPosition.position - transform.position).normalized;
        shootRotation = Quaternion.LookRotation(Vector3.forward, shootDirection);

        Instantiate(currentProjectile, transform.position, shootRotation);

        shootCooldownTimer = currentCooldown;
    }

    public void ChangeProjectile(GameObject newProjectile, float duration)
    {
        if (projectileRoutine != null)
            StopCoroutine(projectileRoutine);

        projectileRoutine = StartCoroutine(ProjectileRoutine(newProjectile, duration));
    }
    private IEnumerator ProjectileRoutine(GameObject newProjectile, float duration)
    {
        currentProjectile = newProjectile;

        yield return new WaitForSeconds(duration);

        currentProjectile = baseProjectile;
    }

    public void ChangeCooldown(float cooldownMultiplier, float duration)
    {
        if (cooldownRoutine != null)
            StopCoroutine(cooldownRoutine);

        cooldownRoutine = StartCoroutine(CooldownRoutine(cooldownMultiplier, duration));
    }
    private IEnumerator CooldownRoutine(float cooldownMultiplier, float duration)
    {
        currentCooldown *= cooldownMultiplier;
        if (currentCooldown < cooldownLowerLimit)
            currentCooldown = cooldownLowerLimit;

        yield return new WaitForSeconds(duration);

        currentCooldown = baseCooldown;
    }
}