using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth = 50f;
    public float Take_Damage = 10f;
    private float currentHealth;

    private Animator anim;
    private bool isDead = false;

    public GateController gateController;

    // Событие, уведомляющее об изменении здоровья
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);
    public event HealthChangedDelegate OnHealthChanged;

    private BossHealthUI BossHealth;

    public bool loadSceneOnDeath = false;
    public bool CageOpen = true;

    private DeathScreenManager DeathScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        // Уведомляем об изменении здоровья
        NotifyHealthChanged();

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        // Воспроизводим анимацию смерти
        anim.SetBool(TagManager.DEATH_ANIMATION_PARAMETER, true);

        // Ждем, чтобы дать время для воспроизведения анимации
        yield return new WaitForSeconds(0.1f); // Подставьте свое время в секундах

        // Отключаем врага или уничтожаем его объект
        gameObject.SetActive(false);
        // Или использовать: Destroy(gameObject);

        if (loadSceneOnDeath)
        {
            //yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("OpenWindow");
        }

        if(CageOpen)
        { // Уведомляем контроллер ворот, что враг убит
            gateController.EnemyKilled();
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            // Если враг соприкасается с игроком, уменьшаем здоровье игрока
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(Take_Damage); // Подставьте свой урон
        }
    }

    // Метод для уведомления о изменении здоровья
    private void NotifyHealthChanged()
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(currentHealth, maxHealth);
        }
    }

    // Добавляем метод, который будет проверять, мертв ли враг
    public bool IsDead()
    {
        return isDead;
    }

}
