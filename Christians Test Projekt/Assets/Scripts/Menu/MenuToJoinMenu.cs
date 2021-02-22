using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToJoinMenu : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject JoinCanvas;


    private void Start()
    {
        JoinCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void JoinMenuLoader()
    {
        MenuCanvas.SetActive(false);
        JoinCanvas.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
