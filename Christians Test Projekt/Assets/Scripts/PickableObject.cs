using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickableObject : NetworkBehaviour
{
    [SyncVar]
    public GameObject SoftParent;

    public GameObject BoxPrefab;
    GameObject PickedUpObject;


    void Update()
    {
        
        CmdLift();
        Debug.Log("Parent via Update is = " + SoftParent);
        
        
        



    }

    [Command]
    public void CmdSetParent(uint ParentInput)
    {
        

        NetworkIdentity.spawned.TryGetValue(ParentInput, out NetworkIdentity identity);


        GameObject ParentIDis = identity.GetComponent<PlayerInteraction>().PickupDestination.gameObject;

        

        SoftParent = ParentIDis;

        Debug.Log("Parent is = " + SoftParent);



    }


    [Command]
    public void CmdLift()
    {
        if (transform.parent != null)
        {
            //Hard parenting with deleting object on pickup and placing it in the hands of the player, mirror official way?
            //NetworkServer.Destroy(gameObject);

            //PickedUpObject = Instantiate(BoxPrefab, transform.parent.position, transform.parent.rotation);
            //NetworkServer.Spawn(PickedUpObject);
            //PickedUpObject.transform.position = transform.parent.position;
            //PickedUpObject.transform.rotation = transform.parent.rotation;


            NetworkServer.Spawn(gameObject);
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            
        }
        else
        {
            //Destroy(PickedUpObject);
            //GameObject BoxObject = Instantiate(BoxPrefab, transform.parent.position, transform.parent.rotation);
            //NetworkServer.Spawn(BoxObject);
            BoxPrefab.GetComponent<Rigidbody>().isKinematic = false;
            BoxPrefab.GetComponent<Rigidbody>().useGravity = true;

        }



        if (SoftParent != null)
        {
            Debug.Log("Trying to move entity");
            
            transform.position = SoftParent.transform.position;
            transform.rotation = SoftParent.transform.rotation;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            

        }
        else
        {
            CmdNoParent();
        }
    }
    [Command]
    public void CmdNoParent()
    {
        
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
