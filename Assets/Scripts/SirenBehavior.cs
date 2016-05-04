using System;
using Pathfinding;
using UnityEngine;

[Serializable]
public struct ProximitySound
{
    public float TriggerProximity;
    public AudioClip Clip;
}

public class SirenBehavior : MonoBehaviour
{
    private Transform _myTransform;
    private AudioSource _myAudioSource;

    [SerializeField]
    private ProximitySound[] _sounds = new ProximitySound[4];

    private Transform _playerTransform;

    private int _previousSoundIndex;

	void Awake()
	{
	    _myTransform = GetComponent<Transform>();
	    _myAudioSource = GetComponent<AudioSource>();
        
        GameObject player = GameObject.FindWithTag("Player");
	    _playerTransform = player.GetComponent<Transform>();
	    _previousSoundIndex = -1;
	}
	
	void Update()
    {
        float distanceFromPlayer = Vector3.Distance(_myTransform.position, _playerTransform.position);
	    int currentSoundIndex = 3;
	    for (int i = 3; i >= 0; i--)
	    {
	        if (distanceFromPlayer <= _sounds[i].TriggerProximity)
	        {
	            currentSoundIndex = i;
	        }
	    }

        if (_previousSoundIndex != currentSoundIndex)
        {
            _myAudioSource.clip = _sounds[currentSoundIndex].Clip;
            _myAudioSource.Play();
            _previousSoundIndex = currentSoundIndex;
        }
    }
}
