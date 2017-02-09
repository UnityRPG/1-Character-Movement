using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;

public class Sword : MonoBehaviour {

    [SerializeField] float swordSpinRPM = 240f;

    Quaternion initialRotation;
    Player player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        initialRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.isAttacking)
        {
            float swordSpinDegreesPerSecond = swordSpinRPM * 360 / 60;
            transform.Rotate(Vector3.left, swordSpinDegreesPerSecond * Time.deltaTime);
        }
        else
        {
            transform.localRotation = initialRotation;
        }
	}
}
