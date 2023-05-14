using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] private Text _gameoverMessage;

    private void Start() {
        EventManager.instance.OnGameOver += OnGameOver;
        _gameoverMessage.text = string.Empty;
    }

    private void OnGameOver(bool isVictory) {
        _gameoverMessage.text = isVictory ? "Victoria" : "Derrota";
        _gameoverMessage.color = isVictory ? Color.cyan : Color.red;

        Invoke("LoadEndgameScene", 3f);
    }

    private void LoadEndgameScene() => SceneManager.LoadScene(2);
}