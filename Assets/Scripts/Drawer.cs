using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    public Animator drawerAnimator;

    public AudioSource audioPlayer;
    public AudioClip drawerOpenSound;

    public void Interact()
    {
        if (!GameData.drawerOpened)
        {
            drawerAnimator.Play("DrawerOpen", 0);
            audioPlayer.PlayOneShot(drawerOpenSound);

            GameData.drawerOpened = true;
        }
    }
}
