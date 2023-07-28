using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        /*
         * This is done because having a camera directly on a RigidBody object can be buggy.
         */
        transform.position = cameraPosition.position;
    }
}
