using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("----Cooldown-----")]
    [SerializeField] private float baseCooldown;
    [SerializeField] private float cooldownLowerLimit;
    private float currentCooldown;
    private float shootCooldownTimer;

    [Header("-----Damage-----")]
    [SerializeField] private int baseDamage;
    [SerializeField] private int damageHigherLimit;
    public int currentDamage;

    [Header("-----Projectile-----")]
    [SerializeField] private GameObject baseProjectile;
    private GameObject currentProjectile;
    private int remainingRounds;

    [SerializeField] private Transform crosshairPosition;
    [SerializeField] private ParticleSystem muzzleFlash;
    private Vector2 shootDirection;
    private Quaternion shootRotation;

    private Coroutine weaponRoutine;

    private void Awake()
    {
        currentCooldown = baseCooldown;
        currentDamage = baseDamage;
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

        remainingRounds--;
    }

    public void BuffWeapon(GameObject newProjectile, int damageIncrease, float cooldownReduction, int numberOfRounds)
    {
        if (weaponRoutine != null)
        {
            remainingRounds = numberOfRounds;
            return;
        }

        weaponRoutine = StartCoroutine(WeaponRoutine(newProjectile, damageIncrease, cooldownReduction, numberOfRounds));
    }
    private IEnumerator WeaponRoutine(GameObject newProjectile, int damageIncrease, float cooldownReduction, int numberOfRounds)
    {
        currentProjectile = newProjectile;

        currentDamage += damageIncrease;
        if (currentDamage > damageHigherLimit)
            currentDamage = damageHigherLimit;

        currentCooldown -= cooldownReduction;
        if (currentCooldown < cooldownLowerLimit)
            currentCooldown = cooldownLowerLimit;

        remainingRounds = numberOfRounds;

        while (remainingRounds > 0)
            yield return null;

        currentProjectile = baseProjectile;

        currentDamage -= damageIncrease;
        if (currentDamage < baseDamage)
            currentDamage = baseDamage;

        currentCooldown += cooldownReduction;
        if (currentCooldown > baseCooldown)
            currentCooldown = baseCooldown;

        weaponRoutine = null;
    }

    public void BuffCooldown(float cooldownReduction, float duration)
    {
        StartCoroutine(CooldownRoutine(cooldownReduction, duration));
    }
    private IEnumerator CooldownRoutine(float cooldownReduction, float duration)
    {
        currentCooldown -= cooldownReduction;
        if (currentCooldown < cooldownLowerLimit)
            currentCooldown = cooldownLowerLimit;

        yield return new WaitForSeconds(duration);

        currentCooldown += cooldownReduction;
        if (currentCooldown > baseCooldown)
            currentCooldown = baseCooldown;
    }

    public void BuffDamage(int damageIncrease, float duration)
    {
        StartCoroutine(DamageRoutine(damageIncrease, duration));
    }
    private IEnumerator DamageRoutine(int damageIncrease, float duration)
    {
        currentDamage += damageIncrease;
        if (currentDamage > damageHigherLimit)
            currentDamage = damageHigherLimit;

        yield return new WaitForSeconds(duration);

        currentDamage -= damageIncrease;
        if (currentDamage < baseDamage)
            currentDamage = baseDamage;
    }
}