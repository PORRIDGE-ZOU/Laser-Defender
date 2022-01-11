using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    WaveConfig currentWave;
    void Start()
    {
       StartCoroutine(SpawnEnemies());
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemies()
    {
        do
        {
            foreach (WaveConfig wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                    Instantiate(currentWave.GetEnemyPrefab(i),
                            currentWave.GetStartingWaypoint().position,
                            Quaternion.Euler(0, 0, 180), transform);
                    //can't I simply dismiss quaternion and transform and change the
                    //rotation in prefab instead?
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }

        }
        while (isLooping);
    }
}
