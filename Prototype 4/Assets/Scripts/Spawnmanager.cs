using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawnmanager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;
    private float spawnRange = 9.0f;
    public int waveNumber = 1;
    private int score = 0;
    private int pointToadd;

    // Start is called before the first frame update
    void Start()
    {
       // SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        IncrementWaveNumber();
    }

    Vector3 GenerateSpawnPosition(){
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0.5f , spawnPosZ);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn){
        for(int i = 0; i < enemiesToSpawn ; i++){
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void IncrementWaveNumber(){
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0){
            UpdateScore(waveNumber);
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: "+score;
    } 

}


