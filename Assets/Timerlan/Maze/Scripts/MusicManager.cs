using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSourse;

    void Start()
    {
        audioSourse = GetComponent<AudioSource>();
        audioSourse.Play();
    }
}
