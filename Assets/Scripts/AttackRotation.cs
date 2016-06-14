using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackRotation : MonoBehaviour
{
    public MouseManager mouseManager;
    public GameObject mainCamera;
    private Vector2 direction;
    private Vector2 mousePosition;
    private Transform _transform;
    private float angle;


    void Start()
    {
        _transform = transform;
        mainCamera = GameObject.Find("MainCamera");
        //////////////////////////mouseManager = GameObject.Find("GameManager").GetComponent<MouseManager>();
        // Requires the block to be directly to the right of the center
        //   with rotation set correctly on start

    }

    void Update()
    {
        Vector3  vPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mainCamera.transform.eulerAngles.y < 90 || mainCamera.transform.eulerAngles.y > 270)
        {
            if (vPos.x > 0.5f)
            {
                if (vPos.y > 0.4f && vPos.y < 0.6f)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }

                else if (vPos.y < 0.4f)
                {
                    transform.localEulerAngles = new Vector3(0, 60, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 300, 0);
                }
            }

            if (vPos.x < 0.5f)
            {
                if (vPos.y > 0.4f && vPos.y < 0.6f)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }

                else if (vPos.y < 0.4f)
                {
                    transform.localEulerAngles = new Vector3(0, 120, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 240, 0);
                }
            }
        }

        else if (mainCamera.transform.eulerAngles.y < 270 || mainCamera.transform.eulerAngles.y > 90)
        {
            print(vPos);
            if (vPos.x > 0.5f)
            {
                if (vPos.y > 0.4f && vPos.y < 0.6f)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }

                else if (vPos.y < 0.4f)
                {
                    transform.localEulerAngles = new Vector3(0, 240, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 120, 0);
                }
            }

            if (vPos.x < 0.5f)
            {
                if (vPos.y > 0.4f && vPos.y < 0.6f)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }

                else if (vPos.y < 0.4f)
                {
                    transform.localEulerAngles = new Vector3(0, 300, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 60, 0);
                }
            }
        }
    }
}