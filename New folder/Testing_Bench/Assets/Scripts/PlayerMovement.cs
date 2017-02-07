using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    GameObject MainCamera;
    [SerializeField]
    float Movement_Speed = 3;
    GameObject Player;
    Rigidbody PlayerRB;
    [SerializeField]
    float JumpHeight = 2;
    GameObject Horizontal_Look_Rotator;
    GameObject Vertical_Look_Rotator;
    GameObject ZTranslator;
    GameObject XTranslator;
    [SerializeField]
    float VerticalSensitivity = 1;
    [SerializeField]
    float HorizontalSensitivity = 1;
    private Quaternion Horizontal_TargetRot;
    private Quaternion Vertical_TargetRot;
    GameObject Cube;

    // Use this for initialization
    void Start () {

        Player = GameObject.FindWithTag("Player");
        Horizontal_Look_Rotator = GameObject.FindWithTag("XRotator");
        Horizontal_Look_Rotator.transform.position = Player.transform.position;
        Vertical_Look_Rotator = GameObject.FindWithTag("YRotator");
        Vertical_Look_Rotator.transform.position = Player.transform.position;
        ZTranslator = GameObject.FindGameObjectWithTag("Z Translator");
        ZTranslator.transform.position = Player.transform.position;
        XTranslator = GameObject.FindGameObjectWithTag("X Translator");
        XTranslator.transform.position = Player.transform.position;
        MainCamera = GameObject.FindWithTag("MainCamera");
        MainCamera.transform.position = Player.transform.position;
        PlayerRB = Player.GetComponent<Rigidbody>();
        Cube = GameObject.Find("Cube");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

	}
    void MovementHandler()
    {

        if (Input.GetKey("a"))
        {
            PlayerRB.AddForce(transform.right * Movement_Speed * -1);
        }
        if (Input.GetKey("d"))
        {
            PlayerRB.MovePosition(transform.position + transform.right * Time.deltaTime * Movement_Speed);
        }
        if (Input.GetKey("w"))
        {
            PlayerRB.MovePosition(transform.position + transform.forward * Time.deltaTime * Movement_Speed);
        }
        if (Input.GetKey("s"))
        {
            PlayerRB.MovePosition(transform.position + transform.forward * Time.deltaTime * Movement_Speed * -1);
        }


        if (Input.GetKeyDown("space"))
        {
            PlayerRB.AddForce(0, JumpHeight, 0, ForceMode.VelocityChange);
        }
    }

    void RotationHandler()
    {
        
        Horizontal_TargetRot = Horizontal_Look_Rotator.transform.localRotation;
        Vertical_TargetRot = Vertical_Look_Rotator.transform.localRotation;

        float X = Input.GetAxis("Mouse X") * HorizontalSensitivity;
        float Y = Input.GetAxis("Mouse Y") * VerticalSensitivity;


        Horizontal_TargetRot *= Quaternion.Euler(0, X, 0);
        Vertical_TargetRot *= Quaternion.Euler(-Y, 0, 0);

        Horizontal_Look_Rotator.transform.localRotation = Horizontal_TargetRot;
        Vertical_Look_Rotator.transform.localRotation = Vertical_TargetRot;

        Player.transform.rotation = Horizontal_TargetRot * Vertical_TargetRot;


    }

    void CubeDestroyer()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            var cubeClone = (GameObject)Instantiate(Cube, hit.point + hit.normal, Quaternion.identity);
            var cubeCloneRB = (Rigidbody)cubeClone.GetComponent<Rigidbody>();
            cubeCloneRB.AddForce(0, 10, 0, ForceMode.Impulse);

            
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            hit.rigidbody.AddExplosionForce(25, hit.point, 1, 0, ForceMode.Impulse);

        }


    }
	
	// Update is called once per frame
	void Update () {

        RotationHandler();
        CubeDestroyer();

    }

    void FixedUpdate ()
    {
        MovementHandler();
    }
}
