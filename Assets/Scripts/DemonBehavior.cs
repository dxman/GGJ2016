using UnityEngine;
using XInputDotNetPure;

public class DemonBehavior : MonoBehaviour
{
    public float TriggerProximity;
    public float LookSpeed;

    private Transform _myTransform;
    private Transform _playerTransform;

	void Awake()
	{
	    _myTransform = GetComponent<Transform>();

        GameObject player = GameObject.FindWithTag("Player");
	    _playerTransform = player.GetComponent<Transform>();
	}

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(_myTransform.position, _playerTransform.position);
        if (distanceFromPlayer <= TriggerProximity)
        {
            float vibrateStrength = 1f - (distanceFromPlayer/TriggerProximity);
            GamePad.SetVibration(PlayerIndex.One, vibrateStrength, vibrateStrength);

            _myTransform.rotation = Quaternion.RotateTowards(_myTransform.rotation, Quaternion.LookRotation((_playerTransform.position) - _myTransform.position, Vector3.up), LookSpeed * Time.deltaTime);
        }
        else
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        }
    }

    void OnDestroy()
    {
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
    }
}
