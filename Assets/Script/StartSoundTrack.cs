using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSoundTrack : MonoBehaviour
{
    [SerializeField] string name;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer.timer(0.001f,()=>SoundtrackUtil.PlayMusic(name)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
