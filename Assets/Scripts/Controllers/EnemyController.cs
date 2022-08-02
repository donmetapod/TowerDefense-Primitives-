using UnityEngine;

// This class is inside each enemy and currently only serves to
// add and subtract to the enemy count on the GameState
public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    private void OnEnable()
    {
        _gameState.EnemyCount++;
    }

    private void OnDisable()
    {
        _gameState.EnemyCount--;
    }
}
