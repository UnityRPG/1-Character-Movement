using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    GameObject player;
	
    void Start ()
    {
        // Find with tag as Player class isn't very "componenty"
        player = GameObject.FindWithTag("Player");
    }

	void Update () { // Cursor is taking LateUpdate to prevent race
        transform.position = player.transform.position;
	}
}
