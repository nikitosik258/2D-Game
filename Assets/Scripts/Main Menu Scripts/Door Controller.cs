using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private Animator doorAnimation;
    private AudioSource gateOpenSound;
    private void Awake()
    {
        gateOpenSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.PLAYER_TAG))
        {
            gateOpenSound.Play();
            doorAnimation.SetBool(TagManager.OPEN_ANIMATION_PARAMETER, true);
        }
    }
}
