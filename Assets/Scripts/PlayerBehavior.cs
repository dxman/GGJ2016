using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerBehavior : MonoBehaviour
{
    private AudioSource _myAudioSource;
    public AudioClip wallHitSound;

	void Awake()
	{
	    _myAudioSource = GetComponentInParent<AudioSource>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level")
        {
            _myAudioSource.clip = wallHitSound;
            _myAudioSource.Play(0);
        }
        else if (other.tag == "Enemy")
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            SceneManager.LoadScene("LoseScreen");
        }
        else if (other.tag == "Goal")
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            SceneManager.LoadScene("WinScreen");
        }
    }
}
