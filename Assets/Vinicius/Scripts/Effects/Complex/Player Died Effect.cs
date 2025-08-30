using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDiedEffect : MonoBehaviour
{
    public static PlayerDiedEffect Instance;

    [Header("-----Objects-----")]
    [SerializeField] private ParticleSystem windParticles;
    [SerializeField] private GameObject playerCrosshair;

    private HeartsManager heartsManager;
    private AltitudeBarManager altitudeBarManager;
    private AltitudeManager altitudeManager;
    private TransitionScreenManager transitionScreenManager;
    private AmmoCounterManager ammoCounterManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        heartsManager = FindFirstObjectByType<HeartsManager>();
        altitudeBarManager = FindFirstObjectByType<AltitudeBarManager>();
        altitudeManager = FindFirstObjectByType<AltitudeManager>();
        transitionScreenManager = FindFirstObjectByType<TransitionScreenManager>();
        ammoCounterManager = FindFirstObjectByType<AmmoCounterManager>();
    }

    public void ApplyEffect(float deathDuration)
    {
        altitudeManager.hasStopped = true;

        playerCrosshair.SetActive(false);

        windParticles.Stop();

        heartsManager.DeactivateAll();
        altitudeBarManager.Deactivate();
        ammoCounterManager.Deactivate();

        StartCoroutine(Routine(deathDuration));
    }

    private IEnumerator Routine(float deathDuration)
    {
        yield return new WaitForSeconds(deathDuration);

        transitionScreenManager.PlayEnd("Defeat Screen");
    }
}