using UnityEngine;
using System.Collections;

public class TileMouseOver : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
	}

    public Material defaultMaterial;
    public Material highlightMaterial;

    void OnMouseOver()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material = defaultMaterial;
    }
}
