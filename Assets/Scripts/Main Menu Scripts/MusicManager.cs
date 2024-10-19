using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Получаем компонент Audio Source
        audioSource = GetComponent<AudioSource>();

        // Запускаем воспроизведение музыки
        audioSource.Play();
    }

    void Update()
    {
        // Ваши логики управления музыкой могут быть добавлены здесь
    }
}
