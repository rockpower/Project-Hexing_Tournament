using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    bool isSecondary;
    public GameObject PlayerCamera;
    public GameObject TopCamera;

    //Initiallization
    void Start()
    {
        isSecondary = false;
    }

    //Camera Switch
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
            isSecondary = !isSecondary;

        if (isSecondary == false)
        {
            PlayerCamera.gameObject.SetActive(true);
            TopCamera.gameObject.SetActive(false);
        }
        else
        {
            PlayerCamera.gameObject.SetActive(false);
            TopCamera.gameObject.SetActive(true);
        }
    }
}
