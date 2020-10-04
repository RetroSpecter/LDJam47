using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    public string audioEvent = "";
    public AudioSource source;

    public void PlaySound() {
        if (source == null)
            AudioManager.instance.Play(audioEvent);
        else 
            AudioManager.instance.Play(audioEvent, source);
    }
}
