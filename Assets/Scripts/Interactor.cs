using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Interface for interactable objects to "extend" or inherit
 */
interface IInteractable
{
    public void Interact();
}
interface IJumpable
{
}

public class Interactor : MonoBehaviour
{
    public MyCursor cursor;
    public Transform source;
    public float range;

    void Update()
    {
        InteractMode mode = InteractMode.NORMAL;

        /*
         * Create a ray with the position and direction of the source game object
         */
        Ray rc = new Ray(source.position, source.forward);
        if (Physics.Raycast(rc, out RaycastHit info, range))
        {
            if (info.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
                else
                {
                    mode = InteractMode.INTERACT;
                }
            }
            else if (info.collider.gameObject.TryGetComponent(out IJumpable jumpObj))
            {
                mode = InteractMode.JUMP;
            }
        }
        cursor.SetInteract(mode);
    }
}
