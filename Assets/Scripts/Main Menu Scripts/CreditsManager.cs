using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public string[] lines;
    public float speedText;
    public Text dialogText;

    private int index;

    public PlayerMovement playerMovement;
    public playerShootingManager shootingManager;

    private void Start()
    {
        dialogText.text = string.Empty;
        StartDialogue();
        playerMovement.DisableMovement();
        shootingManager.DisableMovement();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(speedText);
        }
    }

    public void scipTextClick()
    {
        if(dialogText.text == lines[index])
        {
            NextLines();
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = lines[index];
        }
    }

    private void NextLines()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            playerMovement.EnableMovement();
            shootingManager.EnableMovement();
        }
    }
}
