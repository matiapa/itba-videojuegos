using UnityEngine;
    
public class SoundEffectController : MonoBehaviour
{
    [SerializeField] private AudioClip _victoryAudioClip;
    [SerializeField] private AudioClip _defeatAudioClip;

    public AudioSource AudioSource => _audioSource;
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
