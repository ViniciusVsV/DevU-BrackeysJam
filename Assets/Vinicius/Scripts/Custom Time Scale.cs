using UnityEngine;

[CreateAssetMenu(fileName = "TimeData", menuName = "Scriptable Objects/TimeData")]
public class CustomTimeScale : ScriptableObject
{
    [Range(0f, 2f)][SerializeField] private float timeScaleMultiplier = 1f;

    private float DeltaTime => Time.deltaTime * timeScaleMultiplier;
    private float FixedDeltaTime => Time.fixedDeltaTime * timeScaleMultiplier;

    public void ChangeTimeScale(float newMultiplier)
    {
        timeScaleMultiplier = newMultiplier;
    }
    public void ResetTimeScale()
    {
        timeScaleMultiplier = 1f;
    }

    public float GetDeltaTime() { return DeltaTime; }
    public float GetFixedDeltaTime() { return FixedDeltaTime; }
}
