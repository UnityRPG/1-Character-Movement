using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    [SerializeField] [Tooltip("To visualise switch to Scene and select enemy(s)")]
    float attackRadius = 1f;
    [SerializeField]
    int damagePointsPerAttack = 1;
    [SerializeField]
    float secondsBetweenAttacks = 0.1f;
    [SerializeField]
    Material attackingMaterial;
    [SerializeField]
    int maxHealthPoints = 100;

    int currentHealthPoints;
    Player player;
    NavMeshAgent navMeshAgent;
    bool isAttacking = false;
    SkinnedMeshRenderer enemySkin;

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySkin = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update() {
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= attackRadius)
        {
            navMeshAgent.SetDestination(player.transform.position);
            if (!isAttacking) {
                isAttacking = true;
                InvokeRepeating("PeriodicallyAttackPlayer", 0f, secondsBetweenAttacks); // TODO avoid string reference?
            }
        }
        else
        {
            CancelInvoke();
            isAttacking = false;
        }
    }

    // Note copy-paste from player
    public void TakeDamage(int damagePoints)
    {
        var newHealthPoints = currentHealthPoints - damagePoints;
        currentHealthPoints = Mathf.Clamp(newHealthPoints, 0, maxHealthPoints);
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    void PeriodicallyAttackPlayer()
    {
        player.TakeDamage(damagePointsPerAttack);
        enemySkin.material = attackingMaterial;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
