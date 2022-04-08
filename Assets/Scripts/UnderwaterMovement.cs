using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterMovement : MonoBehaviour
{
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;

	public float horizontalSpeed = 10f;
	public float verticalSpeed = 10f;
	
	void Start()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		// Move our character
		Move();
	}

	void Move()
	{
		float horizontalDirection = Input.GetAxis("Horizontal");
		float verticalDirection = Input.GetAxis("Vertical");
		m_Rigidbody2D.velocity = new Vector2(horizontalDirection * horizontalSpeed, verticalDirection * verticalSpeed);

		// If the input is moving the player right and the player is facing left, then correct the character orientation
		if (horizontalDirection > 0 && !m_FacingRight)
		{
			Flip();
		}

		// Otherwise if the input is moving the player left and the player is facing right, then correct the character orientation
		else if (horizontalDirection < 0 && m_FacingRight)
		{
			Flip();
		}
	}

	// Flips the horizontal orientation of the character
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}