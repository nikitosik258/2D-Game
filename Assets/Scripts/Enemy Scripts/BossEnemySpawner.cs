using System.Collections;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Префаб врага
    public int enemiesPerWave = 3;  // Количество врагов в каждой волне
    public float timeBetweenWaves = 20f;  // Время между волнами
    public int totalWaves = 10;  // Общее количество волн


    private void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    public IEnumerator SpawnEnemyWaves()
    {
        for (int wave = 1; wave <= totalWaves; wave++)
        {
            // Ждем перед созданием следующей волны
            yield return new WaitForSeconds(timeBetweenWaves);

            // Создаем волну врагов
            SpawnEnemies(enemiesPerWave);
        }
        
    }
}
