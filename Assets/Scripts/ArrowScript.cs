using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public Rigidbody Rigidbody;

    
    public void SetToRope(Transform ropeTransform)
    {
        transform.parent = ropeTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody.isKinematic = true;
    }

    public void Shot(float velocity)
    {
        transform.parent = null;
        Rigidbody.isKinematic = false;
        Rigidbody.velocity = transform.forward * velocity;

    }
}
