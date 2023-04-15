using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 60f;
    
    private float _countdown = 2f;
    private int _waveIndex = 0;

    void Update () {
        if (_waveIndex == waves.Length)
            this.enabled = false;

        if (_countdown <= 0f) {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves + waves[_waveIndex].Duration;
            return;
        }

        _countdown -= Time.deltaTime;
    }
    
    IEnumerator SpawnWave () {
        Wave wave = waves[_waveIndex];
        
        for (int i = 0; i < wave.count; i++) {
            Instantiate(wave.enemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        
        _waveIndex++;
    }

}
