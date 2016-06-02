using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IceScript : MonoBehaviour {

    // Other Class Sript
    private CameraScript cameraScript;
    private PlayerScript playerScript;
    private PlayerMovement playerMovementScript;
    private UIHighlightHex UIHighLightScript;
    private MouseManager mouseManager;

    // Public Unity Variabls
    public RawImage iceHex;
    public Texture unHighlighted;
    public Texture highlighted;
    public Texture selected;
    public GameObject currentHex;

    // Public Member Variables
    public bool isClicked;

    void Awake()
    {
        iceHex = GetComponent<RawImage>();
    }

    void Start()
    {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        playerScript = cameraScript.TargetReturn().GetComponent<PlayerScript>();
        // print(playerscript);
        isClicked = false;
    }

    void Update()
    {
        if (isClicked)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                Destroy(this.gameObject);
                playerScript.actionPoint -= 2;
                cameraScript.isActive = false;
                GetComponent<RawImage>().texture = unHighlighted;
            }

            else if (Input.GetMouseButtonDown(1))
            {
                isClicked = false;
                cameraScript.isActive = false;
                GetComponent<RawImage>().texture = unHighlighted;
            }
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
        if (playerScript.actionPoint >= 2 && !cameraScript.isActive)
        {
            isClicked = true;
            cameraScript.isActive = true;
            GetComponent<RawImage>().texture = selected;
            // print(isClicked);
        }
    }
}
