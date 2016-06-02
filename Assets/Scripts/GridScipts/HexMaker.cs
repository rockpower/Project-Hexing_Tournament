using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexMaker : MonoBehaviour {

    public GameObject hexPrefab;

    public List<GameObject> myHex;
    // Size of the map in terms of number of hex tiles
    // This is NOT representative of the amount of 
    // world space that we're going to take up.
    // (i.e. our tiles might be more or less than 1 Unity World Unit)
    int width = 11;
    int height = 11;

    private float xOffset = 1.43f;
    private float zOffset = 1.23f;

    // Use this for initialization
    void Awake()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * xOffset;
                // Are we on an odd row?
                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }
                GameObject hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0, y * zOffset), Quaternion.identity);

                // Name the gameobject something sensible.
                hex_go.name = "Hex_" + x + "_" + y;

                // Make sure the hex is aware of its place on the map
                hex_go.GetComponent<Hex>().x = x;
                hex_go.GetComponent<Hex>().y = y;

                // For a cleaner hierachy, parent this hex to the map
                hex_go.transform.SetParent(this.transform);

                myHex.Add(hex_go); // Adds the new hex to myHex list

                // TODO: Quill needs to explain different optimization later...
               // hex_go.isStatic = true;
            }
        }
    }
}
