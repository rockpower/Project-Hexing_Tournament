using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FireScript : MonoBehaviour
{

    // Other Class Sript
    private CameraScript cameraScript;
    private PlayerScript playerScript;
    private PlayerMovement playerMovementScript;
    private UIHighlightHex UIHighLightScript;
    private MouseManager mouseManager;
    //public Animator anim;

    // Public Unity Variabls
    public RawImage fireHex;
    public Texture unHighlighted;
    public Texture highlighted;
    public Texture selected;

    // Variables needed for attack/defense
    public GameObject currentHex;
    public GameObject firePreFab;
    public GameObject fire;

    // Public Member Variables
    public bool isClicked;

    void Awake()
    {
        fireHex = GetComponent<RawImage>();
    }

    void Start()
    {
        cameraScript = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        playerScript = cameraScript.TargetReturn().GetComponent<PlayerScript>();
        playerMovementScript = playerScript.GetComponent<PlayerMovement>();
        mouseManager = GameObject.Find("GameManager").GetComponent<MouseManager>();
        // print(playerscript);
        isClicked = false;

        //anim = GetComponent<>();
    }

    void Update()
    {
        if (!isClicked)
            return;

        else if (isClicked)
        {
            /*
            Vector3 v3Pos = Camera.main.WorldToScreenPoint(fire.transform.position);
            v3Pos = Input.mousePosition - v3Pos;
            float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

            currentHex.transform.position = fire.transform.position;
            currentHex.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
            */

            if (EventSystem.current.IsPointerOverGameObject())
            {
                //print("Over Hex");
                return;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                for (int index = 0; index < firePreFab.transform.childCount; ++index)
                {
                    fire.transform.GetChild(index).GetComponent<Attack_Trigger>().isAttacking = true;
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
                Destroy(fire);
            }
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        Destroy(fire);
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
            fire = Instantiate(firePreFab, currentHex.transform.position, Quaternion.identity) as GameObject;
            //fire.transform.parent = currentHex.transform;
            //fire.transform.localPosition = new Vector3(0, 0, 0);
            // print("Child Count: " + firePreFab.transform.childCount);
            // print(isClicked);
        }
    }

}
