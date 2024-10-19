using System.Collections;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // ������ �����
    public int enemiesPerWave = 3;  // ���������� ������ � ������ �����
    public float timeBetweenWaves = 20f;  // ����� ����� �������
    public int totalWaves = 10;  // ����� ���������� ����


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
            // ���� ����� ��������� ��������� �����
            yield return new WaitForSeconds(timeBetweenWaves);

            // ������� ����� ������
            SpawnEnemies(enemiesPerWave);
        }
        
    }
}
