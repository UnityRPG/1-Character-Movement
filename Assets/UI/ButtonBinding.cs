using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBinding : MonoBehaviour {

    Player player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
	}

    public void DealPlayerDamage(int amount) {
        player.TakeDamage(amount);
    }
}
