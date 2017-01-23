using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField] float health = 1f;

	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponentInChildren<TextMesh> ();
	}
		
	// Update is called once per frame
	void Update () {
		int healthPercent = Mathf.RoundToInt(health * 100); // Note bracketing
		if (Mathf.Approximately(health, 0f))
		{
			textMesh.text = "RIP";
		}
		else
		{
			textMesh.text = healthPercent.ToString ();
		}
	}


	public void DealDamage (float amount) {
		var damageToDeal = (health - amount / 100);
		health = Mathf.Clamp (damageToDeal, 0f, 1f);
	}
}
