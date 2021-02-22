using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Interaction : NetworkBehaviour
{
    public float Radius = 3f;
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }


    private void FixedUpdate()
    {
        
        

    }

    void OnMouseDown()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        
        foreach (GameObject go in gos)
        {
            Vector3 position = transform.position;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().isKinematic = true;
                this.transform.position = go.transform.position;//GameObject.FindGameObjectWithTag("PlayerPickup").transform.position;
                this.transform.parent = go.transform;//GameObject.FindGameObjectWithTag("PlayerPickup").transform;
            }
        }
    }


    void OnMouseUp()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                this.transform.parent = null;
            }
        }
    }

}
