using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour {

    // Other Script Cass
    public PlayerControl playerControl;
    public PlayerMovement target;
    public CameraScript cameraScript;
    public HexMaker map;

    // Public Unity Variable
    public List<GameObject> hexNeighbor;
    public GameObject currentHex;
    public Hex hexScript;
    private Hex localHexScript;
    public Material defaultMaterial;
    public Material highlightMaterial;

    void Start()
    {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Is the mouse over a Unity UI Element?
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // It is, so let's not do any of our own custom
            // mouse stuff, because that would be weird.

            // NOTE!  We might want to ask the system WHAT KIND
            // of object we're over -- so for things that aren't
            // buttons, we might not actually want to bail out early.

            return;

        }

        target = cameraScript.TargetReturn().GetComponent<PlayerMovement>();
        if (cameraScript.isActive)
            return;

        foreach (GameObject hex in map.myHex)
        {
            if (target.transform.position == hex.transform.position)
            {
                hexScript = hex.GetComponent<Hex>();
                hexNeighbor = hexScript.GetNeighbours(hex);
                currentHex = hex;
            }
        } 

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject ourHitObject = hitInfo.collider.transform.gameObject;

            // So...what kind of object are we over?
            if (ourHitObject.GetComponent<Hex>() != null)
            {
                // Ah! We are over a hex!
                MouseOver_Hex(ourHitObject);
            }
        }
    }

    void MouseOver_Hex(GameObject ourHitObject)
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject hex in hexNeighbor)
            {
               
               // hex.GetComponent<Renderer>().material = defaultMaterial;
                // We could also check to see if we're clicking on the thing.

                // We have clicked on a hex.  Do something about it!
                // This might involve calling a bunch of other functions
                // depending on what mode you happen to be in, in your game.
                // We're just gonna colorize the hex, as an example.
                /*
                Might use this code segment later down the line
                MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
                if (mr.material.color == Color.red)
                {
                    mr.material.color = Color.white;
                }
                else {
                    mr.material.color = Color.red;
                }
                */

                // If we have a unit selected, let's move it to this tile!

                if (hex == ourHitObject && target.GetComponent<PlayerScript>().actionPoint > 0)
                {
                    localHexScript = hex.GetComponent<Hex>();
                    if (!localHexScript.isOccupied)
                    {
                        target.GetComponent<PlayerScript>().actionPoint -= 1;
                        target.destination = ourHitObject.transform.position;
                        // mainCamera.noMoreAction();
                    }
                }
            }
        }
    }

    public GameObject CurrentOccupiedHex()
    {
        return currentHex;
    }
}
