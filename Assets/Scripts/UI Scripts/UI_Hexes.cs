using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UI_Hexes : MonoBehaviour
{
    /*
    The hex list is in order from 1-6
    It is best if it is kept this way so things won't get confused.
    */
    // Public Unity Variables    
    public RawImage lightHex;
    public RawImage darkHex;
    public RawImage windHex;
    public RawImage fireHex;
    public RawImage iceHex;
    public RawImage lighteningHex;
    public RawImage broken;
    
    // Use this for initialization
    void Start()
    {
    }

    /*
    Function to "draw" a card.
    It takes a number from one to six(there is a seven there because 
    Unity doesn't include the last number if it is a int)
    Once a number is picked, it assigns that hex to the local hex variable.
    It returns is a hex that is picked randomly from Unity.
    */
    /*
    This function will most likely be used to spawn to the sixth 
    spot only except during the start of the game.
    */
    public RawImage DrawCard(int randomHex)
    {
        RawImage hex = broken;
        // int randomHex = Random.Range(1, 7);
        switch (randomHex)
        {
            case 1:
                hex = Instantiate(lightHex) as RawImage;
                
                return hex;

            case 2:
                hex = Instantiate(darkHex) as RawImage;
                return hex;

            case 3:
                hex = Instantiate(windHex) as RawImage;
                return hex;

            case 4:
                hex = Instantiate(fireHex) as RawImage;
                return hex;

            case 5:
                hex = Instantiate(iceHex) as RawImage;
                return hex;

            case 6:
                hex = Instantiate(lighteningHex) as RawImage;
                return hex;
        }
        return hex;
    }

    public void moveHex(GameObject moveThis, GameObject moveHere)
    {
        RawImage hex = moveThis.gameObject.transform.GetChild(0).GetComponent<RawImage>();
        hex.transform.SetParent(moveHere.transform);
        hex.rectTransform.localPosition = new Vector3(0, 0, 0);
        hex.rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
    }

    /*
    Function to show the hexes
    It takes in two parameters, a rawimage and a randomhexNumber

    If the player is five or six, then it would scale the hexes to be smaller.
    It will also allow the player not to be able click on them

    Anything else, the function will get the new hex and parent it under it's parent
    and have them be in the correct spot.
    */
    public void ShowHex(GameObject parent, int randomHex)
    {
        RawImage child = DrawCard(randomHex);
        if (parent.tag == "spawnFive")
        {
            child.transform.SetParent(parent.transform);
            child.rectTransform.localPosition = new Vector3(0, 0, 0);
            child.rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
            child.GetComponent<EventTrigger>().enabled = false;
        }

        else if (parent.tag == "spawnSix")
        {
            child.transform.SetParent(parent.transform);
            child.rectTransform.localPosition = new Vector3(0, 0, 0);
            child.rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
            child.GetComponent<EventTrigger>().enabled = false;
        }

        else
        {
            child.transform.SetParent(parent.transform);
            child.rectTransform.localPosition = new Vector3(0, 0, 0);
            child.rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
        }
    }

    /*
    When a hex from one to four get's detroyed,
    the fifth hex will take over the hex that got destroyed
    */ 
    public void MoveFiveToHand(GameObject parentFive, GameObject parentDestroyed)
    {
        if (parentFive.transform.childCount == 0)
            return;
        RawImage hex = parentFive.gameObject.transform.GetChild(0).GetComponent<RawImage>();
        hex.transform.SetParent(parentDestroyed.transform);
        hex.rectTransform.localPosition = new Vector3(0, 0, 0);
        hex.rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
        hex.GetComponent<EventTrigger>().enabled = true;
    }

    /*
    This function moves the sixth hex to the fifth place
    */
    public void MoveSixToFive(GameObject parentSix, GameObject parentFive)
    {
        RawImage hex = parentSix.gameObject.transform.GetChild(0).GetComponent<RawImage>();
        hex.transform.SetParent(parentFive.transform);
        hex.rectTransform.localPosition = new Vector3(0, 0, 0);
        hex.rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
        hex.GetComponent<EventTrigger>().enabled = false;
    }
}


