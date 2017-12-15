using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private int gemCount = 0;
	private Animator anim;

	[System.Serializable]
	public class MoveSettings
	{

		public float forwardVel = 12;
		public float rotateVel = 100;
		public float jumpVel = 25;
		public float distToGrounded = 0.1f;
		public LayerMask ground;
	}

	[System.Serializable]
	public class PhysSettings
	{
		public float downAccel = 0.75f;
	}

	[System.Serializable]
	public class InputSettings
	{
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string TURN_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";

	}

	public MoveSettings moveSetting = new MoveSettings();
	public PhysSettings physSetting = new PhysSettings();
	public InputSettings inputSetting = new InputSettings();

	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	float forwardInput, turnInput, jumpInput;

	public Quaternion TargetRotation
	{
		get { return targetRotation; }
	}

	bool Grounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
	}

	void Start()
	{
		anim = GetComponent<Animator>();

		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody>())
			rBody = GetComponent<Rigidbody>();
		else
			Debug.LogError("The character needs a rigidbody");

		forwardInput = turnInput = jumpInput = 0;
			
		gemCount = 0;

	}

	void GetInput()
	{
		forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS);
		turnInput = Input.GetAxis(inputSetting.TURN_AXIS);
		jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);
		if (jumpInput > 0)
		{
			Debug.Log(jumpInput);
		}
	}

	void Update()
	{
		GetInput();
	}

	void FixedUpdate()
	{
		Run();
		Jump();
		Turn();
		rBody.velocity = transform.TransformDirection(velocity);

		//Character Walk
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) )
		{
			anim.SetBool("Sprint", true);
			anim.SetBool("Idle", false);
		}

		//Character Stop 
		if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			anim.SetBool("Idle", true);
			anim.SetBool("Sprint", false);

		}
	}

	void Run()
	{
		if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
		{
			velocity.z = moveSetting.forwardVel * forwardInput;
		}
		else
			velocity.z = 0;
	}

	void Turn()
	{
		if(Mathf.Abs(turnInput) > inputSetting.inputDelay)
		{
			targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
		}
		transform.rotation = targetRotation;
	}

	void Jump()
	{
		if (jumpInput > 0 && Grounded())
		{
			velocity.y = moveSetting.jumpVel;
		}
		else if (jumpInput ==0 && Grounded())
		{
			velocity.y = 0;
		}
		else
		{
			velocity.y -= physSetting.downAccel;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Collectable"))
		{
			other.gameObject.SetActive(false);
			gemCount++;
		}

		if (other.gameObject.CompareTag("Portal1"))
		{
			SceneManager.LoadScene("Level 2 Instructions");
		}
		if (other.gameObject.CompareTag ("End"))
		{
			SceneManager.LoadScene ("Win");
		}
	}

	public int getGems()
	{
		return gemCount;
	}
}