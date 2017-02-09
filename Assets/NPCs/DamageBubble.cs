using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageBubble : MonoBehaviour {

    RectTransform rectTransform = null;
    Text bubbleText = null;

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
        bubbleText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Note copy-paste from player
    public void DoFixedDamage(int points) // TODO public required
    {
        bubbleText.text = points.ToString();
        Invoke("FadeBubble", .5f);
        // TODO trigger animation instead
    }

    void FadeBubble()
    {
        bubbleText.text = "";
    }

}
