using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public Vector3 moveDirection = Vector3.forward;

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * Time.deltaTime); // move
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("wall")){
            ReflectProjectile();
        }
    }

    public void ReflectProjectile()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, moveDirection);

        if (Physics.Raycast(ray, out hit))
        {
            moveDirection = Vector3.Reflect(moveDirection, hit.normal);
        }
    }
}
