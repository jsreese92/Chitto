using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	
	private Rigidbody rb;
	private Vector3 movement; 
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	// for physics stuff
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");
		
		// Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		// rb.AddForce (movement * speed);
		Move (moveHorizontal, moveVertical);
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

}