using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour, IInteractable
{
    public Animator animationAnimator;
    public Animator hookShineAnimator;

    public void Interact()
    {
        if (GameData.hasBone)
        {
            Debug.Log("You have the bone! Now you have the hook too!");
            GameData.hasHook = true;

            animationAnimator.Play("PullHookWithBone", 0);
        }
        else
        {
            Debug.Log("You don't have the bone!");
        }
    }
}
