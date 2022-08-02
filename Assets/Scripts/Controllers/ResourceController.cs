using System.Collections;
using UnityEngine;

// Controls how the amount of Gold increases over time and subtracts gold when a weapon is purchased
public class ResourceController : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private ResourceData _resourceData;
    private int _selectedWeaponCost;

    private void Start()
    {
        _resourceData.Gold = _resourceData.InitialGoldAmount;
        StartCoroutine(IncreaseGoldRoutine());
    }
    
    // Increases Gold over time, this coroutine can also be inside ResourceData
    IEnumerator IncreaseGoldRoutine()
    {
        while (_gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            yield return new WaitForSeconds(_resourceData.IncreaseGoldDelay);
            _resourceData.Gold += _resourceData.IncreaseGoldAmount;
            _resourceData.OnGoldAmountChange?.Invoke(_resourceData.Gold);    
        }
    }
}
