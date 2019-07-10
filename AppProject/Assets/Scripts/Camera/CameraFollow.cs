using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic script to make camera follow player
public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    public float offset = 20;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, (player.transform.position.z - offset));
    }
}
