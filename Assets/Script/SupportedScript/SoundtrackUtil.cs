using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundtrackUtil
{

    public static void PlayMusic(string name)
    {
        GameObject.FindWithTag("SoundTrack").GetComponent<SoundTrackScript>().PlayMusic(name);
    }

    public static void SetVolume(float num)
    {
        GameObject.FindWithTag("SoundTrack").GetComponent<SoundTrackScript>().SetVolume(num);
    }

    public static AudioSource GetAudio()
    {
        return GameObject.FindWithTag("SoundTrack").GetComponent<AudioSource>();
    }




}
