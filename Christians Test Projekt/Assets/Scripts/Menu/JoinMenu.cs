using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class JoinMenu : NetworkBehaviour
{

    public InputField ServerIPAddress;
    public NetworkManager networkManager;
    public GameObject MenuCanvas;
    public GameObject JoinCanvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Button1()
    {
        ServerIPAddress.text = ServerIPAddress.text + "1";
    }
    public void Button2()
    {
        ServerIPAddress.text = ServerIPAddress.text + "2";
    }
    public void Button3()
    {
        ServerIPAddress.text = ServerIPAddress.text + "3";
    }
    public void Button4()
    {
        ServerIPAddress.text = ServerIPAddress.text + "4";
    }
    public void Button5()
    {
        ServerIPAddress.text = ServerIPAddress.text + "5";
    }
    public void Button6()
    {
        ServerIPAddress.text = ServerIPAddress.text + "6";
    }
    public void Button7()
    {
        ServerIPAddress.text = ServerIPAddress.text + "7";
    }
    public void Button8()
    {
        ServerIPAddress.text = ServerIPAddress.text + "8";
    }
    public void Button9()
    {
        ServerIPAddress.text = ServerIPAddress.text + "9";
    }
    public void Button0()
    {
        ServerIPAddress.text = ServerIPAddress.text + "0";
    }
    public void ButtonDot()
    {
        ServerIPAddress.text = ServerIPAddress.text + ".";
    }

    public void ButtonBackSpace()
    {
        ServerIPAddress.GetComponentInChildren<InputField>().text = ServerIPAddress.GetComponentInChildren<InputField>().text.Remove(ServerIPAddress.GetComponentInChildren<InputField>().text.Length - 1, 1);
    }

    public void ButtonJoinServer()
    {
        string ipAddress = ServerIPAddress.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
    }

    public void ButtonLocalhost()
    {
        ServerIPAddress.text = "localhost";
    }


    public void ButtonBack()
    {
        JoinCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }
}
