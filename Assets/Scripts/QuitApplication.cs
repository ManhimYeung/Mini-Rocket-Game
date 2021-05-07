using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        QuitApp();
    }
    void QuitApp()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
