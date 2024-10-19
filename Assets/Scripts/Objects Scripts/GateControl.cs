using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    public int enemiesToKill = 5; // Количество врагов, которых нужно убить, чтобы открыть ворота

    private int killedEnemies = 0; // Количество уже убитых врагов

    [SerializeField]
    private GameObject gateObject; // Ссылка на объект ворот

    [SerializeField]
    private AudioSource gateAudioSource;

    private void Start()
    {
        gateObject.SetActive(true); // Изначально ворота видимы
    }

    // Вызывается при убийстве врага
    public void EnemyKilled()
    {
        killedEnemies++;

        // Проверяем, достигнуто ли количество убитых врагов для открытия ворот
        if (killedEnemies >= enemiesToKill)
        {
            StartCoroutine(OpenGate());

            // Проверка, что AudioSource был присвоен
            if (gateAudioSource != null)
            {
                // Воспроизведение аудио при открытии ворот
                gateAudioSource.Play();
            }
        }
    }

    // Корутина для плавного открытия ворот
    IEnumerator OpenGate()
    {

        yield return new WaitForSeconds(0.5f); // Пауза для эффектов (пример)

        gateObject.SetActive(false); // Выключаем объект ворот
    }
}
