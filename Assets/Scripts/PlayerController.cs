using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	
	private Rigidbody rb;
	private Vector3 movement; 
	private int floorMask; // mask that goes across the whole ground, used for rotation
	private float camRayLength = 100f;
	private Animator anim;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

	}

	void Awake ()
	{
		// layer for the floor
		floorMask = LayerMask.GetMask ("Ground");
		anim = GetComponent <Animator> ();
	}

	// for physics stuff
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");
		
		// Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		// rb.AddForce (movement * speed);
		Move (moveHorizontal, moveVertical);
		Turn ();
		Animating (moveHorizontal, moveVertical);
	}

	void Move (float h, float v) 
	{
		movement.Set (h, 0f, v);

		// magic incantation to normalize speed vector and
		// normalize it to the tick speed 
		movement = movement.normalized * speed * Time.deltaTime;

		// move the player
		rb.MovePosition(transform.position + movement);
	}

	void Turn ()
	{
		// ray from the mouse cursor in the direction of the camera
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// RaycastHit variable so we know when the mouse touches the ground
		RaycastHit floorHit;

		// if the mouse is on the ground, turn the player towards it
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;

			playerToMouse.y = 0f; // ensures the vector is along the floor plane

			// new quaternion (rotation) based on the mouse and floor
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			// set player's roation to this rotation
			rb.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v)
	{
		bool isWalking = (h != 0f || v != 0f);
		anim.SetBool ("isWalking", isWalking);
	}


}