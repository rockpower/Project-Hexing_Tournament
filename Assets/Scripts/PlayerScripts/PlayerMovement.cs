using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    // Other Class Script
    public PlayerControl playerControl;
    public Animator anim;
    public CameraScript cameraScript;
    public Hex hexScript;
    public HexMaker hexMap;

    // Public Unity Variables
    public Transform target;
    public Vector3 destination;
    public GameObject map;
    public GameObject occupiedHex;

    // Public Member Variable
    public int initialStart;
    float speed = 2;

    // Use this for initialization
    void Start()
    {
        map = GameObject.Find("HexMap");
        this.gameObject.transform.position = map.transform.GetChild(initialStart).position;
        occupiedHex = map.transform.GetChild(initialStart).gameObject;
        destination = this.gameObject.transform.position;
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        playerControl = gameObject.GetComponent<PlayerControl>();
        //destination = transform.position;

        //Retrieve Character Animator
        anim = GetComponent<Animator>();
        anim.Play("IdleAnim 1", -1);

        foreach (GameObject hex in hexMap.myHex)
        {
            if (destination == hex.transform.position)
            {
                hexScript = hex.GetComponent<Hex>();
                occupiedHex = hex;
                hexScript.isOccupied = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = cameraScript.TargetReturn();
        if (target != transform)
            return;

        // Move towards our destination

        // NOTE!  This just moves directly there, but really you'd want to feed
        // this into a pathfinding system to get a list of sub-moves or something
        // to walk a reasonable route.
        // To see how to do this, look up my TILEMAP tutorial.  It does A* pathfinding
        // and throughout the video I explain how you can apply that pathfinding
        // to hexes.

        Vector3 dir = destination - transform.position;
        Vector3 velocity = dir.normalized * speed * Time.deltaTime;

        // Make sure the velocity doesn't actually exceed the distance we want.
        velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);
        transform.Translate(velocity);

        if (velocity.x != 0 | velocity.y != 0 | velocity.z !=0)
        {
            //Play Running Animation
            anim.Play("Run_Anim_1", -1);
        } else
        {
            anim.Play("IdleAnim 1", -1);
        }

    }

    public void SetHexOccupied()
    {
        foreach(GameObject hex in hexMap.myHex)
        {
            if(destination == hex.transform.position)
            {
                hexScript = hex.GetComponent<Hex>();
                occupiedHex = hex;
                hexScript.isOccupied = true;
            }
        }
    }

    public void SetHexUnoccupied()
    {
        hexScript = occupiedHex.GetComponent<Hex>();
        hexScript.isOccupied = false;
    }
}
