using UnityEngine;

public class NavAgentTest : MonoBehaviour
{
    public Transform targetTransform;

    private NavMeshAgent _myNavMeshAgent;

	void Awake()
	{
	    _myNavMeshAgent = GetComponent<NavMeshAgent>();
	    _myNavMeshAgent.SetDestination(new Vector3(-8f, 0f, -8f));
	}

    // Update is called once per frame
    void Update () {
	
	}
}
