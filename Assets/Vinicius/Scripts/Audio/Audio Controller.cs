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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        PlayMenuMusic();
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

    private void PlayEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}