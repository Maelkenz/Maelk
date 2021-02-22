using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteraction : NetworkBehaviour
{

    float MaxDistance = 10f;
    
    
    public GameObject Player;
    public LayerMask HitLayer;
    public Camera PlayerCam;
    RaycastHit hit;



    public GameObject PickupDestination;



    public float ThrowForce = 3f;


    public bool carryObject;
    public bool IsThrowable;



    [SerializeField]
    private GameObject PickedUp;

    [SyncVar]
    public GameObject NetworkPickup;
    





    // Start is called before the first frame update
    void FixedUpdate()
    {

        if (isLocalPlayer)
        {
            Debug.Log("Islocalplayer");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Get Keyinput E");

                InstantiatePickup();

                

               
            }



            if (Input.GetMouseButton(0))
            {
                ThrowObject();
            }

            if (Input.GetMouseButton(1))
            {

                DropObject();


            }
        }
    }
    

    
    public void InstantiatePickup()
    {

        if (isLocalPlayer)
        {


            Ray ray = PlayerCam.ScreenPointToRay(Input.mousePosition);

            //Ray directionRay = new Ray(PickupDestination.transform.position, PickupDestination.transform.forward);

            //if (Physics.Raycast(Player.transform.position, Player.transform.forward, out hit, MaxDistance, HitLayer))
            if (Physics.Raycast(ray, out hit, MaxDistance, HitLayer))
            {
                Debug.Log("Hit object with raycast");

                if (hit.collider.CompareTag("PlayerPickup"))
                {
                    Debug.Log("Hit object with Tag 'PlayerPickup'");

                    carryObject = true;
                    IsThrowable = true;

                    if (carryObject == true)
                    {


                        Debug.Log("Hit Object and trying to execute method, CmdPickupObject()");

                        PickupObject(hit.collider.gameObject);


                    }
                }


            }
        }
    }


    
    public void PickupObject(GameObject Pickup)
    {
        if (isLocalPlayer)
        {


            PickedUp = Pickup;



            Debug.Log("Executing Method 'CmdPickupObject()'");


            Debug.Log("PickedUp = " + PickedUp);
            Debug.Log("PickupDestination = " + PickupDestination);

            //SoftParenting with sending netId of player to PickableObjects

            //uint PlayerID = GetComponent<NetworkIdentity>().netId;
            //PickedUp.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            //PickedUp.GetComponent<PickableObject>().SoftParent = PickupDestination;
            //PickedUp.GetComponent<PickableObject>().CmdSetParent(PlayerID);




            //Soft Parenting with getting network ID of PickableObjects gameobject

            //uint HitID = PickedUp.GetComponent<NetworkIdentity>().netId;
            // NetworkIdentity.spawned.TryGetValue(HitID, out NetworkIdentity identity);

            // identity.GetComponent<PickableObject>().SoftParent = PickupDestination.gameObject;


            //NetworkIdentity ni = NetworkClient.connection.identity;
            //ni.GetComponent<PickableObject>().SoftParent = PickupDestination.gameObject;





            //Hard Parenting

            //PickedUp.transform.SetParent(PickupDestination.transform);
            //PickedUp.gameObject.transform.position = PickupDestination.transform.position;
            //PickedUp.GetComponent<Rigidbody>().isKinematic = true;
            //PickedUp.GetComponent<Rigidbody>().useGravity = false;



            //Hard Parenting by removing networkidentity of PickableObject gameobject


            CmdAddAuthority(Pickup);

            PickedUp.GetComponent<NetworkIdentity>().enabled = false;
            PickedUp.transform.SetParent(PickupDestination.transform);
            PickedUp.transform.position = PickupDestination.transform.position;
            PickedUp.transform.rotation = PickupDestination.transform.rotation;
            PickedUp.GetComponent<Rigidbody>().isKinematic = true;
            PickedUp.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    [Command]
    public void CmdAddAuthority(GameObject Pup)
    {
        NetworkPickup = Pup;

        Pup.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);

        NetworkServer.Spawn(Pup);



    }

    [Command]
    public void CmdRemoveAuthority()
    {
        NetworkPickup.GetComponent<NetworkIdentity>().RemoveClientAuthority();
    }

    
    public void ThrowObject()
    {
        if (isLocalPlayer)
        {


            if (IsThrowable && PickedUp != null)
            {
                carryObject = false;
                IsThrowable = false;

                PickedUp.transform.SetParent(null);
                PickedUp.GetComponent<NetworkIdentity>().enabled = true;
                PickedUp.GetComponent<PickableObject>().SoftParent = null;
                PickedUp.GetComponent<Rigidbody>().isKinematic = false;
                PickedUp.GetComponent<Rigidbody>().useGravity = true;
                PickedUp.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * ThrowForce);

                CmdRemoveAuthority();
            }
            else
            {
                return;
            }
        }
    }



   
    public void DropObject()
    {
        if (isLocalPlayer)
        {

            if (PickedUp != null)
            {
                carryObject = false;
                IsThrowable = false;

                PickedUp.transform.SetParent(null);
                PickedUp.GetComponent<NetworkIdentity>().enabled = true;
                PickedUp.GetComponent<PickableObject>().SoftParent = null;
                PickedUp.GetComponent<Rigidbody>().isKinematic = false;
                PickedUp.GetComponent<Rigidbody>().useGravity = true;

                CmdRemoveAuthority();
            }
            else
            {
                return;
            }
        }

    }




    /*
    
    void OnMouseDown()
    {
        
        if (isLocalPlayer)
        {
            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(Player.transform.position, Player.transform.forward, out hit, MaxDistance, HitLayer))
            {

                if (hit.collider.tag == "PlayerPickup")
                {
                    carryObject = true;
                    IsThrowable = true;

                    if (carryObject == true)
                    {


                        PickedUp = hit.collider.gameObject;

                        
                        PickedUp.transform.position = PickupDestination.transform.position;
                        PickedUp.transform.SetParent(PickupDestination.transform);
                        PickedUp.gameObject.transform.position = PickupDestination.transform.position;
                        PickedUp.GetComponent<Rigidbody>().isKinematic = true;
                        PickedUp.GetComponent<Rigidbody>().useGravity = false;
                    }
                }


            }






            
            RaycastHit hit;

            if (Physics.Raycast(Player.transform.position, Player.transform.forward, out hit, MaxDistance) && hit.collider.gameObject.CompareTag("PlayerPickup"))
            {
                
                hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                PickedUp = Instantiate(hit.collider.gameObject, PickupDestination.transform);
                Destroy(hit.collider);



            }
        }
        
    }

    void OnMouseUp()
    {

        carryObject = false;
        IsThrowable = false;

        PickedUp.transform.SetParent(null);
        PickedUp.GetComponent<Rigidbody>().isKinematic = false;
        PickedUp.GetComponent<Rigidbody>().useGravity = true;

        
        Instantiate(PickedUp,PickupDestination.transform);
        Destroy(PickedUp);
        
    }*/

    
}
