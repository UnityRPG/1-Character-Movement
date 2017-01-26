using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class UpdateHealthBar : MonoBehaviour
    {

        Enemy enemy;
        Text healthText;

        // Use this for initialization
        void Start()
        {
            enemy = GetComponentInParent<Enemy>();
            healthText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            healthText.text = ((int)(enemy.healthAsPercentage * 100)).ToString();
        }
    }
}