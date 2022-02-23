using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    void Update()
    {
        MouseInputFollow();
    }

    void MouseInputFollow()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
        }

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.white);

    }
}
