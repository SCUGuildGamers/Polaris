using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	private bool _facingRight = true;

	public bool CanPlayerMove = true;

	public bool IsOceanMovement = false;

	public float HorizontalSpeed = 10f;
	public float VerticalSpeed = 10f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		// Move our character
		Move();
	}

	void Move()
	{	
		// Check if the player can move or not
		if (CanPlayerMove == false)
		{
			rb.velocity = new Vector2(0, 0);
			return;
		}
			
		float horizontalDirection = Input.GetAxis("Horizontal");

		// Controls whether or not the player can do underwater movement or not
		float verticalDirection;
		if (IsOceanMovement)
			verticalDirection = Input.GetAxis("Vertical");
		else
			verticalDirection = 0;

		rb.velocity = new Vector2(horizontalDirection * HorizontalSpeed, verticalDirection * VerticalSpeed);

		// If the input is moving the player right and the player is facing left, then correct the character orientation
		if (horizontalDirection > 0 && !_facingRight)
		{
			Flip();
		}

		// Otherwise if the input is moving the player left and the player is facing right, then correct the character orientation
		else if (horizontalDirection < 0 && _facingRight)
		{
			Flip();
		}
	}

	// Flips the horizontal orientation of the character
	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}