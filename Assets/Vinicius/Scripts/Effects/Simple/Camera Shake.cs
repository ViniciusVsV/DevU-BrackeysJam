using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private CinemachineImpulseSource playerDamagedImpulse;
    [SerializeField] private CinemachineImpulseSource explosionImpulse;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(bool isExplosion)
    {
        if (isExplosion)
            explosionImpulse.GenerateImpulse();
        else
            playerDamagedImpulse.GenerateImpulse();
    }
}