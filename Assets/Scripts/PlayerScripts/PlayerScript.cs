using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    //Other scipt to use their functions
    public UI_Hexes uiScript;
    public CameraScript cameraScript;
    public PlayerHealth healthScript;
    public PlayerMovement playerMovement;

    // Public Unity Variables
    public Transform target;
    public Text actionPointText;
    public Color playerColor;

    public GameObject parentOne;
    public GameObject parentTwo;
    public GameObject parentThree;
    public GameObject parentFour;
    public GameObject parentFive;
    public GameObject parentSix;

    // Public Member Variables
    public List <int> spawnHex;
    public int health;
    public float actionPoint;
    public bool isTargeted;
    public bool occultCloak;
    public float roundCounter;
    
    void Awake()
    {
        spawnHex = new List<int>();
        for(int index = 0; index < 6; ++index)
        {
            spawnHex.Add(Random.Range(1, 7));
        }

        for(int index = 0; index < 6; ++index)
        {
            for(int value = 0; value < 6; ++value)
            {
                if (index == value)
                {
                    continue;
                }

                else if(spawnHex[index] == spawnHex[value])
                {

                    while (spawnHex[index] == spawnHex[value])
                    {
                        spawnHex[index] = Random.Range(1, 7);
                    } 
                     value = 0;
                }
            }
        }

        /*
        for(int i = 0; i < spawnHex.Count; ++i)
        {
            print(spawnHex[i]);
        }
        */
        /*
        //Picks a number from 1 to 6 to draw for spawnOne to spawnSix
        spawnOne = Random.Range(1, 7);
        spawnTwo = Random.Range(1, 7);
        spawnThree = Random.Range(1, 7);
        spawnFour = Random.Range(1, 7);
        spawnFive = Random.Range(1, 7);
        spawnSix = Random.Range(1, 7);
        //Sets this to false to first
        // It will later be true when the camera's target equals this gameobject
        */
        isTargeted = false;
        occultCloak = false;
        actionPoint = 6;
        health = 20;
        roundCounter = 0;
        healthScript = GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update () {

        // Doesn't do anything if the camera is not looking at this object
        if (cameraScript.TargetReturn() != this.gameObject.transform)
            return;
        /*
        if (actionPoint <= 0)
        {
            actionPointText.enabled = false;
        }
        else
        {
            actionPointText.enabled = true;
        }
        */

        if (isTargeted)
        {
            // Calls thje function to show which hexes the player is holding
            SendToUi();
            isTargeted = false;
        }
        
        // actionPointText.color = healthScript.chooseColor;
        actionPointText.color = playerColor;
        actionPointText.text = actionPoint.ToString();


        /*
        A number of if-statements that goes together.
        It first checks to see if the sixth place holder is holding a card.
        If it is not holding a card, it will choose a random  number from one to six
        and calls the function to show the card.
        
        If five is not holding a card, it will get its card from six.
        
        If the first to the fourth place has no card, they will get the card from five 
        */
        
        if(parentSix.transform.childCount <= 0)
        {
            spawnHex[5] = getRandomNum();
            uiScript.ShowHex(parentSix, spawnHex[5]);
        }
        
        if (parentFive.transform.childCount <= 0)
        {
            spawnHex[4] = spawnHex[5];
            uiScript.MoveSixToFive(parentSix, parentFive);
        }
        if (parentOne.transform.childCount <= 0)
        {
            spawnHex[0] = spawnHex[1];
            uiScript.moveHex(parentTwo, parentOne);
        }

        if (parentTwo.transform.childCount <= 0)
        {
            spawnHex[1] = spawnHex[2];
            uiScript.moveHex(parentThree, parentTwo);
        }
        if (parentThree.transform.childCount <= 0)
        {
            spawnHex[2] = spawnHex[3];
            uiScript.moveHex(parentFour, parentThree);
        }
        if (parentFour.transform.childCount <= 0)
        {
            spawnHex[3] = spawnHex[4];
            uiScript.MoveFiveToHand(parentFive, parentFour);
        }
        if (parentFive.transform.childCount <= 0)
        {
            spawnHex[4] = spawnHex[5];

            uiScript.MoveFiveToHand(parentFive, parentFour);
        } 
        
        // print(this.gameObject.transform.tag + " " + spawnOne + " " + spawnTwo + " " + spawnThree + " " + spawnFour + " " + spawnFive + " " + spawnSix);
    }

    //Function to set the target to true so it will show the player's hexes
    public void SetToTrue()
    {
        isTargeted = true;
    }

    /*
    Function to show the UI/Hexes
    If the target is equal to this gameobject, it will show what the player is holding
    It then set's it to false so it won't run over and over
    */
    public void SendToUi()
    {
        
            uiScript.ShowHex(parentOne, spawnHex[0]);
            uiScript.ShowHex(parentTwo, spawnHex[1]);
            uiScript.ShowHex(parentThree, spawnHex[2]);
            uiScript.ShowHex(parentFour, spawnHex[3]);
            uiScript.ShowHex(parentFive, spawnHex[4]);
            uiScript.ShowHex(parentSix, spawnHex[5]);
            
    }


    public int getRandomNum()
    {
        int getNum = Random.Range(1,7);
        for (int index = 0; index < 6; ++index)
        {
            if (index == 5)
                continue;

            else if (getNum == spawnHex[index])
            {

                while (getNum == spawnHex[index])
                {
                    getNum = Random.Range(1, 7);
                }
                index = 0;
            }
        }
        return getNum;
    }
    
    /*

    This function detroys all the hexes displayed whent the player's turn is over.
    It is called from the Camera script.
    The hexes will reappear when it comes back to the players turn
    */
    public void DestroyHex()
    {
        foreach (Transform child in parentOne.transform)
            GameObject.Destroy(child.gameObject);

        foreach (Transform child in parentTwo.transform)
            GameObject.Destroy(child.gameObject);

        foreach (Transform child in parentThree.transform)
            GameObject.Destroy(child.gameObject);

        foreach (Transform child in parentFour.transform)
            GameObject.Destroy(child.gameObject);

        foreach (Transform child in parentFive.transform)
            GameObject.Destroy(child.gameObject);

        foreach (Transform child in parentSix.transform)
            GameObject.Destroy(child.gameObject);
    }
}