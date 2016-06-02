using UnityEngine;
using System.Collections;

public class AttackRotation : MonoBehaviour
{
    public MouseManager mouseManager;
    private Vector2 direction;
    private Vector2 mousePosition;
    private Transform _transform;
    private float angle;


    void Start()
    {
        _transform = transform;
        //////////////////////////mouseManager = GameObject.Find("GameManager").GetComponent<MouseManager>();
        // Requires the block to be directly to the right of the center
        //   with rotation set correctly on start

    }

    void Update()
    {
        /////////transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        Vector3  vPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if(vPos.x > 0.5f)
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
        /*
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - (Vector2)_transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        print(direction.x);
        
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //////////////////////////print(mousePosition);
        _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        /*
         float mousePos = Input.GetAxis("Mouse Y");
         print(mousePos);
        
         if (mousePos > 0 && mousePos < 60)
         {
             //transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime * 5f);
             transform.localEulerAngles = new Vector3(0, 60, 0);
         }

         else if (mousePos > 60 && mousePos < 120)
         {
             //transform.Rotate(new Vector3(0, 120, 0) * Time.deltaTime * 5f);
             transform.localEulerAngles = new Vector3(0, 120, 0);
         }

         else if (mousePos > 120 && mousePos < 180)
         {
             //transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime * 5f);
             transform.localEulerAngles = new Vector3(0, 180, 0);
         }

         /*
         Vector3 centerScreenPos = Camera.main.WorldToScreenPoint(center.position);
         Vector3 dir = Input.mousePosition - centerScreenPos;
         float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
         Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
         transform.position = center.position + q * v;
         transform.rotation = q;
         */
    }
}