using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour

/*
FIX:
When the game starts, assign them to the hex so
the other player cannot move there
*/
{

    // Other Scipt Class
    public PlayerScript playerOneScript;
    public PlayerScript playerTwoScript;
    public PlayerScript playerThreeScript;

    // Public Unity Variables
    public Button endTurn;
    public Text endTurnText;
    public float switchSpeed;
    // The target we are following
    [SerializeField]
    private Transform target;

    // Public Member Variables
    // The distance in the x-z plane to the target
    [SerializeField]
    private float distance = 13.0f;
    // the height we want the camera to be above the target
    //[SerializeField]
    private float height = 5.0f;
    private float xRotate;
    public bool isActive;
    [SerializeField]
    private float rotationDamping = 3f;
    [SerializeField]
    private float heightDamping;
    public float speedH;
    public float speedV;

    private float xRotation = 0f;
    //private float yRotation = 0f;

    // Use this for initialization
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        playerOneScript.isTargeted = true;
        playerOneScript.actionPoint = 6;
        isActive = false;
        endTurn.enabled = false;
        endTurnText.enabled = false;
    }

    void Start()
    {
        target.GetComponent<PlayerMovement>().SetHexUnoccupied();
        target.GetComponent<PlayerScript>().roundCounter += 1;
    }

    void Update()
    {
        if (!target)
            return;

        noMoreAction();

        if (Input.GetMouseButton(1))
        {
            xRotation += speedH * Input.GetAxis("Mouse X");
            //yRotation -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(0, xRotation, 0.0f);
        }

        // Gets the input of the mouse scroll
        float mouseScroll = Input.mouseScrollDelta.y;

        // Calculate the distance and how mant times the mouse scroll happened
        distance = distance - mouseScroll;

        int dist = (int)distance; // Convert float distance to an int to use in switch-case statement
        switch (dist)
        {
            case 4:
                height = 2.5f;
                break;
            case 5:
                height = 3f;
                break;
            case 6:
                height = 4f;
                break;
            case 7:
                height = 5f;
                break;
            case 8:
                height = 6f;
                break;
            case 9:
                height = 7f;
                break;
            case 10:
                height = 9f;
                break;
            case 11:
                height = 10f;
                break;
            case 12:
                height = 11f;
                break;
            case 13:
                height = 13;
                break;
        }

        //Locks the distance at eight 
        if (distance > 13.1f)
        {
            distance = 13.0f;
        }

        // Locks the distance at four
        if ((distance < 3.9f && distance > 0.1f) || (distance < 0))
        {
            distance = 4.0f;
        }

        // Changes the mouse depending on how the mouse is looking
        if (Input.GetMouseButtonDown(2))
        {
            if (distance > 0.1f)
            {
                distance = 0.0f;
                height = 9f;
            }

            else
            {
                distance = 8.0f;
            }

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
            return;

        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = -(target.position.y + height);

        var currentRotationAngleY = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        /* 
         var wantedRotationAngleX = target.eulerAngles.x;
         var wantedHeightX = target.position.x + height;
         var currentRotationAngleX = transform.eulerAngles.x;
         var currentXHeight = transform.position.x;
         */

        // Damp the rotation around the y-axis
        currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngle, rotationDamping * Time.deltaTime);
        //currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, rotationDamping * Time.deltaTime);
        // Debug.Log(currentRotationAngleY);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        // Debug.Log("Current Height: " + currentHeight);

        // Convert the angle into a rotation
        //var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);
        var currentRotation = Quaternion.Euler(0, currentRotationAngleY, 0);
        //Debug.Log(currentRotation);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * (distance);
        // Debug.Log(transform.position);

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        // Debug.Log("New Position:" + transform.position);


        // Always look at the target
        transform.LookAt(target);



    }

    // Function to show the end turn text when action point is not done yet
    public void ShowEndTurn()
    {
        target.GetComponent<PlayerScript>().actionPointText.enabled = false;
        endTurnText.color = target.GetComponent<PlayerScript>().playerColor;
        endTurnText.enabled = true;
        endTurn.enabled = true;
    }


    // Function to show action point when mouse leaves the mouse leave
    public void ShowActionPoint()
    {
        target.GetComponent<PlayerScript>().actionPointText.enabled = true;
        endTurnText.enabled = false;
        endTurn.enabled = false;
    }

    /*
    Returns the target of the camera.
    This will determine if the player is allowed to move or not.
    */
    public Transform TargetReturn()
    {
        return target;
    }

    /* 
    Switches between Players one, two and three
    The code now only assumes that there are only three players
    And will always be three players
    */
    public void SwitchTarget()
    {
        if (target.tag == "Player")
        {
            //  print(transform.position);
            
            playerTwoScript.actionPoint = 6;
            target = GameObject.FindGameObjectWithTag("Player2").transform;
            playerOneScript.DestroyHex();
            playerTwoScript.SetToTrue();
            playerTwoScript.checkRound();
            playerTwoScript.roundCounter += 1;
            playerOneScript.GetComponent<PlayerMovement>().SetHexOccupied();
            playerTwoScript.GetComponent<PlayerMovement>().SetHexUnoccupied();
        }

        else if (target.tag == "Player2")
        {
            playerThreeScript.actionPoint = 6;
            target = GameObject.FindGameObjectWithTag("Player3").transform;
            playerTwoScript.DestroyHex();
            playerThreeScript.SetToTrue();
            playerThreeScript.checkRound();
            playerThreeScript.roundCounter += 1;
            playerTwoScript.GetComponent<PlayerMovement>().SetHexOccupied();
            playerThreeScript.GetComponent<PlayerMovement>().SetHexUnoccupied();
        }


        else if (target.tag == "Player3")
        { 
            playerOneScript.actionPoint = 6;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            playerThreeScript.DestroyHex();
            playerOneScript.SetToTrue();
            playerOneScript.checkRound();
            playerOneScript.roundCounter += 1;
            playerThreeScript.GetComponent<PlayerMovement>().SetHexOccupied();
            playerOneScript.GetComponent<PlayerMovement>().SetHexUnoccupied();
        }

        endTurn.enabled = false;
        endTurnText.enabled = false;
        isActive = false;
    }

    public void noMoreAction()
    {
        if (target.tag == "Player")
        {
            if (playerOneScript.health > 0)
            {
                if (playerOneScript.actionPoint == 0)
                {
                    playerTwoScript.actionPoint = 6;
                    endTurnText.color = playerOneScript.playerColor;
                    target.GetComponent<PlayerScript>().actionPointText.enabled = false;
                    endTurnText.enabled = true;
                }
            }
            else
            {
                playerTwoScript.actionPoint = 6;
                target = GameObject.FindGameObjectWithTag("Player2").transform;
                playerOneScript.DestroyHex();
                playerTwoScript.SetToTrue();
            }
        }
        else if (target.tag == "Player2")
        {
            if (playerTwoScript.health > 0)
            {
                if (playerTwoScript.actionPoint == 0)
                {
                    playerThreeScript.actionPoint = 6;
                    endTurnText.color = playerTwoScript.playerColor;
                    target.GetComponent<PlayerScript>().actionPointText.enabled = false;
                    endTurnText.enabled = true;
                    endTurn.enabled = true;
                }
            }

            else
            {
                playerThreeScript.actionPoint = 6;
                target = GameObject.FindGameObjectWithTag("Player3").transform;
                playerTwoScript.DestroyHex();
                playerThreeScript.SetToTrue();
            }
        }
        
        else if (target.tag == "Player3")
        {
            if (playerThreeScript.health > 0)
            {
                if (playerThreeScript.actionPoint == 0)
                {
                    playerOneScript.actionPoint = 6;
                    endTurnText.color = playerThreeScript.playerColor;
                    target.GetComponent<PlayerScript>().actionPointText.enabled = false;
                    endTurnText.enabled = true;
                    endTurn.enabled = true;
                }
            }

            else
            {
                playerOneScript.actionPoint = 6;
                target = GameObject.FindGameObjectWithTag("Player").transform;
                playerThreeScript.DestroyHex();
                playerOneScript.SetToTrue();
            }
        }
    }
    
    IEnumerator waitForTurnEnd(GameObject newTarget, PlayerScript p1, PlayerScript p2)
    {
        yield return new WaitForSeconds(1.5f);

        p1.DestroyHex();
        p2.SendToUi();
        target = newTarget.transform;

    }

    IEnumerator waitForEndTurnColor(PlayerScript playerScript)
    {
        yield return new WaitForSeconds(1);
        endTurn.image.color = playerScript.playerColor;
        yield return null;
    }

    public void HealthCheck()
    {

    }
}


