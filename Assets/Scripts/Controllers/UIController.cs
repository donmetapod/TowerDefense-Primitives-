using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Controls all UI elements
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private Button[] _weaponButtons;
    [SerializeField] private TMP_Text _goldAmountText;
    [SerializeField] private TMP_Text _currentWaveText;
    [SerializeField] private ResourceData _resourceData;
    [SerializeField] private GameState _gameState;

    private void OnEnable()
    {
        _gameState.OnGameOver.AddListener(ShowGameOverScreen);
        _resourceData.OnGoldAmountChange.AddListener(CheckIfEnoughGoldForWeapon);
        _resourceData.OnGoldAmountChange.AddListener(UpdateGoldAmountUI);
    }

    private void OnDisable()
    {
        _gameState.OnGameOver.RemoveListener(ShowGameOverScreen);
        _resourceData.OnGoldAmountChange.RemoveListener(CheckIfEnoughGoldForWeapon);
        _resourceData.OnGoldAmountChange.RemoveListener(UpdateGoldAmountUI);
    }

    // Shows win or lose screen on game over
    public void ShowGameOverScreen()
    {
        if (_gameState.Winner)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _loseScreen.SetActive(true);
        }
    }
    
    public void GameSpeedSliderValueChanged(float value)
    {
        _gameState.GameSpeed = value;
    }

    public void RetryGame(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    // Everytime Gold amount changes this method is called to check if
    // weapon buttons need to be updated
    public void CheckIfEnoughGoldForWeapon(int currentGoldAmount)
    {
        for (int i = 0; i < _weaponButtons.Length; i++)
        {
            _weaponButtons[i].interactable = false;
        }

        for (int i = _weaponButtons.Length; i > 0; i--)
        {
            if (currentGoldAmount >= _resourceData.WeaponsCosts[i - 1].WeaponCost)
            {
                _weaponButtons[i-1].interactable = true;
            }
        }
    }

    public void UpdateGoldAmountUI(int currentGoldAmount)
    {
        _goldAmountText.text = $"Gold: {currentGoldAmount}";
    }
    
    public void UpdateCurrentWaveUI(int currentWaveNumber)
    {
        _currentWaveText.text = $"Current wave: {currentWaveNumber}";
    }
}
