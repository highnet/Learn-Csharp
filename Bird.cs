using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public Vector3 movementV = Vector3.forward;
    [Range(0.0F, 10.0F)]
    public float alignmentRadius = 5f;

    private void FixedUpdate()
    {
        DoAlignment();
        transform.Translate(movementV * Time.deltaTime); // move
    }

    private void LateUpdate()
    {
        transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x,90,-90);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("wall")){
            ReflectProjectile();
        }
    }

    private void DoAlignment()
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, alignmentRadius);
        int neighborCount = 0;

        foreach (Collider hit in hits)
        {
            if (hit.gameObject != this.gameObject && hit.gameObject.tag.Contains("bird"))
            {
                {
                    transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x + hit.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + hit.transform.rotation.eulerAngles.y, -90);
                    neighborCount++;
                }
            }
        }
        if (neighborCount == 0)
        {
            return;
        }
        this.movementV.x /= neighborCount;
        this.movementV.y /= neighborCount;
        this.movementV.Normalize();

    }

    private void ReflectProjectile()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {

            transform.rotation = Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal),Vector3.back);
        }
    }
}
