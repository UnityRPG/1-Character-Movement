using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	[SerializeField]
	private float health = 1f;

	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponentInChildren<TextMesh> ();
	}
		
	// Update is called once per frame
	void Update () {
		int healthPercent = (int)(health * 100); // Note bracketing
		textMesh.text = healthPercent.ToString ();
	}
}
