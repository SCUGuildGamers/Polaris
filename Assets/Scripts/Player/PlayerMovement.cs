using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;

	// For reference
	GlideCharge glideCharge;

	// Keeps track of whether or not the player is in a level or boss fight
	public bool InLevel;

	// Keeps track of whether the player can move
	public bool CanPlayerMove = true;

	// Movement values
	private float HorizontalSpeed = 10f;

	// Gravity constant
	private float GravityConstant = 7.5f;

	// Directional state
	private bool _facingRight = true;

	// Glide state
	private bool isGliding;

	// Glide values
	private static float glidingPower = 20f;

	// Glide variables
	private bool showTrajectory;
	private TrajectoryLine trajectoryLine;

	// Current forces
	private static float currentPower = glidingPower / 4;
	private Vector2 currUpVelocity = new Vector3(0, currentPower);
	private Vector2 currRightVelocity = new Vector3(currentPower, 0);
	private Vector2 currDownVelocity = new Vector3(0, -currentPower);
	private Vector2 currLeftVelocity = new Vector3(-currentPower, 0);

	void Start()
	{
		// For reference
		rb = GetComponent<Rigidbody2D>();
		glideCharge = GetComponent<GlideCharge>();
		trajectoryLine = GetComponent<TrajectoryLine>();

		// States
		isGliding = false;
		showTrajectory = false;

		// Toggles gravity depending on if the player is in a level or not
		if (InLevel) {
			ToggleGravity(true);

			// For debugging the glide charge system
			if(glideCharge != null)
				glideCharge.SetStarting();
		}
			
		else
			ToggleGravity(false);
	}

	void FixedUpdate()
	{
		// Prevent character from moving while the dash or glide is occuring
		if (!isGliding && CanPlayerMove)
			// Move our character
			Move();
	}

	void Update()
	{
		// Only perform these statements if the player is in a level/boss fight
		if (InLevel && CanPlayerMove) {
			// Update the trajectory line
			if (showTrajectory)
				UpdatePlayerTrajectory();

			if (Input.GetKeyDown("g"))
			{
				Glide();
			}

			// Canceling the trajectory line
			else if (Input.GetKeyDown(KeyCode.Escape) && showTrajectory)
			{
				showTrajectory = false;
				trajectoryLine.ClearLine(); // Clear the trajectory line
			}
		}
	}



	// When the player collides with anything, the player stop moving (implemented to stop glide movement)
	void OnCollisionEnter2D(Collision2D collider)
	{
		// If the player collides with something while they're gliding, the glide should stop 
		if (isGliding)
		{ 
			ToggleGravity(true);

			rb.velocity = new Vector2(0, 0);

			// Toggle player move state variables
			CanPlayerMove = true;

			// Toggle glide state variables
			isGliding = false;
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name.Contains("Current"))
        {
			AddCurrentVelocity(col);
			if (!isGliding)
				ToggleGravity(false);
		}	
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.name.Contains("Current") && !isGliding)
		{
			ToggleGravity(true);
			rb.velocity = new Vector2(0, 0);

			// Toggle player move state variables
			CanPlayerMove = true;
		}
	}

	// Toggles the gravity on/off with the GravityConstant given the boolean value 'toggle'
	void ToggleGravity(bool toggle)
	{
		if (toggle)
			rb.gravityScale = GravityConstant;
		else
			rb.gravityScale = 0f;
	}

	// Add current velocity depending on the collider name
	void AddCurrentVelocity(Collider2D col)
	{
		CanPlayerMove = false;

		// Add current velocity
		if (col.name == "CurrentUp")
		{
			rb.velocity = rb.velocity + currUpVelocity;
		}

		else if (col.name == "CurrentRight")
		{
			rb.velocity = rb.velocity + currRightVelocity;
		}

		else if (col.name == "CurrentDown")
		{
			rb.velocity = rb.velocity + currDownVelocity;
		}

		else if (col.name == "CurrentLeft")
		{
			rb.velocity = rb.velocity + currLeftVelocity;
		}
	}

	// Update the trajectory line relative to the player
	void UpdatePlayerTrajectory()
	{
		// Direction of dash is the unit vector of mouse position - rigidbody position
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = transform.position.z;
		var glideDirection = mousePos - transform.position;

		trajectoryLine.ShowTrajectoryLine(transform.position, glideDirection);
	}

	void Move()
	{
		float horizontalDirection = Input.GetAxis("Horizontal");

		rb.velocity = new Vector2(horizontalDirection * HorizontalSpeed, 0);

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

	// Handles logic when the Glide button is pressed
	// Physics2D.IgnoreLayerCollision(6,7,true) and Physics2D.IgnoreLayerCollision(6,7,false);
	private void Glide()
	{
		// First Glide button click
		if (!showTrajectory && glideCharge.GetChargeCounter() > 0)
		{
			showTrajectory = true;
		}

		// Second Glide button click
		else if (showTrajectory)
		{
			// Decrement the charge counter
			glideCharge.DecreaseCharge();

			// Toggle player move state variables
			CanPlayerMove = false;

			// Toggle glide state variables
			isGliding = true;

			showTrajectory = false;
			trajectoryLine.ClearLine(); // Clear the trajectory line

			// Direction of dash is the unit vector of mouse position - rigidbody position
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = transform.position.z;
			var glideDirection = mousePos - transform.position;

			// Glide initiated
			rb.velocity = new Vector2(glideDirection.normalized.x * glidingPower, glideDirection.normalized.y * glidingPower);

			// Toggle gravity off when the player is gliding
			ToggleGravity(false);
		}
	}
}
