using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SimplePlayerPickUpandDrop : NetworkBehaviour
{
    //Dette script arbejder sammen med TestCollision på PickupParent objektet og 
    //PickableObject på det opsamlede objekt, som skulle gøre så den pickableobject kunne samles op
    
    //Nuværende problem: Den lukker spillet for clienten, med beskeden at 
    //PickableObjects "Parent not set to an instance of an object"




    public GameObject Pickup = null;
    public GameObject PickupParent;
    private bool Hastriggered;

    // Start is called before the first frame update
    void Update()
    {
        if (isLocalPlayer)
        {


            if (Input.GetMouseButtonDown(0))
            {
                if (Hastriggered == true)
                {
                    Debug.Log("Getting MouseButtonDown(0)");
                    CmdPickUp();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {

                Debug.Log("Getting MouseButtonUp(0)");
                CmdDropOff();

            }
        }
    }

    


    public void PullTrigger(Collider other)
    {
        //other.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        Debug.Log("Recieved trigger input from child");
        /*if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Getting MouseButtonDown(0)");
            CmdPickUp(other.gameObject);

        }*/
        Hastriggered = true;
        Pickup = other.gameObject;
        Debug.Log("Collider has been detected as "+ other.gameObject);
    }
    public void PullTriggerExit(Collider other)
    {
        Debug.Log("Recieved exit trigger input from child");
        //Pickup.GetComponent<NetworkIdentity>().RemoveClientAuthority();
        Hastriggered = false;
        
    }

    


    /*public void OnTriggerEnter(Collider other)
    {
        // if (!other.CompareTag("PlayerPickup")) return;

        Debug.Log("Colliding with " + other);

        CmdPickUp(other.gameObject);

    }*/

    [Command]
    public void CmdPickUp()
    {
        Debug.Log("Trying CmdPickUp Command");
        
        
        //Pickup.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        Pickup.GetComponent<PickableObject>().SoftParent = PickupParent.transform.gameObject;
        Debug.Log("Set parent for " + Pickup + "as " + PickupParent);

    }

    [Command]
    public void CmdDropOff()
    {
        Debug.Log("Trying CmdDropOff Command");
        Pickup.GetComponent<PickableObject>().SoftParent = null;
        Pickup.GetComponent<PickableObject>().CmdNoParent();
        //Pickup.GetComponent<NetworkIdentity>().RemoveClientAuthority();
    }
}
