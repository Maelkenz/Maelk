using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class MouseMovement : NetworkBehaviour
{



    
    public float MouseSensitivity = 100f;

    public Transform PlayerBody;


    public float XRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        
            Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


            XRotation -= MouseY;
            XRotation = Mathf.Clamp(XRotation, -90f, 90f);


            transform.localRotation = Quaternion.Euler(XRotation, 0, 0);

            PlayerBody.Rotate(Vector3.up * MouseX);
            return;
        }
    }

    
}
