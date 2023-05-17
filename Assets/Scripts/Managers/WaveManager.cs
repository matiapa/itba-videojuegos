using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 60f;
    
    private float _countdown = 2f;
    private int _waveIndex = 0;

    void Update () {
        if (_waveIndex < waves.Length) {
            if (_countdown <= 0f) {
                StartCoroutine(SpawnWave());
                _countdown = timeBetweenWaves + waves[_waveIndex].Duration;
                return;
            }

            _countdown -= Time.deltaTime;     
        } else {            
            if (GameObject.FindObjectsOfType<Enemy>().Length == 0)
                EventManager.instance.GameOver(true);
        }
    }    
    
    IEnumerator SpawnWave () {
        Wave wave = waves[_waveIndex];
        
        for (int i = 0; i < wave.count; i++) {
            GameObject enemyObj = Instantiate(wave.enemy, transform.position, transform.rotation);
            enemyObj.GetComponent<Enemy>().SetPath(wave.path);

            yield return new WaitForSeconds(1f / wave.rate);
        }
        
        _waveIndex++;
        EventManager.instance.WaveChange(_waveIndex, waves.Length);
    }

    [System.Serializable]
    public class Wave {
        public GameObject enemy;
        public GameObject path;
        public int count;
        public float rate;
        
        public float Duration {
            get { return count / rate; }
        }
    }

}
