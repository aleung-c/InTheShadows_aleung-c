using UnityEngine;
using System.Collections;

public class WorldSoundController : MonoBehaviour {
    public AudioSource[]        AudioSources;

	// Use this for initialization
	void Start ()
    {
        AudioSources = GetComponentsInChildren<AudioSource> ();
        if (GameManager.instance.GameSettings.WithSound == false)
        {
            foreach (AudioSource source in AudioSources)
            {
                source.enabled = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
