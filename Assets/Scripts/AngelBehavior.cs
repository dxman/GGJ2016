using UnityEngine;

public class AngelBehavior : MonoBehaviour
{
    private Transform _myTransform;
    private AIPath _myAiPath;

    private SpawnPointManager _spawnPointManager;
    private Transform[] _spawnTransforms;

	void Awake()
	{
	    _myTransform = GetComponent<Transform>();
	    _myAiPath = GetComponent<AIPath>();
	    
	    _spawnPointManager = GameObject.Find("SpawnPoints").GetComponent<SpawnPointManager>();
	    
    }

	void Update ()
    {
	    if (_spawnTransforms == null)
	    {
            _spawnTransforms = _spawnPointManager.GetSpawnTransforms();
        }

	    if (_myAiPath.TargetReached || (_myAiPath.target == null))
	    {
	        int i = (int)(Random.value * _spawnTransforms.Length);
            _myAiPath.target = _spawnTransforms[i];
	    }

	}
}
