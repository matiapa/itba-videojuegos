using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    static public EventManager instance;

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public event Action<bool> OnGameOver;
    public event Action<int> OnCoinChange;
    public event Action<int, int> OnWaveChange;


    public void GameOver(bool isVictory)  {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    public void CoinChange(int newCoins) {
        if (OnCoinChange != null) OnCoinChange(newCoins);
    }

    public void WaveChange(int currentWave, int maxWave) {
        if (OnWaveChange != null) OnWaveChange(currentWave, maxWave);
    }
}