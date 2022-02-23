using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotate : MonoBehaviour
{
    [Range(0,1)]
    public float rotateSpeed = 1f;
    
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed);
    }
}
