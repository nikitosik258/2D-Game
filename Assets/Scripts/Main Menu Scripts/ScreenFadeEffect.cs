using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeEffect : MonoBehaviour
{
    private Image fadeImage;
    public GameObject deathScreenUI; // UI панель, содержащая текст "DEATH" и кнопки
    public float fadeDuration = 3f;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        deathScreenUI.SetActive(false); // Скрываем UI панель при старте
        gameObject.SetActive(false); // Скрываем изображение при старте
    }

    public void StartFade()
    {
        gameObject.SetActive(true); // Показываем затемняющее изображение
        StartCoroutine(FadeToRed());
    }

    private IEnumerator FadeToRed()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.7f); // Непрозрачный красный

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        // Показываем UI панель после затемнения экрана
        deathScreenUI.SetActive(true);
    }
}
