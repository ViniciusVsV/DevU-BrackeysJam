using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticlesPrefab;

    [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency;
    [SerializeField] private float rumbleDuration;

    public static ExplosionEffect Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(Vector2 position)
    {
        Instantiate(explosionParticlesPrefab, position, Quaternion.identity);

        CameraShake.Instance.ApplyEffect(true, Vector2.zero);
        ControllerRumble.Instance.ApplyEffect(lowFrequency, highFrequency, rumbleDuration);
        AudioController.Instance.PlayExplosionSound();
    }
}