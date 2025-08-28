using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AltitudeManager : MonoBehaviour
{
    [SerializeField] private float totalDuration = 180f;
    private float elapsedTime;
    [SerializeField] private float startingAltitude = 2000f;
    private float currentAltitude;

    [SerializeField] private Slider altitudeBar;


    private void Awake()
    {
        currentAltitude = startingAltitude;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        currentAltitude = Mathf.Lerp(startingAltitude, 0f, elapsedTime / totalDuration);
        currentAltitude = Mathf.Max(currentAltitude, 0f);

        UpdateUI();

        if (elapsedTime >= totalDuration)
            SceneManager.LoadScene("Final Menu");
    }

    private void UpdateUI()
    {
        float progress = 1f - (currentAltitude / startingAltitude);

        altitudeBar.value = progress;
    }
}