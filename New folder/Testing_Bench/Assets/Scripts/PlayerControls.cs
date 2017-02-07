using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	GameObject Player;
	Rigidbody RB_Player;
	GameObject MainCamera;
	GameObject Horizontal_Look_Rotator;
	GameObject Vertical_Look_Rotator;
	GameObject ZTranslator;
	GameObject XTranslator;
	public float Horizontal_Look_Sensitivity = 3f;
	public float Vertical_Look_Sensitivity = 3f;
	public float Movement_Speed = 1f;
	public float Upper_Look_Limit = 80f;
	public float Lower_Look_Limit = -80f;
	float X_Rotation;
	float Y_Rotation;
	GameObject ExplosionTest;
	ParticleSystem ExplosionTestPS;
	GameObject Cube;

	// Use this for initialization
	void Start () {

		Player = GameObject.FindWithTag("Player");
		RB_Player = Player.GetComponent<Rigidbody>();
		MainCamera = GameObject.FindWithTag("MainCamera");
		Horizontal_Look_Rotator = GameObject.FindWithTag("XRotator");
		Vertical_Look_Rotator = GameObject.FindWithTag("YRotator");
		ZTranslator = GameObject.FindWithTag("Z Translator");
		XTranslator = GameObject.FindWithTag("X Translator");
		Cube = GameObject.FindWithTag("Respawn");

		//Sets the location and rotation of the rotators equal to the player transform
		Horizontal_Look_Rotator.transform.localPosition = Vector3.zero;
		Horizontal_Look_Rotator.transform.localRotation = Quaternion.identity;
		Vertical_Look_Rotator.transform.localPosition = Vector3.zero;
		Vertical_Look_Rotator.transform.localRotation = Quaternion.identity;

		//sets the location and rotation of the translators equal to the player transform
		ZTranslator.transform.localPosition = Vector3.zero;
		ZTranslator.transform.localRotation = Quaternion.identity;
		XTranslator.transform.localPosition = Vector3.zero;
		XTranslator.transform.localRotation = Quaternion.identity;
		ExplosionTest = GameObject.Find("Explosion");
		ExplosionTestPS = ExplosionTest.GetComponent<ParticleSystem>();


	}


	void RotationHandler()
	{
		var x = Input.GetAxis("Mouse X");
		var y = Input.GetAxis("Mouse Y");

		Quaternion Horizontal_Target = Horizontal_Look_Rotator.transform.localRotation;
		Quaternion Vertical_Target = Vertical_Look_Rotator.transform.localRotation;

		Y_Rotation += x * Horizontal_Look_Sensitivity;
		Horizontal_Target = Quaternion.Euler(0f, Y_Rotation, 0f);

		X_Rotation -= y * Vertical_Look_Sensitivity;
		X_Rotation = Mathf.Clamp(X_Rotation, Lower_Look_Limit, Upper_Look_Limit);

		Vertical_Target = Quaternion.Euler(X_Rotation, 0f, 0f);

		Player.transform.rotation = Horizontal_Target * Vertical_Target;

	}

	void MovementHandler()
	{

		if (Input.GetKey("a"))
		{
			RB_Player.MovePosition(transform.position + transform.right * Time.deltaTime * Movement_Speed * -1);
		}
		if (Input.GetKey("d"))
		{
			RB_Player.MovePosition(transform.position + transform.right * Time.deltaTime * Movement_Speed);
		}
		if (Input.GetKey("w"))
		{
			RB_Player.MovePosition(transform.position + transform.forward * Time.deltaTime * Movement_Speed);
		}
		if (Input.GetKey("s"))
		{
			RB_Player.MovePosition(transform.position + transform.forward * Time.deltaTime * Movement_Speed * -1);
		}
	}

	void BlowUp()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast(ray, out hit))
		{
			return;
		}

		Collider[] colliders = Physics.OverlapSphere(hit.point, 100);
		foreach (Collider Col_hit in colliders)
		{
			Rigidbody rb = Col_hit.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(500f, hit.point, 10f, 4f);
		}

		var ExplosionClone = (GameObject)Instantiate(ExplosionTest, hit.point + hit.normal, Quaternion.identity);
	}

	void CubeSpawner()
	{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (!Physics.Raycast(ray, out hit))
				return;

			var CubeClone = (GameObject)Instantiate(Cube, hit.point + hit.normal, Quaternion.identity);
	}

	void FixedUpdate ()
	{
		MovementHandler();
	}

	// Update is called once per frame
	void Update () {

		RotationHandler();

		if (Input.GetMouseButtonDown(0))
			CubeSpawner();

		if (Input.GetMouseButtonDown(1))
			BlowUp();

	}
}