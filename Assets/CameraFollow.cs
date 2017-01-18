using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    GameObject player;
	
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
    }

	// Update is called once per frame
	void Update () {
        var targetPosition = player.transform.position;
        transform.position = targetPosition;
	}
}
