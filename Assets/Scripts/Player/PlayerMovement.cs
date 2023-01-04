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

	// Dashing vairables
	[Header("Dash Variables")]
	public static bool canDash;
	public bool isDashing;
	public float dashingPower = 50f;
	public float dashingTime = 0.2f;
	public float dashCooldown = 0.1f;
	//public float iFrameTime = 0.3f;

	// Gliding vairables
	[Header("Glide Variables")]
	public static bool canGlide;
	public bool isGliding;
	public float glidingPower = 50f;


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		canDash = true;
		isDashing = false;
		canGlide = true;
		isGliding = false;
	}

	void Update()
	{
		if ((Input.GetKeyDown("e") && canDash)) {
				StartCoroutine(Dash());
		}

		else if (Input.GetKeyDown("g") && canGlide)
		{
			Glide();
		}
	}
	void FixedUpdate()
	{
		// Stops all other actions while dashing is occuring
		if(!isDashing && !isGliding)
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
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private IEnumerator Dash()
	{
		canDash = false;
		isDashing = true;

		// I-Frame Activation
		Physics2D.IgnoreLayerCollision(6,7,true);

		// Direction of dash is the unit vector of mouse position - rigidbody position
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = transform.position.z;
		var dashDirection = mousePos - transform.position;

		// Copied Flip instructions but with x value of dashDirection
		if (dashDirection.x > 0 && !_facingRight)
		{
			Flip();
		}
		else if (dashDirection.x < 0 && _facingRight)
		{
			Flip();
		}

		// Dash initiated
		rb.velocity = new Vector2(dashDirection.normalized.x * dashingPower, dashDirection.normalized.y * dashingPower);
		// What? v
		yield return new WaitForSeconds(dashingTime);

		//I-Frame deactivaton and reset variables
		isDashing = false;
		Physics2D.IgnoreLayerCollision(6,7,false);

		// Below dashCooldown may not be necessary depending on what is necessary for crafting system
		yield return new WaitForSecondsRealtime(dashCooldown);
		canDash = true;
	}

	private void Glide()
	{
		isGliding = true;

		// Direction of dash is the unit vector of mouse position - rigidbody position
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = transform.position.z;
		var dashDirection = mousePos - transform.position;

		// Glide initiated
		rb.velocity = new Vector2(dashDirection.normalized.x * glidingPower, dashDirection.normalized.y * glidingPower);
		Debug.Log(rb.velocity);
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		isGliding = false;

		rb.velocity = new Vector2(0, 0);
	}
}
