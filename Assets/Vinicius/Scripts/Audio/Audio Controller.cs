using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    [Header("-----Music-----")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("-----SFX-----")]
    [SerializeField] private AudioClip smgGunshot;
    [SerializeField] private AudioClip akGunshot;
    [SerializeField] private AudioClip damageTaken;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip powerUp;

    [SerializeField] private float fadeDuration = 1.5f;

    private Coroutine fadeRoutine;
    float startVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlayMenuMusic()
    {
        if (musicSource.clip != menuMusic)
        {
            musicSource.clip = menuMusic;
            musicSource.Play();
        }
    }

    public void PlayGameMusic()
    {
        if (musicSource.clip != gameMusic)
        {
            musicSource.clip = gameMusic;
            musicSource.Play();
        }
    }

    public void PlaySmgShotSound() => PlayEffect(smgGunshot);
    public void PlayAkShotSound() => PlayEffect(akGunshot);
    public void PlayDamageTakenSound() => PlayEffect(damageTaken);
    public void PlayExplosionSound() => PlayEffect(explosion);
    public void PlayPowerUpSound() => PlayEffect(powerUp);

    private void PlayEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            musicSource.volume = startVolume;
        }

        fadeRoutine = StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        startVolume = musicSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }
}