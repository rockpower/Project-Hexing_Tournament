using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Attack_Trigger : MonoBehaviour {

    public CameraScript cameraScript;
    public Text damageDelt;

    public int attackDamage;
    public bool isAttacking;

    private GameObject target;

    // Use this for initialization
    void Start()
    {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        damageDelt = GameObject.Find("DamageDelt").GetComponent<Text>();
        damageDelt.enabled = false;
        isAttacking = false;
    }

    void Update()
    {
        if(isAttacking)
        StartCoroutine(WaitToDestroy());
    }

    void OnTriggerStay(Collider col)
    {
        if (isAttacking)
        {
            damageDelt.enabled = true;
            if (col.tag == "Player")
            {
                target = GameObject.FindGameObjectWithTag("Player");
                target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
                screenPos.y += 50;
                damageDelt.rectTransform.position = screenPos;

            }

            else if (col.tag == "Player2")
            {
                target = GameObject.FindGameObjectWithTag("Player2");
                target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
                screenPos.y += 50;
                damageDelt.rectTransform.position = screenPos;
            }

            else if (col.tag == "Player3")
            {
                target = GameObject.FindGameObjectWithTag("Player3");
                target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                // print(target);
                Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
                screenPos.y += 50;
                damageDelt.rectTransform.position = screenPos;
            }
            damageDelt.text = "- " + attackDamage.ToString();
            isAttacking = false;
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        damageDelt.enabled = false;
    }

}

