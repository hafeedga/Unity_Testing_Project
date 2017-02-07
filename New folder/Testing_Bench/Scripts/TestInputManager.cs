using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestInputManager : MonoBehaviour
{

    public GameObject Cube;
    public GameObject Player;
    public Vector3 PlayerPos;
    public Vector3 CubePos;

    [SerializeField]
    private float zOffset = 5;
    [SerializeField]
    private float yOffset = 0;
    [SerializeField]
    private float xOffset = 0;

    void SpawnTheCube()
    {
        //assign the gameobjects
        Player = GameObject.FindWithTag("Player");
        Cube = GameObject.FindWithTag("Respawn");

        CubePos = Player.transform.position + new Vector3(0,0,zOffset);
        
        Instantiate(Cube, CubePos, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKey("up"))
            SpawnTheCube();
    }
}
