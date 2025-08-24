using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    [Header("-----Music-----")]
    [SerializeField] private AudioClip testMusic;

    [Header("-----SFX-----")]
    [SerializeField] private AudioClip testSound;

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

    public void PlayTestMusic()
    {
        if (musicSource.clip != testMusic)
        {
            musicSource.clip = testMusic;
            musicSource.Play();
        }
    }

    public void PlayTestSound() => PlayEffect(testSound);

    private void PlayEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}