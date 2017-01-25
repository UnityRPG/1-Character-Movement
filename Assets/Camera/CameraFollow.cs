using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    GameObject player;
	
    void Start ()
    {
        // Find with tag as Player class isn't very "componenty"
        player = GameObject.FindWithTag("Player");
    }

	void LateUpdate () { // Modelled from UnityStandardAssets.Utility FollowTarget
        transform.position = player.transform.position; 
	}
}
