using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIHighlightHex : MonoBehaviour
{

    /*Notes
    1. Change the Hex to be under the parent of the Original Hex Material
    2. When the mouse enters the Original Hex Matieral, change that material to whatever the hex is
    3. When the mouse exit, change the material back to the original
    4. When the hex is clicked, destory and move five to the position of the destoryed object, and move six to the fifth position
    5. Randomize a new hex for the sixth position
    */

    /*
    Script to show which hex is highlighted.
    The script should also be abled to be clicked on 
    and show kind of hex is clicked
    */


    //Public Variables
    public RawImage img;
    public Texture textureNoTexture;
    public Texture texture_dark;
    public Texture texture_wind;
    public Texture texture_fire;
    public Texture texture_ice;
    public Texture texture_light;
    public Texture texture_lightning;

    public void ShowingTexture()
    {
        foreach (Transform child in img.transform)
        {
            var rawImage = child.GetComponent<RawImage>();
            if (rawImage.tag == "DarkHex")
            {
                Dark();
            }

            if (rawImage.tag == "LightHex")
            {
                Light();
            }

            if (rawImage.tag == "WindHex")
            {
                Wind();
            }

            if (rawImage.tag == "FireHex")
            {
                Fire();
            }

            if (rawImage.tag == "IceHex")
            {
                Ice();
            }

            if (rawImage.tag == "LightningHex")
            {
                Lightning();
            }
        }
    }

    public void Dark()
    {
        img.GetComponent<RawImage>().texture = texture_dark;
       
    }

    public void Light()
    {
        img.GetComponent<RawImage>().texture = texture_light;
    }

    public void Wind()
    {
        img.GetComponent<RawImage>().texture = texture_wind;
    }

    public void Fire()
    {
        img.GetComponent<RawImage>().texture = texture_fire;
    }

    public void Ice()
    {
        img.GetComponent<RawImage>().texture = texture_ice;
    }

    public void Lightning()
    {
        img.GetComponent<RawImage>().texture = texture_lightning;
    }

    public void NoShow()
    {
        img.GetComponent<RawImage>().texture = textureNoTexture;
    }
}
