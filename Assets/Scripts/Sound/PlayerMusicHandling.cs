using UnityEngine;
using System.Collections;

public class PlayerMusicHandling : MonoBehaviour {
    public AudioSource  Source;
    public AudioClip    Music1;
    public AudioClip    Music2;

    public AudioClip    ActiveAudioClip;
    public bool         Transitionning;
    private bool        MusicOff;

	// Use this for initialization
	void Start () {
        Source = GetComponentInChildren<AudioSource>();
        ActiveAudioClip = Music1;
        Source.clip = ActiveAudioClip;
        if (GameManager.instance.GameSettings.WithSound == false)
        {
            Source.enabled = false;
        }
        else
        {
            Source.Play();
        }
    }

    public void ChangeMusic()
    {
        StartCoroutine("FadeMusicChangeRoutine");
    }
	
    IEnumerator     FadeMusicChangeRoutine()
    {
        for (;;)
        {
            if (MusicOff == false && Source.volume > 0.0F)
            {
                Source.volume -= 0.05F;
            }
            else if (MusicOff == true && Source.volume < 1.0F)
            {
                Source.volume += 0.05F;
                if (Source.volume >= 1.0F)
                {
                    yield break;
                }
            }

            if (Source.volume <= 0.0F)
            {
                MusicOff = true;
                if (ActiveAudioClip == Music1)
                {
                    ActiveAudioClip = Music2;
                }
                else
                {
                    ActiveAudioClip = Music1;
                }
                Source.clip = ActiveAudioClip;
                Source.Play();
            }
            yield return new WaitForSeconds(0.3F);

        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
