using UnityEngine;
using UnityEngine.UI;

public class AltitudeManager : MonoBehaviour
{
    [SerializeField] private float totalDuration = 180f;
    private float elapsedTime;

    [SerializeField] private Slider altitudeBar;

    [Header("-----Background Parallax-----")]
    [SerializeField] private Transform[] backgroundLayers;
    [SerializeField] private float[] layerSpeeds;

    private float[] startY;
    private float[] endY;

    public bool hasStopped;

    private void Awake()
    {
        elapsedTime = 0f;

        int count = backgroundLayers.Length;

        startY = new float[count];
        endY = new float[count];

        for (int i = 0; i < count; i++)
        {
            endY[i] = backgroundLayers[i].position.y;

            float distance = layerSpeeds[i] * totalDuration;

            startY[i] = endY[i] - distance;

            backgroundLayers[i].position = new Vector2(backgroundLayers[i].position.x, startY[i]);
        }
    }

    private void Update()
    {
        if (hasStopped)
            return;

        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / totalDuration);

        altitudeBar.value = progress;

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            float newY = Mathf.Lerp(startY[i], endY[i], progress);
            backgroundLayers[i].position = new Vector2(backgroundLayers[i].position.x, newY);
        }

        if (elapsedTime >= totalDuration)
        {
            GroundReachedEffect.Instance.ApplyEffect();
            enabled = false;
        }
    }
}