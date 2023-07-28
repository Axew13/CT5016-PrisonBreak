using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{
    public AudioSource audioPlayer;
    public AudioClip eatingSound;

    public void Interact()
    {
        audioPlayer.PlayOneShot(eatingSound);
        this.gameObject.SetActive(false);
    }
}
