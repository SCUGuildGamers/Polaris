using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;

	// Directional state
	private bool _facingRight = true;

	// Current state
	private bool inCurrentRight = false;
	private bool inCurrentLeft = false;
	private bool inCurrentUp = false;
	private bool inCurrentDown = false;

	// Movement state
	public bool CanPlayerMove = true;
	public bool IsOceanMovement = false;

	// Movement values
	public float HorizontalSpeed = 10f;
	public float VerticalSpeed = 10f;

	// Gravity constant
	public float GravityConstant = 5f;

	// Dash state
	public static bool canDash;
	public bool isDashing;

	// Dash values
	public float dashingPower = 50f;
	public float dashingTime = 0.2f;
	public float dashCooldown = 0.1f;

	// Glide state
	public static bool canGlide;
	public bool isGliding;

	// Glide values
	public float glidingPower = 50f;

	// Glide variables
	private bool showTrajectory;
	private TrajectoryLine trajectoryLine;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		canDash = true;
		isDashing = false;

		canGlide = true;
		isGliding = false;

		showTrajectory = false;
		trajectoryLine = GetComponent<TrajectoryLine>();

		ToggleGravity(true);
	}

	void FixedUpdate()
	{
		// Prevent character from moving while the dash or glide is occuring
		if (!isDashing && !isGliding)
			// Move our character
			Move();
	}

	void Update()
	{
		// Update the trajectory line
		if (showTrajectory)
			UpdatePlayerTrajectory();

		// Checks if the player is in a current and handles the logic
		AddCurrentForce();

		if ((Input.GetKeyDown("e") && canDash)) {
				StartCoroutine(Dash());
		}

		else if (Input.GetKeyDown("g") && canGlide)
		{
			Glide();
		}
	}
	


	// When the player collides with anything, the player stop moving (implemented to stop glide movement)
	void OnCollisionEnter2D(Collision2D collider)
	{
		if (isGliding) { // If the player collides with something while they're gliding, the glide should stop 
			ToggleGravity(true);
			
			rb.velocity = new Vector2(0, 0);

			isGliding = false;
		}
		
	}

	void OnTriggerEnter2D(Collider2D col)
    {
		// When the player collides with the current, stop their movement and/or glide
		if (col.name == "Current"){
			inCurrentRight = true;
			CanPlayerMove = false;
			rb.velocity = new Vector2(0, 0);
			isGliding = false;

			IsOceanMovement = true;
			ToggleGravity(false);
		}
    }

	void OnTriggerExit2D(Collider2D col){
		// When the player exits the current, they should be able to move again
		if (col.name == "Current"){
			inCurrentRight = false;
			CanPlayerMove = true;
			IsOceanMovement = false;
			ToggleGravity(true);
		}
	}

	// Toggles the gravity on/off with the GravityConstant given the boolean value 'toggle'
	void ToggleGravity(bool toggle) {
		if (toggle)
			rb.gravityScale = GravityConstant;
		else
			rb.gravityScale = 0f;
	}

	// Update the trajectory line relative to the player
	void UpdatePlayerTrajectory() {
		// Direction of dash is the unit vector of mouse position - rigidbody position
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = transform.position.z;
		var glideDirection = mousePos - transform.position;

		trajectoryLine.ShowTrajectoryLine(transform.position, glideDirection);
	}

	// Adds a directional force depending on which/if they are in a current
	void AddCurrentForce() {
		if (inCurrentRight)
		{
			rb.AddForce(new Vector3(glidingPower, 0, 0));
		}
		else if (inCurrentLeft)
		{
			rb.AddForce(new Vector3(-glidingPower, 0, 0));
		}
		else if (inCurrentUp)
		{
			rb.AddForce(new Vector3(0, glidingPower, 0));
		}
		else if (inCurrentDown)
		{
			rb.AddForce(new Vector3(0, -glidingPower, 0));
		}
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

	// Handles logic when the Dash button is pressed
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

		// Pauses script to perform dash
		yield return new WaitForSeconds(dashingTime);

		//I-Frame deactivaton and reset variables
		isDashing = false;
		Physics2D.IgnoreLayerCollision(6,7,false);

		// Below dashCooldown may not be necessary depending on what is necessary for crafting system
		yield return new WaitForSecondsRealtime(dashCooldown);
		canDash = true;
	}

	// Handles logic when the Glide button is pressed
	private void Glide()
	{
		isGliding = true;

		// First Glide button click
		if (!showTrajectory)
		{
			showTrajectory = true;
		}

		// Second Glide button click
		else
		{
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
