using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public float defaultVolume = 1;
    public audioQueue[] audioQueues;
    private Dictionary<String, audioQueue> audioQueueDict;

    public void Awake()
    {
        instance = this;
        audioQueueDict = new Dictionary<string, audioQueue>();
        foreach (audioQueue aq in audioQueues) {
            if (audioQueueDict.ContainsKey(aq.name)) {
                Debug.LogError("Invalid Input: repeated name in audioQueues", this.gameObject);
                break;
            }

            audioQueueDict.Add(aq.name, aq);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Play(AudioSource source, AudioClip sound, float volume = 1, float pitch = 1)
    {
        /* this is adjusting the volume and pitch values of the AudioSource to match the settings we passed in*/
        source.volume = volume;
        source.pitch = pitch;

        /* this is a function of AudioSource that tells it to play itself with whatever audiosettings we just gave it*/
        source.PlayOneShot(sound);
    }

    public void Play(AudioSource source, AudioClip[] sound, float volume = 1, float pitch = 1)
    {
        /* this is adjusting the volume and pitch values of the AudioSource to match the settings we passed in*/
        source.volume = volume;
        source.pitch = pitch;

        /* this is a function of AudioSource that tells it to play itself with whatever audiosettings we just gave it*/
        source.PlayOneShot(sound[UnityEngine.Random.Range(0, sound.Length)]);
    }


    public void Play(AudioSource source, AudioClip[] sound, float volume = 1, float minPitch= 1, float maxPitch = 1)
    {
        Play(source, sound, volume, UnityEngine.Random.Range(minPitch, maxPitch));
    }

    public void Play(String audioQueueName) {
        audioQueue aq = audioQueueDict[audioQueueName];
        if (aq == null) {
            Debug.LogError("Invalid Input: name not in list", this.gameObject);
            return;
        }

        Play(aq.audioSource, aq.audio, aq.volume, aq.pitchRange.x, aq.pitchRange.y);
    }

    public void Play(String audioQueueName, AudioSource source)
    {
        audioQueue aq = audioQueueDict[audioQueueName];
        if (aq == null)
        {
            Debug.LogError("Invalid Input: name not in list", this.gameObject);
            return;
        }

        Play(source, aq.audio, aq.volume, aq.pitchRange.x, aq.pitchRange.y);
    }
}

[System.Serializable]
public class audioQueue
{
    public string name;
    public AudioSource audioSource;
    public AudioClip[] audio;
    public float volume = 1;
    public Vector2 pitchRange = Vector2.one;
}