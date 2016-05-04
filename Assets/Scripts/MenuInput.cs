using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    private float scrollDelay = 0.1f;
    private float delayCounter;
    private bool isDelaying = false;
    public int menuSelected = 0;

    private GameSettings _gameSettings;
    private AudioSource _myAudioSource;

    public AudioClip menuDownClip;
    public AudioClip menuUpClip;
    public AudioClip menuSelectClip;

    public SpriteRenderer[] menuImages = new SpriteRenderer[3];

    void Awake()
    {
        _gameSettings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
        _myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
	{
        for (int i = 0; i < menuImages.Length; i++)
        {
            menuImages[i].enabled = (i == menuSelected);
        }
        
        if (isDelaying)
        {
            delayCounter += Time.deltaTime;
            if (delayCounter >= scrollDelay)
            {
                isDelaying = false;
            }
        }
	    else
        {
	        float scrollInput = Input.GetAxis("Vertical");
	        if (scrollInput > 0.25f)
	        {
	            menuSelected--;
	            if (menuSelected < 0)
	            {
	                menuSelected = menuImages.Length - 1;
	            }
	            isDelaying = true;
	            delayCounter = 0;

	            _myAudioSource.clip = menuUpClip;
                _myAudioSource.Play(0);
	        }

            if (scrollInput < -0.25f)
            {
                menuSelected++;
                if (menuSelected >= menuImages.Length)
                {
                    menuSelected = 0;
                }
                isDelaying = true;
                delayCounter = 0;

                _myAudioSource.clip = menuDownClip;
                _myAudioSource.Play(0);
            }
        }
	    
        if (Input.GetButtonDown("Fire3"))
	    {
	        if (menuSelected == 0)
	        {
	            _gameSettings.SpawnDistance = 20;
	            _gameSettings.NumEnemies = 5;
	            _gameSettings.LanternOn = true;
	        }
            else if (menuSelected == 1)
            {
                _gameSettings.SpawnDistance = 15;
                _gameSettings.NumEnemies = 8;
                _gameSettings.LanternOn = true;
            }
            else
            {
                _gameSettings.SpawnDistance = 20;
                _gameSettings.NumEnemies = 5;
                _gameSettings.LanternOn = false;
            }

            _myAudioSource.clip = menuSelectClip;
            _myAudioSource.Play(0);

            SceneManager.LoadScene("Game");
        }
	    
	}
}
