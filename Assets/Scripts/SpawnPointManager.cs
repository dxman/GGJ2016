using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public GameObject EnemyPrefab;

    private GameSettings _gameSettings;

    private SpawnPointBehavior[] _spawnPoints;
    private Transform[] _spawnTransforms;

    void Start()
    {
        _gameSettings = GameObject.Find("GameSettings").GetComponent<GameSettings>();

        _spawnPoints = GetComponentsInChildren<SpawnPointBehavior>();
        _spawnTransforms = new Transform[_spawnPoints.Length];
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnTransforms[i] = _spawnPoints[i].gameObject.GetComponent<Transform>();
        }

        GameObject player = GameObject.FindWithTag("Player");
        Transform playerTransform = player.GetComponent<Transform>();

        GameObject siren = GameObject.Find("Siren");
        Transform sirentTransform = siren.GetComponent<Transform>();

        GameObject angel = GameObject.Find("Angel");
        Transform angelTransform = angel.GetComponent<Transform>();

        int spawnIndexA;
        int spawnIndexB;
        int spawnIndexC;
        float distanceAB;
        float distanceAC;
        float distanceBC;

        do
        {
             spawnIndexA = (int)(Random.value * _spawnPoints.Length);
            spawnIndexB = (int)(Random.value * _spawnPoints.Length);
            spawnIndexC = (int)(Random.value * _spawnPoints.Length);

            distanceAB = Vector3.Distance(_spawnTransforms[spawnIndexA].position,
                _spawnTransforms[spawnIndexB].position);
            distanceAC = Vector3.Distance(_spawnTransforms[spawnIndexA].position,
                _spawnTransforms[spawnIndexC].position);
            distanceBC = Vector3.Distance(_spawnTransforms[spawnIndexB].position,
                _spawnTransforms[spawnIndexC].position);
        } while ((distanceAB < _gameSettings.SpawnDistance) || (distanceAC < _gameSettings.SpawnDistance) || (distanceBC < _gameSettings.SpawnDistance));
        
        playerTransform.position = _spawnTransforms[spawnIndexA].position;
        sirentTransform.position = _spawnTransforms[spawnIndexB].position;
        angelTransform.position = _spawnTransforms[spawnIndexC].position;

        _spawnPoints[spawnIndexA].IsOccupied = true;
        _spawnPoints[spawnIndexB].IsOccupied = true;
        _spawnPoints[spawnIndexC].IsOccupied = true;

        Quaternion enemyRotation = EnemyPrefab.GetComponent<Transform>().rotation;
        for (int i = 0; i < _gameSettings.NumEnemies; i++)
        {
            int spawnIndex;
            do
            {
                spawnIndex = (int)(Random.value * _spawnPoints.Length);
            } while (_spawnPoints[spawnIndex].IsOccupied);

            Instantiate(EnemyPrefab, _spawnTransforms[spawnIndex].position, enemyRotation);
            _spawnPoints[spawnIndex].IsOccupied = true;
        }

        Light playerLight = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Light>();
        playerLight.enabled = _gameSettings.LanternOn;
    }

    public Transform[] GetSpawnTransforms()
    {
        return _spawnTransforms;
    }
}
