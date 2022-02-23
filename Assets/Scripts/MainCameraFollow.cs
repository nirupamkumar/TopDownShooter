using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private Vector3 targetOffset;

    private void Update()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);
    }
}
