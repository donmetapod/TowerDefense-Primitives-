using UnityEngine;
using UnityEngine.Events;

// Manages the general state of the game with the help of GameState
public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnGameStart;
    [SerializeField] private GameState _gameState;

    private void Start()
    {
        OnGameStart?.Invoke();
    }

    // Resets game state when closing the game or resetting the scene
    private void OnDisable()
    {
        _gameState.ResetData();
    }
}
