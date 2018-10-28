using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour {

    public AudioClip MenuMusic;
    public Slider Volume;
    public AudioSource MusicSource;

	// Use this for initialization
	void Start () {
        MusicSource.clip = MenuMusic;
	}

    // Update is called once per frame
    void Update()
    {
        MusicSource.volume = Volume.value;
    }
}
