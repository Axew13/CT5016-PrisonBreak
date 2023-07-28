using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool requiresKey;

    private bool isLocked = true;
    private bool isOpen = false;

    public Animator doorAnimator;
    public GameObject keyhole;
    public AudioSource audioSource;
    public AudioClip doorLockedSound;
    public AudioClip doorUnlockSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    public void Awake()
    {
        if (requiresKey)
        {
            keyhole.SetActive(true);
        }
    }

    public void Interact()
    {
        if (isOpen)
        {
            closeDoor();
        }
        else
        {
            if (isLocked && requiresKey)
            {
                if (GameData.hasHook)
                {
                    openDoor();
                }
                else
                {
                    failToOpenDoor();
                }
            }
            else
            {
                openDoor();
            }
        }
    }

    private void openDoor ()
    {
        Debug.Log("Opening door");

        isLocked = false;
        isOpen = true;

        audioSource.PlayOneShot(doorOpenSound);
        doorAnimator.Play("DoorOpen", 0);
    }

    private void closeDoor ()
    {
        Debug.Log("Closing door");

        isOpen = false;

        audioSource.PlayOneShot(doorCloseSound);
        doorAnimator.Play("DoorClose", 0);
    }

    private void failToOpenDoor ()
    {
        Debug.Log("Door is locked!");

        audioSource.PlayOneShot(doorLockedSound);
    }
}
