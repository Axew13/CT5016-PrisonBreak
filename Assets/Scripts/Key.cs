using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public Animator animationAnimator;
    public Animator hookShineAnimator;

    public void Interact()
    {
        GameData.hasKey = true;

        Vector3 newPosition = this.transform.position;
        newPosition.y -= 1000f;
        this.transform.position = newPosition;
    }
}
