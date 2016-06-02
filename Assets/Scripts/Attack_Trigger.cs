using UnityEngine;
using System.Collections;

public class Attack_Trigger : MonoBehaviour {

    public CameraScript cameraScript;

    public int attackDamage;
    public bool isAttacking;

    private GameObject target;

    // Use this for initialization
    void Start()
    {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        isAttacking = false;
    }

    void Update()
    {
    }

    void OnTriggerStay(Collider col)
    {
        if (isAttacking)
        {
            if (col.tag == "Player")
            {
                target = GameObject.FindGameObjectWithTag("Player");
                target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }

            else if (col.tag == "Player2")
            {
                target = GameObject.FindGameObjectWithTag("Player2");
                target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }

            else if (col.tag == "Player3")
            {
                target = GameObject.FindGameObjectWithTag("Player3");
                target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                // print(target);
            }
            isAttacking = false;
        }
    }

}

