using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hex : MonoBehaviour
{
    // Other Script Class
    public HexMaker hexMaker;
    public bool isOccupied;

    // Our coordinates in the map array
    public int x;
    public int y;
    public List<GameObject> myList = new List<GameObject>();



    void Awake()
    {
        hexMaker = GameObject.Find("HexMap").GetComponent<HexMaker>();
        isOccupied = false;
    }
    void Start()
    {

        if (y % 2 == 1)
        {
            myList.Add(GameObject.Find("Hex_" + x + "_" + (y + 1)));
            myList.Add(GameObject.Find("Hex_" + (x + 1) + "_" + (y + 1)));
            myList.Add(GameObject.Find("Hex_" + (x + 1) + "_" + y));
            myList.Add(GameObject.Find("Hex_" + (x - 1) + "_" + y));
            myList.Add(GameObject.Find("Hex_" + x + "_" + (y - 1)));
            myList.Add(GameObject.Find("Hex_" + (x + 1) + "_" + (y - 1)));
        }

        else
        {
            myList.Add(GameObject.Find("Hex_" + x + "_" + (y + 1)));
            myList.Add(GameObject.Find("Hex_" + (x - 1) + "_" + (y + 1)));
            myList.Add(GameObject.Find("Hex_" + (x + 1) + "_" + y));
            myList.Add(GameObject.Find("Hex_" + (x - 1) + "_" + y));
            myList.Add(GameObject.Find("Hex_" + x + "_" + (y - 1)));
            myList.Add(GameObject.Find("Hex_" + (x - 1) + "_" + (y - 1)));

        }
    }

    void Update()
    {

        // print("Script is getting called");
    }

    public List<GameObject> GetNeighbours(GameObject hex)
    { 
        return myList;
    }


     void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Border")
        {
            foreach(GameObject gO in myList)
            {
                if(gO == this.gameObject)
                {
                    print("Something");
                }
            }
            for (int index = 0; index < hexMaker.myHex.Count; ++index)
            {
                if (this.gameObject == hexMaker.myHex[index].gameObject)
                {
                    // hexmaker.myHex[index] = null;
                    hexMaker.myHex.RemoveAt(index);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
