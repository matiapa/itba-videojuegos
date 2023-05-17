using UnityEngine;
    
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _victoryAudioClip;
    [SerializeField] private AudioClip _defeatAudioClip;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        EventManager.instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver(bool isVictory)
    {
        _audioSource.PlayOneShot(isVictory? _victoryAudioClip : _defeatAudioClip);
    }
}
