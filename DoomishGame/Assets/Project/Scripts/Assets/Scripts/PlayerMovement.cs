/*
	Created by @DawnosaurDev at youtube.com/c/DawnosaurStudios
	Thanks so much for checking this out and I hope you find it helpful! 
	If you have any further queries, questions or feedback feel free to reach out on my twitter or leave a comment on youtube :D

	Feel free to use this in your own games, and I'd love to see anything you make!
 */

using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;

public class PlayerMovement  : MonoBehaviour
{
    //Scriptable object which holds all the player's movement parameters. If you don't want to use it
    //just paste in all the parameters, though you will need to manuly change all references in this script
    
	#region MINE
    public bool canPlayerMove = true;
	public PlayerData Data;
	[SerializeField] GameObject playerVisuals;
	[SerializeField] Transform mousePos;
	[SerializeField] float wallSlideSpeed;
	[SerializeField] GameObject dashCollider;

	private GroundSlamWithBonus groundSlamWithBonus;
	private PlayerBlinkingAbility blikingAbility;
	private SpriteRenderer spriteRenderer;
	Player player;
	private RotateWeaponCollider rotateWeaponCollider;
	public bool isGrounded;
	public bool isWallSliding { get; private set; }
	public bool isGroundSlamming { get; set; }
	public bool recentlyReallyColseToWalls { get; private set; }

    #endregion

    #region COMPONENTS
    public Rigidbody2D RB { get; private set; }
	//Script to handle all player animations, all references can be safely removed if you're importing into your own project.
	public PlayerAnimator AnimHandler { get; private set; }
	#endregion

	#region STATE PARAMETERS
	//Variables control the various actions the player can perform at any time.
	//These are fields which can are public allowing for other sctipts to read them
	//but can only be privately written to.
	public bool IsFacingRight { get; private set; }
	public bool IsJumping { get; set; }
	public bool IsWallJumping { get; private set; }
	public bool IsDashing { get; private set; }
	public bool IsSliding { get; private set; }
	public bool isRolling { get; private set; }

	//Timers (also all fields, could be private and a method returning a bool could be used)
	public float LastOnGroundTime { get; private set; }
	public float LastOnWallTime { get; private set; }
	public float LastOnWallRightTime { get; private set; }
	public float LastOnWallLeftTime { get; private set; }

	//Jump
	private bool _isJumpCut;
	private bool _isJumpFalling;
	private int _bonusJumpsLeft;

	//Wall Jump
	private float _wallJumpStartTime;
	private int _lastWallJumpDir;

	//Dash
	private int _dashesLeft;
	private bool _dashRefilling;
	private Vector2 _lastDashDir;
	private bool _isDashAttacking;

	//Roll
	private int _rollLeft;
    private bool _rollRefilling;
	private Vector2 _lastRoolDir;
    private bool _isRollAttacking;

	//Run
	private float _currentSpeed;

    #endregion

    #region INPUT PARAMETERS
    private Vector2 _moveInput;

	public float LastPressedJumpTime { get; private set; }
	public float LastPressedDashTime { get; private set; }
	public float LastPressedRollTime { get; private set; }

	private PlayerPogJump playerPogJump;
    #endregion

    #region CHECK PARAMETERS
    //Set all of these up in the inspector
    [Header("Checks")] 
	[SerializeField] private Transform _groundCheckPoint;
	//Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	[Space(5)]
	[SerializeField] private Transform _frontWallCheckPoint;
	[SerializeField] private Transform _backWallCheckPoint;
	[SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;
	#endregion

    private void Awake()
	{
		groundSlamWithBonus = GetComponent<GroundSlamWithBonus>();
		blikingAbility = GetComponent<PlayerBlinkingAbility>();
		playerPogJump = GetComponent<PlayerPogJump>();
		rotateWeaponCollider = GetComponentInChildren<RotateWeaponCollider>();
		RB = GetComponent<Rigidbody2D>();
		AnimHandler = GetComponentInChildren<PlayerAnimator>();
		player = GetComponentInChildren<Player>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	private void Start()
	{
		canPlayerMove = true;
		SetGravityScale(Data.gravityScale);
		IsFacingRight = true;
		_rollLeft = 1;
		isGroundSlamming = false;
		_currentSpeed = Data.runMaxSpeed;
	}

	private void Update()
	{
        isGrounded = false;

        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
		LastOnWallTime -= Time.deltaTime;
		LastOnWallRightTime -= Time.deltaTime;
		LastOnWallLeftTime -= Time.deltaTime;

		LastPressedJumpTime -= Time.deltaTime;
		LastPressedDashTime -= Time.deltaTime;
		LastPressedRollTime -= Time.deltaTime;
		#endregion

		#region INPUT HANDLER

		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");

		if (_moveInput.x != 0)
			CheckDirectionToFace(_moveInput.x > 0);

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.J))
        {
			OnJumpInput();
        }

		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.J))
		{
			OnJumpUpInput();
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.C))
		{
			OnDashInput();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			OnRollInput();
		}
		#endregion

		#region COLLISION CHECKS
		if (!IsDashing && !IsJumping)
		{
			//Ground Check
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping) //checks if set box overlaps with ground
			{
				isGrounded = true;
				if(LastOnGroundTime < -0.1f)
                {
					AnimHandler.justLanded = true;
				}

				LastOnGroundTime = Data.coyoteTime; //if so sets the lastGrounded to coyoteTime
            }		

			//Right Wall Check
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
				LastOnWallRightTime = Data.coyoteTime;
				
				

			//Right Wall Check
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
				LastOnWallLeftTime = Data.coyoteTime;


            //Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
            LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);

			//if((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && Input.GetAxis("Horizontal") > 0f)
			//	|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && Input.GetAxis("Horizontal")  < 0f))
   //         {
			//}

			//else
			//{
			//}

        }
		#endregion

		#region JUMP CHECKS
		if (IsJumping && RB.velocity.y < 0)
		{
			IsJumping = false;

			if(!IsWallJumping)
				_isJumpFalling = true;
		}

		if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
		{
			IsWallJumping = false;
		}

		if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
			_isJumpCut = false;
			_bonusJumpsLeft = Data.airJumps;

			if(!IsJumping)
				_isJumpFalling = false;
		}
		
		//thanks to that extra condition, it ghater inputs at the begining of the roll
		if (!IsDashing && !_isRollAttacking)
		{
			//Jump
			if (CanJump() && LastPressedJumpTime > 0 && !Input.GetKey(KeyCode.DownArrow))
			{
				IsJumping = true;
				IsWallJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;
				Jump();

				AnimHandler.startedJumping = true;
			}
			//WALL JUMP
			else if (CanWallJump() && LastPressedJumpTime > 0)
			{
				IsWallJumping = true;
				IsJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;

				_wallJumpStartTime = Time.time;
				_lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;

				WallJump(_lastWallJumpDir);
			}

			else if(LastPressedJumpTime > 0 && _bonusJumpsLeft > 0 && !Input.GetKey(KeyCode.DownArrow))
			{
				IsJumping = true;
				IsWallJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;

				_bonusJumpsLeft--;

				Jump();

                AnimHandler.startedJumping = true;
            }

			else if(playerPogJump.makingPogJump && !Input.GetKey(KeyCode.DownArrow))
			{
                IsJumping = true;
                IsWallJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;

				playerPogJump.makingPogJump = false;
                Jump();

                AnimHandler.startedJumping = true;
            }
        }
		#endregion

		#region DASH CHECKS
		if (CanDash() && LastPressedDashTime > 0 && !isRolling)
		{
			//Freeze game for split second. Adds juiciness and a bit of forgiveness over directional input
			Sleep(Data.dashSleepTime);
			//If not direction pressed, dash forward
			if (_moveInput != Vector2.zero)
				_lastDashDir = _moveInput;
			else
				_lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;
			//_lastDashDir = mousePos.right;

			//IsDashing = true;
			IsJumping = false;
			IsWallJumping = false;
			_isJumpCut = false;

			rotateWeaponCollider.RotateWeapon(_lastDashDir);
			StartCoroutine(nameof(StartDash), _lastDashDir);
		}
		#endregion

		#region ROLL CHECKS
		if (LastPressedRollTime > 0 && !isGroundSlamming && !_rollRefilling && _rollLeft > 0 && isGrounded)
		{
			Vector2 rollDir = Vector2.zero;

			if (_moveInput.x > 0.75f)
			{
				rollDir = Vector2.right;
			}

			else if(_moveInput.x < -0.75)
			{
                rollDir = -Vector2.right;
            }

            else
			{
				if(IsFacingRight)
				{
                    rollDir = Vector2.right;
                }
                else
				{
                    rollDir = -Vector2.right;
                }
            }
			StartCoroutine(StartRoll(rollDir));
		}

        #endregion

        #region SLIDE CHECKS
        if (CanSlide() && ((LastOnWallLeftTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
			IsSliding = true;
		else
			IsSliding = false;
		#endregion

		#region GRAVITY
		if (!_isDashAttacking)
		{
			//Higher gravity if we've released the jump input or are falling
			if (IsSliding || blikingAbility.afterBlink || groundSlamWithBonus.isGroundSlammingWithBonus)
			{
				SetGravityScale(0);
			}
			else if (RB.velocity.y < 0 && _moveInput.y < 0)
			{
				//Much higher gravity if holding down
				SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
			}
			else if (_isJumpCut)
			{
				//Higher gravity if jump button released
				SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
			{
				SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
			}
			else if (RB.velocity.y < 0)
			{
				//Higher gravity if falling
				SetGravityScale(Data.gravityScale * Data.fallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else
			{
				//Default gravity if standing on a platform or moving upwards
				SetGravityScale(Data.gravityScale);
			}
		}
		else
		{
			//No gravity when dashing (returns to normal once initial dashAttack phase over)
			SetGravityScale(0);
		}
		#endregion

		#region MINE
		//if(Data.coyoteTime > LastOnWallTimePog)
		//{
		//	recentlyReallyColseToWalls = false;
		//}

		//else
		//{
		//	recentlyReallyColseToWalls = true;
		//}
		#endregion

	}

	private void FixedUpdate()
	{
		//Handle Run
		if (!IsDashing && !groundSlamWithBonus.isGroundSlammingWithBonus)
		{
			if (IsWallJumping)
				Run(Data.wallJumpRunLerp);
			else
				Run(1);
		}
		else if (_isDashAttacking)
		{
			Run(Data.dashEndRunLerp);
		}

		//Handle Slide
		if (IsSliding)
			Slide();
    }

    #region INPUT CALLBACKS
	//Methods which whandle input detected in Update()
    public void OnJumpInput()
	{
		LastPressedJumpTime = Data.jumpInputBufferTime;
	}

	public void OnJumpUpInput()
	{
		if (CanJumpCut() || CanWallJumpCut())
			_isJumpCut = true;
	}

	public void OnDashInput()
	{
		LastPressedDashTime = Data.dashInputBufferTime;
	}

	public void OnRollInput()
	{	
		if(!isGroundSlamming)
		LastPressedRollTime = Data.rollInputBufferTime;
	}
	#endregion

	#region GENERAL METHODS

	public void ActivateBonusSpeed(float extraSpeed)
	{
		_currentSpeed = Data.runMaxSpeed + extraSpeed;
	}

	public void DeactivateBonusSpeed()
	{
		_currentSpeed = Data.runMaxSpeed;
	}

    public void SetGravityScale(float scale)
	{
		RB.gravityScale = scale;
	}

	private void Sleep(float duration)
    {
		//Method used so we don't need to call StartCoroutine everywhere
		//nameof() notation means we don't need to input a string directly.
		//Removes chance of spelling mistakes and will improve error messages if any
		StartCoroutine(nameof(PerformSleep), duration);
    }

	private IEnumerator PerformSleep(float duration)
    {
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(duration); //Must be Realtime since timeScale with be 0 
		Time.timeScale = 1;
	}
    #endregion

	//MOVEMENT METHODS
    #region RUN METHODS
    private void Run(float lerpAmount)
	{
		//Calculate the direction we want to move in and our desired velocity
		float targetSpeed = _moveInput.x * _currentSpeed;
		//We can reduce are control using Lerp() this smooths changes to are direction and speed
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

		#region Calculate AccelRate
		float accelRate;

		//Gets an acceleration value based on if we are accelerating (includes turning) 
		//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
		#endregion

		#region Add Bonus Jump Apex Acceleration
		//Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
		if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
		{
			accelRate *= Data.jumpHangAccelerationMult;
			targetSpeed *= Data.jumpHangMaxSpeedMult;
		}
		#endregion

		#region Conserve Momentum
		//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
		if(Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			//Prevent any deceleration from happening, or in other words conserve are current momentum
			//You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
			accelRate = 0; 
		}
		#endregion

		//Calculate difference between current velocity and desired velocity
		float speedDif = targetSpeed - RB.velocity.x;
		//Calculate force along x-axis to apply to thr player

		float movement = speedDif * accelRate;

		//Convert this to a vector and apply to rigidbody
		RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

		/*
		 * For those interested here is what AddForce() will do
		 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
	}

	private void Turn()
	{
		Vector3 trans;

		//flips visuals of player
		Vector3 scale = playerVisuals.transform.localScale;
		scale.x *= -1;
        playerVisuals.transform.localScale = scale;

		//change needed due to flipping only visuals of player
		trans = _frontWallCheckPoint.position;
		_frontWallCheckPoint.position = _backWallCheckPoint.position;
		_backWallCheckPoint.position = trans;

		IsFacingRight = !IsFacingRight;
	}
    #endregion

    #region JUMP METHODS
    public void Jump()
	{
		//Ensures we can't call Jump multiple times from one press
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;

		#region Perform Jump
		//We increase the force applied if we are falling
		//This means we'll always feel like we jump the same amount 
		//(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
		float force = Data.jumpForce;
		if (RB.velocity.y < 0)
			force -= RB.velocity.y;

		RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		#endregion
	}

	private void WallJump(int dir)
	{
		//Ensures we can't call Wall Jump multiple times from one press
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;
		LastOnWallRightTime = 0;
		LastOnWallLeftTime = 0;

		#region Perform Wall Jump
		Vector2 force;

		if (Input.GetKey(KeyCode.UpArrow))
		{
			force = new Vector2(Data.wallJumpUpForce.x, Data.wallJumpUpForce.y);
		}
		else
		{
            force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);

        }
        force.x *= dir; //apply force in opposite direction of wall

		if (Mathf.Sign(RB.velocity.x) != Mathf.Sign(force.x))
			force.x -= RB.velocity.x;

		if (RB.velocity.y < 0) //checks whether player is falling, if so we subtract the velocity.y (counteracting force of gravity). This ensures the player always reaches our desired jump force or greater
			force.y -= RB.velocity.y;

		//else
		//	Mathf.Min(force.y + RB.velocity.y, Data.wallJumpUpForce.y + 5f);

		else
			force.y -= RB.velocity.y;


		//else if (RB.velocity.y > 12f)
		//{
		//	force.y = Mathf.Min(force.y, 20f);
		//}

		//Unlike in the run we want to use the Impulse mode.
		//The default mode will apply are force instantly ignoring masss
		RB.AddForce(force, ForceMode2D.Impulse);

		#endregion
	}
	#endregion

	#region DASH METHODS
	//Dash Coroutine
	private IEnumerator StartDash(Vector2 dir)
	{
		//Overall this method of dashing aims to mimic Celeste, if you're looking for
		// a more physics-based approach try a method similar to that used in the jump
		IsDashing = true;
		LastOnGroundTime = 0;
		LastPressedDashTime = 0;

		float startTime = Time.time;

		_dashesLeft--;
        dashCollider.SetActive(true);
        _isDashAttacking = true;
		AnimHandler.StartDashAnimation();

        SetGravityScale(0);

		//We keep the player's velocity at the dash speed during the "attack" phase (in celeste the first 0.15s)
		while (Time.time - startTime <= Data.dashAttackTime)
		{
			RB.velocity = dir.normalized * Data.dashSpeed;
			//Pauses the loop until the next frame, creating something of a Update loop. 
			//This is a cleaner implementation opposed to multiple timers and this coroutine approach is actually what is used in Celeste :D
			yield return null;
		}

		startTime = Time.time;

		_isDashAttacking = false;

		//Begins the "end" of our dash where we return some control to the player but still limit run acceleration (see Update() and Run())
		SetGravityScale(Data.gravityScale);
		RB.velocity = Data.dashEndSpeed * dir.normalized;

		while (Time.time - startTime <= Data.dashEndTime)
		{
			yield return null;
		}

		//Dash over
		IsDashing = false;
	}

	//Short period before the player is able to dash again

	public void InstaDashRefill()
	{
		_dashesLeft = 1;
	}

	private IEnumerator RefillDash(int amount)
	{
		//SHoet cooldown, so we can't constantly dash along the ground, again this is the implementation in Celeste, feel free to change it up
		_dashRefilling = true;
		yield return new WaitForSeconds(Data.dashRefillTime);
		_dashRefilling = false;
		_dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
	}
	#endregion

	#region ROLL METHOD

	private IEnumerator StartRoll (Vector2 dir)
    {
		isRolling = true;
		AnimHandler.StartRollingAnimation();
        //Overall this method of dashing aims to mimic Celeste, if you're looking for
        // a more physics-based approach try a method similar to that used in the jump

        //LastOnGroundTime = 0;
        //here I should start counting this same time like in time jump buffer
        LastPressedRollTime = 0;

        float startTime = Time.time;

        _rollLeft--;
		_isRollAttacking = true;
		//dashMeleeCollider.SetActive(true);

		player.StartInvincibility(Data.invDuration);
        //We keep the player's velocity at the dash speed during the "attack" phase (in celeste the first 0.15s)
        while (Time.time - startTime <= Data.rollAttackTime)
        {
            RB.velocity = dir.normalized * Data.rollSpeed;
            //Pauses the loop until the next frame, creating something of a Update loop. 
            //This is a cleaner implementation opposed to multiple timers and this coroutine approach is actually what is used in Celeste :D
            yield return null;
        }

        startTime = Time.time;
        _isRollAttacking = false;

        //Begins the "end" of our dash where we return some control to the player but still limit run acceleration (see Update() and Run())
        RB.velocity = Data.rollEndSpeed * dir.normalized;

        while (Time.time - startTime <= Data.rollEndTime)
        {
            yield return null;
        }
        //Dash over
        isRolling = false;

		StartCoroutine(RefillRoll(1));
    }

    private IEnumerator RefillRoll(int amount)
    {
        //SHoet cooldown, so we can't constantly dash along the ground, again this is the implementation in Celeste, feel free to change it up
        _rollRefilling = true;
        yield return new WaitForSeconds(Data.rollRefillTime);
        _rollRefilling = false;
        _rollLeft = Mathf.Min(Data.rollAmount, _rollLeft + 1);
    }




    #endregion

    #region Deflect

    #endregion

    #region OTHER MOVEMENT METHODS
    private void Slide()
	{

		//Works the same as the Run but only in the y-axis
		//THis seems to work fine, buit maybe you'll find a better way to implement a slide into this system
		float speedDif = Data.slideSpeed - RB.velocity.y;
		float movement = speedDif * Data.slideAccel;
		//So, we clamp the movement here to prevent any over corrections (these aren't noticeable in the Run)
		//The force applied can't be greater than the (negative) speedDifference * by how many times a second FixedUpdate() is called. For more info research how force are applied to rigidbodies.
		movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));
		RB.AddForce(movement * Vector2.up);
		//RB.velocity = new Vector2(RB.velocity.x, Mathf.Clamp(RB.velocity.y, -wallSlidingSpeed, float.MaxValue));
	}
    #endregion

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != IsFacingRight)
			Turn();
	}

    private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }

	private bool CanWallJump()
    {
		return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
			 (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
	}

	private bool CanJumpCut()
    {
		return IsJumping && RB.velocity.y > 0;
    }

	private bool CanWallJumpCut()
	{
		return IsWallJumping && RB.velocity.y > 0;
	}
	
	private bool CanDash()
	{
		if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling && !isRolling)
		{
			StartCoroutine(nameof(RefillDash), 1);
		}

		return _dashesLeft > 0;
	}

	public bool CanSlide()
    {
		if (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0)
			return true;
		else
			return false;
	}
    #endregion

    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
		Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
	}
    #endregion
}

// created by Dawnosaur :D