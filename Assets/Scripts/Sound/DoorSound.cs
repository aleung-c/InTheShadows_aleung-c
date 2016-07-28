using UnityEngine;
using System.Collections;

public class DoorSound : MonoBehaviour {
    public AudioClip        DoorClip1;
    public AudioClip        DoorClip2;
    public AudioClip        DoorClip3;
    private AudioSource     DoorAudioSource;

	// Use this for initialization
	void Awake () {
        DoorAudioSource = GetComponentInChildren<AudioSource>();
    }
	
    public void PlayDoorSound1()
    {
        DoorAudioSource.clip = DoorClip1;
        DoorAudioSource.Play();
    }

    public void PlayDoorSound2()
    {
        DoorAudioSource.clip = DoorClip2;
        DoorAudioSource.Play();
    }

    public void PlayDoorSound3()
    {
        DoorAudioSource.clip = DoorClip3;
        DoorAudioSource.Play();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
