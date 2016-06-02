using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    // Other Script Class
    public CameraScript cameraScript;
    public Transform target;

	// Use this for initialization
	void Start () {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
	}

	// Update is called once per frame
	void Update () {
        target = cameraScript.TargetReturn();
	}

    /*
    This is a function to call a function in the Health Script.
    Depending on what kind of power is used, it should use that to affect the 
    number damage.
    This would be the bases for the rest of the damage delt classes.
    */
    public void TakeDamage()
    {
        int damageDelt = 5;
            target.GetComponent<PlayerHealth>().TakeDamage(damageDelt);

        /*
        if (target.tag == "Player")
            playerOneHealth.TakeDamage(damageDelt);
        else if (target.tag == "Player2")
            playerTwoHealth.TakeDamage(damageDelt);
        else if (target.tag == "Player3")
            playerThreeHealth.TakeDamage(damageDelt);
            */
    }
}
