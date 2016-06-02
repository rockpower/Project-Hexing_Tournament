using UnityEngine;
using System.Collections;

public class GridManagerBounds : MonoBehaviour {

    public GameObject Hex;
    //This time instead of specifying the number of the hexes, you should just drop your ground game object on this public variable
    public GameObject Ground;

    private float hexWidth;
    private float hexHeight;
    private float groundWidth;
    private float groundHeight;

    void setSizes()
    {
        hexWidth = Hex.GetComponent<Renderer>().bounds.size.x;
        hexHeight = Hex.GetComponent<Renderer>().bounds.size.z;
        groundWidth = Ground.GetComponent<Renderer>().bounds.size.x;
        groundHeight = Ground.GetComponent<Renderer>().bounds.size.z;
    }

    //The method used to calculate the number hexagons in a row and number of rows
    //Vector2.x is gridWithInHexes and Vector2.y is gridHeightInHexes
    Vector2 calcGridSize()
    {
        //According to the math textbook hexagon's size length is half of the height
        float sideLength = hexHeight / 2;

        //the number of whole hex sides that fit inside the inside ground height
        int nrOfSides = (int)(groundHeight / sideLength);

        //Experiemnted formular
        int gridHeightInHexes = (int)(nrOfSides * 2 / 3);
        
        //When the number of hexes is even the tip of the last hex in the offset column might stick up.
        //The number of hexes in that case is reduced.
        if (gridHeightInHexes % 2 == 0
            && (nrOfSides + 0.5f) * sideLength > groundHeight)
            gridHeightInHexes--;

        //gridWidth in hexes is calculated by simply dividing ground width by hex width
        return new Vector2((int)(groundWidth / hexWidth), gridHeightInHexes);
    }
    
    //Method to calculate the position of the first hexagon tile
    //The center of the grid is (0, 0, 0)
    Vector3 calcInitPos()
    {
        Vector3 initPos;
        initPos = new Vector3(-groundWidth / 2 + hexWidth / 2, 0, groundHeight / 2 - hexWidth / 2);

        return initPos;
    }

    Vector3 calcWorldCoord(Vector2 gridPos)
    {
        Vector3 initPos = calcInitPos();
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = initPos.x + offset + gridPos.x * hexWidth;
        float z = initPos.z - gridPos.y * hexHeight * 0.75f;
        //If your ground is not a plane but a cube you might set coordinate to something like groundDepth/2 + hexDepth/2
        return new Vector3(x, 0, z);
    }
    void createGrid()
    {
        Vector2 gridSize = calcGridSize();
        GameObject hexGridGO = new GameObject("HexGrid");

        for (float y = 0; y < gridSize.y; y++)
        {
            float sizeX = gridSize.x;
            //if the offset now sticks up, reduce the number of hexes in a row
            if (y % 2 != 0 && (gridSize.x + 0.5) * hexWidth > groundWidth)
                sizeX--;
            for (float x = 0; x < sizeX; x++)
            {
                GameObject hex = (GameObject)Instantiate(Hex);
                Vector2 gridPos = new Vector2(x, y);
                hex.transform.position = calcWorldCoord(gridPos);
                hex.transform.parent = hexGridGO.transform;
            }
        }
    }

    void Start()
    {
        setSizes();
        createGrid();
    }
}
