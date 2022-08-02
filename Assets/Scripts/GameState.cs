using UnityEngine;
using UnityEngine.Events;

// Manages the general state of the game with the help of GameManager
[CreateAssetMenu (fileName = "GamState", menuName = "ScriptableObjects/CreateGameStateAsset")]
public class GameState : ScriptableObject
{
    public  UnityEvent OnGameOver;
    public enum GameStateEnum
    {
        Playing,
        GameOver
    }
    [SerializeField] private GameStateEnum _currentGameState;
    public GameStateEnum CurrentGameState { get; set; }

    [SerializeField] private int _enemyCount;
    public int EnemyCount
    {
        get => _enemyCount;
        set => _enemyCount = value;
    }
    
    [SerializeField] private bool _winner;
    public bool Winner
    {
        get => _winner;
        set => _winner = value;
    }
    
    [Range(0, 5)][SerializeField] private float _gameSpeed = 1;
    public float GameSpeed
    {
        set
        {
            _gameSpeed = value;
            Time.timeScale = value;
        }
    }
    public bool GameOver
    {
        get => _gameOver;
        set
        {
            _gameOver = value;
            CurrentGameState = GameStateEnum.GameOver;
            OnGameOver?.Invoke();
        }
    }
    [SerializeField] private bool _gameOver;

    // Resets scriptable data 
    public void ResetData()
    {
        // Not using properties to avoid invoking events
        CurrentGameState = 0;
        // _enemyCount = 0;
        _winner = false;
        _gameSpeed = 1;
        _gameOver = false; 
    }

}
