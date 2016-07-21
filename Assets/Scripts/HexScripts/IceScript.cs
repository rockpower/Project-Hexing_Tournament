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

    // Variables needed for attack/defense
    public GameObject currentHex;
    public GameObject icePreFab;
    public GameObject ice;

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
                return;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                for (int index = 0; index < icePreFab.transform.childCount; ++index)
                {
                    ice.transform.GetChild(index).GetComponent<Attack_Trigger>().isAttacking = true;
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
                Destroy(ice);
            }
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        Destroy(ice);
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
            currentHex = mouseManager.CurrentOccupiedHex();
            ice = Instantiate(icePreFab, currentHex.transform.position, Quaternion.identity) as GameObject;
            // print(isClicked);
        }
    }
}
