using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{
    public int SpawnDistance = 20;
    public int NumEnemies = 5;
    public bool LanternOn = true;

	// Use this for initialization
	void Awake() {
	    DontDestroyOnLoad(this);
	}
}
