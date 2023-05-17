using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [SerializeField] private Wave[] waves;
    
    private float _countdown = 0;
    private int _waveIndex = 0;

    public bool LastWave => _waveIndex == waves.Length;

    void Start () {
        _countdown = waves[_waveIndex].countdown;
    }

    void Update () {
        if (_waveIndex == waves.Length)
            return;
        
        if (_countdown <= 0f) {
            StartCoroutine(SpawnWave());
            _countdown = (_waveIndex < waves.Length-1 ? waves[_waveIndex+1].countdown : 0) + waves[_waveIndex].Duration;
            return;
        }

        _countdown -= Time.deltaTime;   
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
        public int countdown;
        
        public float Duration {
            get { return count / rate; }
        }
    }

}
