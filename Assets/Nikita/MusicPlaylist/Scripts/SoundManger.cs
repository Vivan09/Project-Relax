using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManger : MonoBehaviour
{
    public static SoundManger Instance;

    [SerializeField] private AudioSource musicSource, effectsSource;
    [SerializeField] private Slider slider;

    public GameObject CurrentMusicPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadData();
    }
    private void SaveData()
    {
        SoundData soundData = new SoundData();
        soundData.Volume = musicSource.volume;
    }
    private void LoadData()
    {
        SoundData soundData = new SoundData();
        musicSource.volume = soundData.Volume;
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.PlayOneShot(clip);
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PauseMusic()
    {
        musicSource.Pause();
    }
    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }
    public void OnSliderValueChanged()
    {
        musicSource.volume = slider.value;
        effectsSource.volume = slider.value;

        SaveData();

    }
}
public class SoundData
{
    public float Volume;

}