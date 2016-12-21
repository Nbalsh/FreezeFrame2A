using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float speed = 10, jumpVelocity = 20;
	Transform myTrans, tagGroundLeft, tagGroundRight;
	public LayerMask playerMask;
	Rigidbody2D	myBody;

	float hInput = 0;

	bool isGrounded = false;

    PressureButton[] pressureButtons;

    private int AMOUNT_OF_FROZEN_PLAYERS = 5;

    // old scripts
    public GameObject frozenPlayer;
	List<GameObject> frozenPlayers;
	private Vector2 spawn;
	public GameObject deathParticles;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myTrans = this.transform;
		tagGroundLeft = GameObject.Find (this.name + "/tag_Ground_Left").transform;
		tagGroundRight = GameObject.Find (this.name + "/tag_Ground_Right").transform;

		frozenPlayers = new List<GameObject>(AMOUNT_OF_FROZEN_PLAYERS);
		spawn = transform.position;

        pressureButtons = FindObjectsOfType(typeof(PressureButton)) as PressureButton[];
        Debug.Log("num of pressure buttons: " + pressureButtons.Length);

    }
	

	void FixedUpdate () {
		isGrounded = isGroundedA ();

#if !UNITY_ANDROID
        float hInput = Input.GetAxis("Horizontal");
        //Move (Input.GetAxis("Horizontal"));
        Move(hInput);
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)){
			Jump ();
		}
#endif
		Move (hInput);
	
	}

	public bool isGroundedA(){
		if (Physics2D.Linecast (myTrans.position, tagGroundLeft.position, playerMask)) {
			//Debug.Log("isGroundedLeft");
			return Physics2D.Linecast (myTrans.position, tagGroundLeft.position, playerMask);


		} else if (Physics2D.Linecast (myTrans.position, tagGroundRight.position, playerMask)) {
			//Debug.Log("isGroundedRight");
			return Physics2D.Linecast (myTrans.position, tagGroundRight.position, playerMask);
		}
		return false;

	}
	void Move(float inputHorizontal) {
		Vector2 moveVel = myBody.velocity;
		moveVel.x = inputHorizontal * speed;
		myBody.velocity = moveVel;
	}

	public void Jump(){
		if (isGrounded) {
			//Debug.Log("Jumping");
			myBody.velocity += jumpVelocity * Vector2.up;
		}
		isGrounded = false;
	}

	public void StartMoving(float inputHorizontal){
		hInput = inputHorizontal;
	}

	// old scripts
	public void FreezeFrame()
	{
		if(frozenPlayers.Count < 5)
		{
            GameObject frozenPlayerGO = newFrozenPlayer();
			frozenPlayers.Add(frozenPlayerGO);
			//Debug.Log("frozenPlayers.Count: " + frozenPlayers.Count);
		}
		else
		{
            
            for(int i = 0; i < pressureButtons.Length; i++)
            {
                pressureButtons[i].GetComponent<PressureButton>().shouldBeTriggered();
            }
            frozenPlayers[0].GetComponent<FrozenPlayer>().Die();
            frozenPlayers.RemoveAt(0);
            GameObject frozenPlayerGO = newFrozenPlayer();
			frozenPlayers.Add(frozenPlayerGO);
		}
	}

    private GameObject newFrozenPlayer()
    {
        Vector3 positionToSpawn = transform.position;
        transform.position = spawn;
        return Instantiate(frozenPlayer, positionToSpawn, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

	public void Die()
	{
		Instantiate (deathParticles, transform.position, Quaternion.Euler (270, 0, 0));
		transform.position = spawn;
		
		for(int i = 0; i < frozenPlayers.Count; ++i)
		{
			frozenPlayers[i].GetComponent<FrozenPlayer>().Die();
		}
		frozenPlayers.Clear();
	}

	public void SetCheckpoint(Vector2 position)
	{
		spawn = position;
	}

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.name == "Platform")
        {
            transform.parent = c.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.transform.name == "Platform")
        {
            transform.parent = null;
        }
    }

}
