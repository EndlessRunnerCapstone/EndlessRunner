using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRope_Rotate : MonoBehaviour {

    public float rotateSpeed = 10.0f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * -rotateSpeed);
    }
}
