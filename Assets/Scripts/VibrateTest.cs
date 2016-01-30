using UnityEngine;
using XInputDotNetPure;

public class VibrateTest : MonoBehaviour
{
    public float TriggerProximity;

    private Transform _myTransform;
    private Transform _playerTransform;

	void Awake()
	{
	    _myTransform = GetComponent<Transform>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
	    _playerTransform = player.GetComponent<Transform>();
	}

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(_myTransform.position, _playerTransform.position);
        if (distanceFromPlayer <= TriggerProximity)
        {
            float vibrateStrength = 1f - (distanceFromPlayer/TriggerProximity);
            GamePad.SetVibration(PlayerIndex.One, vibrateStrength, vibrateStrength);
        }
        else
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        }
    }
}
