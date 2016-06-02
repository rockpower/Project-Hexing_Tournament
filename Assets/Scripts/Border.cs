using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Destroy(GetComponent<BoxCollider>());
        // Destroy(gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {

       
    }

    /*
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Border")
        {
            print("hitting");
        }
        if (col.tag == "Player")
        {
            print("working");
            print(this.gameObject);
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        print("No longer in contact with " + collisionInfo.transform.name);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
            other.attachedRigidbody.AddForce(Vector3.up * 10);

    }

    */
}
