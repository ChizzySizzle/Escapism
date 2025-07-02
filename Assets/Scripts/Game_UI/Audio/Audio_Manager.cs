
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance;
    public AudioMixer musicMixer;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0));
        musicMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0));
        musicMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0));
    }

    public void ChangeMasterVolume(float value)
    {
        float masterVolume = ConvertToDB(value);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        musicMixer.SetFloat("MasterVolume", masterVolume);
    }

    public void ChangeMusicVolume(float value)
    {
        float musicVolume = ConvertToDB(value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        musicMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void ChangeSFXVolume(float value)
    {
        float SFXVolume = ConvertToDB(value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        musicMixer.SetFloat("SFXVolume", SFXVolume);
    }

    private float ConvertToDB(float value)
    {
        return Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
    }
}
