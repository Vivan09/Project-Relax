using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject playButton;
    private bool isPlayButtonpressed = false;
    private bool isPauseButtonpressed = false;



    public void OnPlayButtonPressed()
    {
        if (SoundManger.Instance.CurrentMusicPlayer != null && SoundManger.Instance.CurrentMusicPlayer != gameObject)
        {
            OnEndSong();

        }
        SoundManger.Instance.CurrentMusicPlayer = gameObject;
        if (isPauseButtonpressed == true)
        {
            pauseButton.SetActive(true);
            playButton.SetActive(false);
            SoundManger.Instance.UnPauseMusic();
        }
        else
        {
         
            pauseButton.SetActive(true);
            playButton.SetActive(false);
            SoundManger.Instance.PlayMusic(audioClip);
        }
       
    }
    public void OnPauseButtonPressed()
    {
        isPauseButtonpressed = true;
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        SoundManger.Instance.PauseMusic();
    }
    public void OnEndSong()
    {
        isPauseButtonpressed = false;
        SoundManger.Instance.CurrentMusicPlayer.GetComponent<MusicScript>().pauseButton.SetActive(false);
        SoundManger.Instance.CurrentMusicPlayer.GetComponent<MusicScript>().playButton.SetActive(true);
        SoundManger.Instance.StopMusic();
    }
   
}
