using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class winPrompt : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {


            SceneManager.LoadScene("MainMenu");
        }
    }
	
}
