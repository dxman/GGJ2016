using UnityEngine;

public class NavAgentTest : MonoBehaviour
{
    public Transform targetTransform;

    private NavMeshAgent _myNavMeshAgent;
    private CountdownTimer _myTimer;

	void Awake()
	{
	    GameObject goal = GameObject.Find("Goal");

	    _myNavMeshAgent = GetComponent<NavMeshAgent>();
	    _myTimer = GetComponent<CountdownTimer>();
	    //_myNavMeshAgent.SetDestination(new Vector3(-8f, 0f, -8f));
	}

    // Update is called once per frame
    void Update () {
        if (_myTimer.IsExpired)
        {
            _myTimer.Reset();
            _myNavMeshAgent.SetDestination(targetTransform.position);
        }
    }
}
