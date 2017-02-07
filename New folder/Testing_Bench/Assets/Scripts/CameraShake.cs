using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	GameObject PlayerCamera;
	GameObject Player;
	public float ShakeIntensity;



	// Use this for initialization
	void Start () {

		PlayerCamera = GameObject.FindWithTag("MainCamera");
		Player = GameObject.FindWithTag("Player");
	}

	void ShakeCamera ()
	{


	}

	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown(0))
		ShakeCamera();

	}
}
