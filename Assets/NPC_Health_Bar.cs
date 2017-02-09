using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class NPC_Health_Bar : MonoBehaviour {

    RawImage healthBarRawImage;

    Enemy enemy = null;

    // Use this for initialization
    void Start () {
        enemy = GetComponentInParent<Enemy>();
        healthBarRawImage = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
        float xValue = -(enemy.healthAsPercentage / 2f) - 0.5f;  // TODO note hard code
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
