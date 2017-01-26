using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] [Tooltip("To visualise switch to Scene and select enemy(s)")]
    float attackRadius = 1f;
    [SerializeField]
    int damagePointsPerAttack = 1;

    Player player;

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update() {
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= attackRadius)
        {
            InvokeRepeating("DealPeriodicDamage", 0f, 2f);
        }
        else
        {
            CancelInvoke();
        }
    }

    void DealPeriodicDamage()
    {
        player.DealDamage(damagePointsPerAttack);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
