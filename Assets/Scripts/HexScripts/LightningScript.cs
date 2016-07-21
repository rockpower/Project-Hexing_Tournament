using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LightningScript : MonoBehaviour {

    // Other Class Sript
    private CameraScript cameraScript;
    private PlayerScript playerScript;
    private PlayerMovement playerMovementScript;
    private UIHighlightHex UIHighLightScript;
    private MouseManager mouseManager;

    // Public Unity Variabls
    public RawImage lightningHex;
    public Texture unHighlighted;
    public Texture highlighted;
    public Texture selected;

    //Variables for attacking
    public GameObject currentHex;
    public GameObject lightningPreFab;
    public GameObject lightning;

    // Public Member Variables
    public bool isClicked;

    void Awake()
    {
        lightningHex = GetComponent<RawImage>();
    }

    void Start()
    {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        playerScript = cameraScript.TargetReturn().GetComponent<PlayerScript>();
        playerMovementScript = playerScript.GetComponent<PlayerMovement>();
        mouseManager = GameObject.Find("GameManager").GetComponent<MouseManager>();
        // print(playerscript);
        isClicked = false;
    }

    void Update()
    {
        if (!isClicked)
            return;

        else if (isClicked)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //print("Over Hex");
                return;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                for (int index = 0; index < lightningPreFab.transform.childCount; ++index)
                {
                    lightning.transform.GetChild(index).GetComponent<Attack_Trigger>().isAttacking = true;
                    // print(fire.transform.GetChild(index).GetComponent<Attack_Trigger>().isAttacking);
                }
                playerScript.actionPoint -= 2;
                cameraScript.isActive = false;
                GetComponent<RawImage>().texture = unHighlighted;
                StartCoroutine(WaitToDestroy());
            }

            else if (Input.GetMouseButtonDown(1))
            {
                isClicked = false;
                cameraScript.isActive = false;
                GetComponent<RawImage>().texture = unHighlighted;
                Destroy(lightning);
            }
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        Destroy(lightning);
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
        if (playerScript.actionPoint >= 3 && !cameraScript.isActive)
        {
            isClicked = true;
            cameraScript.isActive = true;
            GetComponent<RawImage>().texture = selected;
            currentHex = mouseManager.CurrentOccupiedHex();
            lightning = Instantiate(lightningPreFab, currentHex.transform.position, Quaternion.identity) as GameObject;
            // print(isClicked);
        }
    }
}
