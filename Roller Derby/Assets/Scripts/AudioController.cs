using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
    public AudioSource SFX;

	// Use this for initialization
	void Start () 
        {
            SFX = GetComponentInChildren<AudioSource>();
        }
	
	// Update is called once per frame
	void Update () {
	
	}

    void PlayOnce(AudioClip SoundToPlay)
    {
        SFX.PlayOneShot(SoundToPlay);
    }
}
