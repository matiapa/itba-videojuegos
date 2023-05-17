using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private int _initialLives;
    [SerializeField] private int _initialCoins;
    
    private int _lives;
    private int _coins;

    public event Action<int> OnNetCoinChange;
    public event Action<int> OnNetLivesChange;

    public int Lives => _lives;
    public int Coins => _coins;
    static public GameManager instance;

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    private void Start() {
        EventManager.instance.OnCoinChange += OnCoinChange;
        EventManager.instance.OnLivesChange += OnLivesChange;
        EventManager.instance.OnGameOver += OnGameOver;

        _lives = _initialLives;
        _coins = _initialCoins;
    }

    private void OnCoinChange(int coinChange) {
        _coins += coinChange;
        
        if(OnNetCoinChange != null) OnNetCoinChange(_coins);
    }

    private void OnLivesChange(int livesChange) {
        _lives += livesChange;

        if(OnNetLivesChange != null) OnNetLivesChange(_lives);

        if(_lives < 0)
            EventManager.instance.GameOver(false);
    }

    private void OnGameOver(bool isVictory) {
        if (isVictory)
            SceneManager.LoadScene("VictoryScene");
        else
            SceneManager.LoadScene("DefeatScene");
    }
}