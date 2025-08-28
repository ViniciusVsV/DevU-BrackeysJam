using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AltitudeManager : MonoBehaviour
{
    [SerializeField] private float totalDuration = 180f;
    private float elapsedTime;

    [SerializeField] private Slider altitudeBar;

    [Header("-----Background Movement-----")]
    [SerializeField] private Transform[] backgroundLayers;
    [SerializeField] private float[] layerSpeeds;
    [SerializeField] private float targetY;
    private float[] startY;

    private void Awake()
    {
        elapsedTime = 0f;

        startY = new float[backgroundLayers.Length];
        for (int i = 0; i < backgroundLayers.Length; i++)
            startY[i] = backgroundLayers[i].position.y;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / totalDuration);

        altitudeBar.value = progress;

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            float newY = Mathf.Lerp(startY[i], targetY, progress * layerSpeeds[i]);
            backgroundLayers[i].position = new Vector2(backgroundLayers[i].position.x, newY);
        }

        if (elapsedTime >= totalDuration)
            SceneManager.LoadScene("Final Menu");
    }
}