using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class GroundReachedEffect : MonoBehaviour
{
    public static GroundReachedEffect Instance;

    private CinemachineImpulseSource cinemachineImpulseSource;

    [Header("-----Objects-----")]
    [SerializeField] private ParticleSystem windParticles;
    [SerializeField] private GameObject playerCrosshair;

    [Header("-----Rumble Parameters-----")]
    [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency;
    [SerializeField] private float rumbleDuration;

    private BossController bossController;
    private PlayerController playerController;
    private HeartsManager heartsManager;
    private AltitudeBarManager altitudeBarManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bossController = FindFirstObjectByType<BossController>();
        playerController = FindFirstObjectByType<PlayerController>();
        heartsManager = FindFirstObjectByType<HeartsManager>();
        altitudeBarManager = FindFirstObjectByType<AltitudeBarManager>();

        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    //Desativar UI
    public void ApplyEffect()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        bossController.SetDeactivateState();

        float waitDuration = playerController.Crash();

        windParticles.Stop();

        yield return new WaitForSeconds(waitDuration);

        cinemachineImpulseSource.GenerateImpulse(5f);

        SpriteFlash.Instance.ApplyEffect(playerController.gameObject);
        ControllerRumble.Instance.ApplyEffect(lowFrequency, highFrequency, rumbleDuration);

        playerCrosshair.SetActive(false);

        heartsManager.DeactivateAll();
        altitudeBarManager.Deactivate();
    }
}