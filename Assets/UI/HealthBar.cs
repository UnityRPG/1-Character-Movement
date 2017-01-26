using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    RawImage healthBarRawImage;
    Player player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        healthBarRawImage = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
        float xValue = -(player.healthAsPercentage / 2f) - 0.5f;  // TODO note hard code
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
	}
}
