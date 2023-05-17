using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject _coinsTextMesh;
    [SerializeField] private GameObject _livesTextMesh;
    [SerializeField] private GameObject _waveTextMesh;

    private void Start() {
        GameManager.instance.OnNetCoinChange += UpdateCoinsValue;
        GameManager.instance.OnNetLivesChange += UpdateLivesValue;
        EventManager.instance.OnWaveChange += UpdateCurrentWave;

        UpdateCoinsValue(GameManager.instance.Coins);
        UpdateLivesValue(GameManager.instance.Lives);
    }

    private void UpdateCoinsValue(int newCoins) {
        if (_coinsTextMesh != null) 
            _coinsTextMesh.GetComponent<TextMeshProUGUI>().text = $"{newCoins}";
    }

     private void UpdateLivesValue(int newLives) {
        if (_livesTextMesh != null)
            _livesTextMesh.GetComponent<TextMeshProUGUI>().text = $"{newLives}";
    }

    private void UpdateCurrentWave(int currentWave, int maxWave) {
        if (_waveTextMesh != null)
            _waveTextMesh.GetComponent<TextMeshProUGUI>().text = $"{currentWave} / {maxWave}";
    }
}