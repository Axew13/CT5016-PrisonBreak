using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool requiresKey;
    public bool hookable;

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
                if (GameData.hasHook && hookable)
                {
                    doorAnimator.Play("HookUnlockDoor", 0);
                    unlockDoor();
                    openDoor();
                }
                else if (GameData.hasKey)
                {
                    doorAnimator.Play("KeyUnlockDoor", 0);
                    unlockDoor();
                    openDoor();
                }
                else if (GameData.hasHook)
                {
                    doorAnimator.Play("FailToUnlockDoorWithHook", 0);
                    failToUnlockDoor();
                }
                else
                {
                    failToUnlockDoor();
                }
            }
            else
            {
                doorAnimator.Play("DoorOpen", 0);
                openDoor();
            }
        }
    }

    private void unlockDoor ()
    {
        isLocked = false;
        audioSource.PlayOneShot(doorUnlockSound);
    }

    private void openDoor ()
    {
        Debug.Log("Opening door");

        isOpen = true;

        audioSource.PlayOneShot(doorOpenSound);
    }

    private void closeDoor ()
    {
        Debug.Log("Closing door");

        isOpen = false;

        audioSource.PlayOneShot(doorCloseSound);
        doorAnimator.Play("DoorClose", 0);
    }

    private void failToUnlockDoor ()
    {
        Debug.Log("Door is locked!");

        audioSource.PlayOneShot(doorLockedSound);
    }
}
