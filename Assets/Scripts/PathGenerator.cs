using UnityEngine;

public class PathGenerator : MonoBehaviour
{
	void Awake()
	{
	    GameObject goal = GameObject.Find("Goal");
	    Transform goalTransform = goal.GetComponent<Transform>();

	    GameObject siren = GameObject.Find("Siren");
	    NavMeshAgent sirenNavMeshAgent = siren.GetComponent<NavMeshAgent>();

	    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

	    NavMeshPath path = new NavMeshPath();
	    while (!sirenNavMeshAgent.CalculatePath(goalTransform.position, path))
	    {
	        int i = NumberGenerator.Instance.Next(enemies.Length);
            enemies[i].SetActive(false);
	    }

	    sirenNavMeshAgent.SetDestination(goalTransform.position);
	}

    void Update()
    {
	
	}
}
