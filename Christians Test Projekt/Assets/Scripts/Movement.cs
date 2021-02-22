using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Movement : NetworkBehaviour
{
    //Generelle variabler
    public int Speed;
    public int JumpHeight;


    //Spiller variabler
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask groundMask;

    public CharacterController Player;

    //Tjek om spilleren er på jorden
    public bool isGrounded;

    public float Gravity = -9.82f;

    Vector3 velocity;




    //Kamera 

    [SerializeField]
    private Camera CameraMove;
    public GameObject playerCamera;

    private Vector3 cameraRotation = Vector3.zero;
    
    public float MouseSensitivity = 100f;

    public Transform PlayerBody;
    
    public float XRotation = 0;


    



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        if (isLocalPlayer)
        {
            playerCamera.SetActive(true);
        }
        else
        {
            playerCamera.SetActive(false);
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            //Player movement baseret på den akse som spilleren trykker på, på tastaturet, piletaster eller WASD
            float HorizontalInput = Input.GetAxis("Horizontal");
            float VerticalInput = Input.GetAxis("Vertical");

            Vector3 move = transform.right * HorizontalInput + transform.forward * VerticalInput;

            Player.Move(move * Speed * Time.deltaTime);

            //Spiller hop, brug en simpel fysik formel. v = hop højde * -2f * tyngdekraft
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            //Reset the velocity of the player, resetter velocitien for spilleren, så tyngdekraften er passende med den vi har i Danmark.
            velocity.y += Gravity * Time.deltaTime;

            Player.Move(velocity * Time.deltaTime);



            //Afslutter applikationen
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }


            //Kamera Bevægelse

            float MouseX = Input.GetAxisRaw("Mouse X");
            float MouseY = Input.GetAxisRaw("Mouse Y");

            //Rotation på x aksen med kameraet, skal være begrænset til 90 grader op og ned
            XRotation -= MouseY;
            XRotation = Mathf.Clamp(XRotation, -90f, 90f);

            Vector3 _cameraRotationSideWays = new Vector3(0f, MouseX, 0f) * MouseSensitivity * Time.deltaTime;
            Vector3 _cameraRotationUpDown = new Vector3(XRotation, 0f, 0f) * MouseSensitivity * Time.deltaTime;
            





            //Transform CameraMove = this.gameObject.transform.GetChild(0).GetChild(2);




            //CameraMove.transform.rotation = Quaternion.Euler(XRotation, 0, 0);

            //CameraMove.transform.Rotate(-_cameraRotationUpDown);
            CameraMove.transform.localRotation = Quaternion.Euler(XRotation, 0, 0);

            PlayerBody.Rotate(Vector3.up * MouseX);
         




        }

    }
}
