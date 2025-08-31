using System.Collections;
using UnityEngine;

public class FlyAwayEffect : MonoBehaviour
{
    public static FlyAwayEffect Instance;

    [SerializeField] private ParticleSystem windParticles;

    private HeartsManager heartsManager;
    private AltitudeBarManager altitudeBarManager;
    private TransitionScreenManager transitionScreenManager;
    private AmmoCounterManager ammoCounterManager;

    private void Awake()
    {
        Instance = this;

        heartsManager = FindFirstObjectByType<HeartsManager>();
        altitudeBarManager = FindFirstObjectByType<AltitudeBarManager>();
        transitionScreenManager = FindFirstObjectByType<TransitionScreenManager>();
        ammoCounterManager = FindFirstObjectByType<AmmoCounterManager>();
    }

    public void ApplyEffect(float duration)
    {
        windParticles.Stop();

        heartsManager.DeactivateAll();
        altitudeBarManager.Deactivate();
        ammoCounterManager.Deactivate();

        StartCoroutine(Routine(duration));
    }

    private IEnumerator Routine(float duration)
    {
        yield return new WaitForSeconds(duration);

        transitionScreenManager.PlayEnd("Victory Screen");
    }
}