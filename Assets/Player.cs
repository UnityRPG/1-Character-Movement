using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int currentHealthPoints; // TODO remove
    [SerializeField] int initialHealthPoints = 100;
    [SerializeField] int maxHealthPoints = 100;

    // Use this for initialization
    void Start () {
        currentHealthPoints = initialHealthPoints;
	}

    public float healthAsPercentage {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    void DealDamage(int damagePoints)
    {
        var newHealthPoints = currentHealthPoints - damagePoints;
        currentHealthPoints = Mathf.Clamp(newHealthPoints, 0, maxHealthPoints);
    }
}
