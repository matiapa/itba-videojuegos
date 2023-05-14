using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Text _coinsValue;
    [SerializeField] private Text _waveValue;

    private void Start() {
        EventManager.instance.OnCoinChange += UpdateCoinsValue;
        EventManager.instance.OnWaveChange += UpdateCurrentWave;
    }

    private void UpdateCurrentWave(int currentWave, int maxWave) {
        _waveValue.text = $"{currentWave} / {maxWave}";
    }

    private void UpdateCoinsValue(int newCoins) {
        _coinsValue.text = $"$ {newCoins}";
    }
}