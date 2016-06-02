using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DarkScript : MonoBehaviour {

    /*
    Applies to all attacking script;
    If the hex is clicked, it gives a check to see if the player has enough action points
    If the player has enough action points, the player is allowed to use the attack.
    The hex will be highlighted once the player enough action points and is clicked.

    As of now, the player can click anywhere and the action point will be taken away.
    If the player right clicks, the attack will be cancled.
   
    
    Not allow the player to move when the hex is selected. 
    The player can currently move along with the attack.
    Disable the movement once a hex is selected.

    Needs to be done:
    If the player clicks on a highlight area of attack, the attack will happen.

    Find a way to disable the current active hex if a new hex is pressed
    Right now, the players cannot select a new hex if a hex is already selected
     */

    // Other Class Sript
    private CameraScript cameraScript;
    private PlayerScript playerScript;
    private PlayerMovement playerMovementScript;
    private UIHighlightHex UIHighLightScript;
    private MouseManager mouseManager;

    // Public Unity Variabls
    public RawImage darkHex;
    public Texture unHighlighted;
    public Texture highlighted;
    public Texture selected;

    // Variables needed for attack/defense
    public GameObject currentHex;

    // Public Member Variables
    public bool isClicked;


    void Awake()
    {
        darkHex = GetComponent<RawImage>();
    }

    void Start()
    {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        playerScript = cameraScript.TargetReturn().GetComponent<PlayerScript>();
        playerMovementScript = playerScript.GetComponent<PlayerMovement>();
        mouseManager = GameObject.Find("GameManager").GetComponent<MouseManager>();
        // print(playerscript);
        
        //UIHighLightScript = parentHex.gameObject.GetComponent<UIHighlightHex>();
        //print(UIHighLightScript);
        isClicked = false;
    }

    void Update()
    {
        // parentHex = darkHex.transform.parent.GetComponent<RawImage>();
        if (isClicked)
        {
            // UIHighLightScript.Dark();
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //print("Over Hex");
                return;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                Destroy(this.gameObject);
                // UIHighLightScript.NoShow();
                playerScript.actionPoint -= 2;
                isClicked = false;
                cameraScript.isActive = false;
                playerScript.occultCloak = true;
                GetComponent<RawImage>().texture = unHighlighted;
            }

            else if (Input.GetMouseButtonDown(1))
            {
                // UIHighLightScript.NoShow();
                isClicked = false;
                cameraScript.isActive = false;
                GetComponent<RawImage>().texture = unHighlighted;
            }
            /*
            if (cameraScript.firstActive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(this.gameObject);
                    UIHighLightScript.NoShow();
                    playerscript.actionPoint -= 1;
                    cameraScript.firstActive = false;
                    cameraScript.secondActive = true;
                }

                else if (Input.GetMouseButtonDown(1))
                {
                    UIHighLightScript.NoShow();
                    isClicked = false;
                    cameraScript.firstActive = false;
                    cameraScript.secondActive = true;
                }


                if (cameraScript.secondActive)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Destroy(this.gameObject);
                        UIHighLightScript.NoShow();
                        playerscript.actionPoint -= 1;
                        cameraScript.secondActive = false;
                        cameraScript.firstActive = true;
                    }

                    else if (Input.GetMouseButtonDown(1))
                    {
                        UIHighLightScript.NoShow();
                        isClicked = false;
                        cameraScript.secondActive = false;
                        cameraScript.firstActive = true;
                    }
                }
            }
            */
        }
    }

    public void mouseHover()
    {
        GetComponent<RawImage>().texture = highlighted;
    }

    public void mouseExit()
    {
        if (!isClicked)
            GetComponent<RawImage>().texture = unHighlighted;
    }

    public void Clicked()
    {
        if (playerScript.actionPoint >= 1 && !cameraScript.isActive)
        {
            /*
            if(cameraScript.firstActive && !isClicked)
            {
                isClicked = true;
                cameraScript.firstActive = false;
                cameraScript.secondActive = true;
            }

            if (cameraScript.firstActive && isClicked)
            {
                isClicked = false;
                cameraScript.firstActive = false;
                cameraScript.secondActive = true;
            }


            else if (cameraScript.secondActive)
            {
                isClicked = true;
                cameraScript.firstActive = true;
                cameraScript.secondActive = false;;
            }
            */

            isClicked = true;
            cameraScript.isActive = true;
            GetComponent<RawImage>().texture = selected;
            currentHex = mouseManager.CurrentOccupiedHex();
            // print(currentHex);
            // UIHighLightScript = parentHex.gameObject.GetComponent<UIHighlightHex>();

            // print(isClicked);
        }
        
        // print(this.gameObject.transform.name);
    }
}
