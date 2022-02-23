using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 30f;
    public Vector3 direction = Vector3.zero;

    private void OnEnable()
    {
        Invoke("Destroy", 3f); 
    }

    public void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

   public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Destroy()
    {
        var poolable = GetComponent<Poolable>();
        if (poolable != null)
            poolable.ReturnToPool();

    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    //GameObject bullet = Instantiate(this.gameObject, transform.position, transform.rotation);
    //Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //rb.AddForce(-transform.up* bulletSpeed, ForceMode.Impulse);

}
