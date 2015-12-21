using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera2DFollow : MonoBehaviour {
	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
    public float previewCameraTimer = 2.0f;
    public bool isPreviewing;
	
	private float offsetZ;
	private Vector3 lastTargetPosition;
	private Vector3 currentVelocity;
	private Vector3 lookAheadPos;


    public GameObject canvas;
    public PathDefinition Path;
    public float Speed = 10;
    public float MaxDistancToGoal = 0.1f;

    private IEnumerator<Transform> currentCameraPointPreview;

    // Use this for initialization
    private void Start()
	{
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;

        if (Path == null)
            return;
        currentCameraPointPreview = Path.GetPathEnumerator();
        currentCameraPointPreview.MoveNext();

        if (currentCameraPointPreview.Current == null)
            return;
        transform.position = currentCameraPointPreview.Current.position;
    }

    // Update is called once per frame
    private void Update()
	{   // preview level only
        if (isPreviewing)
        {
            previewCameraTimer -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentCameraPointPreview.Current.position, Time.deltaTime * Speed);
            var distanceSquared = (transform.position - currentCameraPointPreview.Current.position).sqrMagnitude;
            if (distanceSquared < MaxDistancToGoal * MaxDistancToGoal)
                currentCameraPointPreview.MoveNext();
    }
        else {
            // only update lookahead pos if accelerating or changed direction
            canvas.SetActive(true);
            float xMoveDelta = (target.position - lastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

            transform.position = newPos;

            lastTargetPosition = target.position;
        }
	}
}
