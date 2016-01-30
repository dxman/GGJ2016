using UnityEngine;
using System.Collections;

public class WallHit : MonoBehaviour
{
    private AudioSource _myAudioSource;
    public AudioClip hitSound;
	// Use this for initialization
	void Awake()
	{
	    _myAudioSource = GetComponentInParent<AudioSource>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level")
        {
            _myAudioSource.clip = hitSound;
            _myAudioSource.Play();
        }
    }
}
