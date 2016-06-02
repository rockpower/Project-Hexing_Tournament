using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    #region Variables (private)
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform followXForm;

    //private global only
    private Vector3 lookDir;
    private Vector3 targetPosition;

    //Smoothing and damping
    private Vector3 velocityCamSmooth = Vector3.zero;
    [SerializeField]
    private float camSmoothDampTime;

    #endregion




    #region Unity event functions
    // Use this for initialization
    void Start ()
    {
        followXForm = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnDrawGizmos()
    {

    }

    void LateUpdate()
    {
        Vector3 characterOffset = followXForm.position + new Vector3(0f, distanceUp, 0f);

        //Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        Debug.DrawRay(this.transform.position, lookDir, Color.green);

        //Setting the target position to be the correct offset from the hovercraft
        targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;
        //Showing in editor
        //Debug.DrawRay(followXForm.position, Vector3.up * distanceUp, Color.red);
        //Debug.DrawRay(followXForm.position, -1f * followXForm.forward * distanceAway, Color.blue);
        Debug.DrawRay(followXForm.position, targetPosition, Color.magenta);

        
        smoothPosition(this.transform.position, targetPosition);

        //make sure the camera is looking the right way!
        transform.LookAt(followXForm);

    }
    #endregion



    #region Methods
    private void smoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        //Making a smooth transition between camera's current position and the position it wants to be in
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        Debug.DrawLine(fromObject, toTarget, Color.cyan);
        //Compensate for walls between camera
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
        }
    }
    #endregion Methods
}
