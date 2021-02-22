using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ObjectSpawner : NetworkBehaviour
{
    public GameObject BoxPrefab;
    // Update is called once per frame

    
    void Start()
    {
        //GameObject BoxObject = Instantiate(BoxPrefab, this.transform.position, this.transform.rotation);
        NetworkServer.Spawn(BoxPrefab);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
