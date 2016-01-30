using UnityEngine;
using XInputDotNetPure;

public enum GamePhase { Explore, Battle, Pause };

[System.Serializable]
public struct GameStatus
{
    public GamePhase Phase;
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameStatus _gameStatus;

    private bool _pauseReleased;

    void Awake()
    {
        DontDestroyOnLoad(this);

        _pauseReleased = true;
    }

    void Update()
    {
        if (!Input.GetButton("Pause"))
        {
            _pauseReleased = true;
        }
        else if (_pauseReleased)
        {
            _pauseReleased = false;
            if (_gameStatus.Phase == GamePhase.Explore)
            {
                _gameStatus.Phase = GamePhase.Pause;
            }
            else if (_gameStatus.Phase == GamePhase.Pause)
            {
                _gameStatus.Phase = GamePhase.Explore;
            }
        }
    }

    void OnDestroy()
    {
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
    }

    public GameStatus GetGameStatus()
    {
        return _gameStatus;
    }
}
