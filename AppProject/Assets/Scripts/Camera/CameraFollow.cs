using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (player.transform.position.z - 20));
    }
}
