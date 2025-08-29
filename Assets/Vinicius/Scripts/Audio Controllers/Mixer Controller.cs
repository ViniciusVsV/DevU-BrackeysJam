using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public void SetMasterVolume(float newVolume)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(newVolume) * 20);
    }

    public void SetMusicVolume(float newVolume)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(newVolume) * 20);
    }

    public void SetEffectVolume(float newVolume)
    {
        mixer.SetFloat("effectVolume", Mathf.Log10(newVolume) * 20);
    }
}
