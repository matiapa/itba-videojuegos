using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 60f;
    
    private float countdown = 2f;
    private int waveIndex = 0;

    void Update () {
        if (waveIndex == waves.Length)
            this.enabled = false;

        if (countdown <= 0f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves + waves[waveIndex].Duration;
            return;
        }

        countdown -= Time.deltaTime;
    }
    
    IEnumerator SpawnWave () {
        Wave wave = waves[waveIndex];
        
        for (int i = 0; i < wave.count; i++) {
            Instantiate(wave.enemy, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        
        waveIndex++;
    }

}
