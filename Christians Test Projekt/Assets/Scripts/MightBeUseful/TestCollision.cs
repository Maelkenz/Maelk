using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }




    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Offline Collided with physical " + collision);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Child Triggered by " + other.gameObject);
        gameObject.GetComponentInParent<SimplePlayerPickUpandDrop>().PullTrigger(other);
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Child exited trigger " + other);
        gameObject.GetComponentInParent<SimplePlayerPickUpandDrop>().PullTriggerExit(other);

    }
}
