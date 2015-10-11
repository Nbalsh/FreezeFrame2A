using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10, jumpVelocity = 20;
	Transform myTrans, tagGroundLeft, tagGroundRight;
	public LayerMask playerMask;
	Rigidbody2D	myBody;

	float hInput = 0;

	bool isGrounded = false;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myTrans = this.transform;
		tagGroundLeft = GameObject.Find (this.name + "/tag_Ground_Left").transform;
		tagGroundRight = GameObject.Find (this.name + "/tag_Ground_Right").transform;
	}
	

	void FixedUpdate () {

		if (Physics2D.Linecast (myTrans.position, tagGroundLeft.position, playerMask)) {
			isGrounded = Physics2D.Linecast (myTrans.position, tagGroundLeft.position, playerMask);
			Debug.Log("isGroundedLeft");
		} else if (Physics2D.Linecast (myTrans.position, tagGroundRight.position, playerMask)) {
			isGrounded = Physics2D.Linecast (myTrans.position, tagGroundRight.position, playerMask);
			Debug.Log("isGroundedRight");
		}

#if !UNITY_ANDROID
		Move (Input.GetAxisRaw("Horizontal"));
		if(Input.GetButtonDown("Jump")){
			Jump ();
		}
#endif
		Move (hInput);
	
	}


	void Move(float inputHorizontal) {
		Vector2 moveVel = myBody.velocity;
		moveVel.x = inputHorizontal * speed;
		myBody.velocity = moveVel;
	}

	public void Jump(){
		if (isGrounded) {
			Debug.Log("Jumping");
			myBody.velocity += jumpVelocity * Vector2.up;
		}
		isGrounded = false;
	}

	public void StartMoving(float inputHorizontal){
		hInput = inputHorizontal;
	}
}
