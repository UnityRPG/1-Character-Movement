using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("To visualise switch to Scene and select enemy(s)")]
        float attackRadius = 1f;
        [SerializeField]
        int damagePointsPerAttack = 1;
        [SerializeField]
        float secondsBetweenAttacks = 0.1f;
        [SerializeField]
        Material attackingMaterial;
        [SerializeField]
        int maxHealthPoints = 100;
        [SerializeField]
        Material ghostMaterial;
        [SerializeField]
        AudioClip[] damageSounds;

        int currentHealthPoints;
        Player player;
        NavMeshAgent navMeshAgent;
        bool isAttacking = false;
        // bool isBeingAttacked = false;
        SkinnedMeshRenderer enemySkin;
        bool isAlive = true;

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<Player>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemySkin = GetComponentInChildren<SkinnedMeshRenderer>();
            currentHealthPoints = maxHealthPoints;
        }

        // Update is called once per frame
        void Update()
        {
            float distanceToPlayer = (player.transform.position - transform.position).magnitude;
            if (distanceToPlayer <= attackRadius)
            {
                navMeshAgent.SetDestination(player.transform.position);
                if (!isAttacking)
                {
                    isAttacking = true;
                    InvokeRepeating("PeriodicallyAttackPlayer", 0f, secondsBetweenAttacks); // TODO avoid string reference?
                }
            }
            else
            {
                CancelInvoke();
                isAttacking = false;
            }

            if (!isAlive) { return; }

            if (currentHealthPoints == 0)
            {
                isAlive = false;
                // AudioSource.PlayClipAtPoint(PickRandomAudioClip(deathSounds), transform.position); TODO add enemy death sounds
                enemySkin.material = ghostMaterial;
                CancelInvoke();
                gameObject.SetActive(false);
            }
        }

        // Note copy-paste from player
        public void DoFixedDamage(int points)
        {
            var newHealthPoints = currentHealthPoints - points; // TODO note hard code due to string reference of messsage
            currentHealthPoints = Mathf.Clamp(newHealthPoints, 0, maxHealthPoints);
            AudioSource.PlayClipAtPoint(damageSounds[Random.Range(0, damageSounds.Length)], transform.position);
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
}