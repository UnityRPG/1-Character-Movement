using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    GameObject player;
	
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
    }

	void Update () {
        var targetPosition = player.transform.position;
        transform.position = targetPosition;
	}
}
