using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    public class Player : MonoBehaviour {

        public int currentHealthPoints; // TODO remove
        [SerializeField] int initialHealthPoints = 100;
        [SerializeField] int maxHealthPoints = 100;
        [SerializeField] Material ghostMaterial;

        [SerializeField] AudioClip[] deathSounds;
        [SerializeField] AudioClip[] startSounds;
        [SerializeField] float radialDamageRadius = 5f;
        [SerializeField] float radialDamagePerSecond = 10f;
        [SerializeField] GameObject currentTarget = null;

        bool isAlive = true;
        SkinnedMeshRenderer playerSkin;


        // Use this for initialization
        void Start() {
            currentHealthPoints = initialHealthPoints;
            playerSkin = GetComponentInChildren<SkinnedMeshRenderer>();
            AudioSource.PlayClipAtPoint(PickRandomAudioClip(startSounds), transform.position); // TODO stick to player
            InvokeRepeating("DoRadialDamage", 0f, 1f);
        }

        

        public void SetTarget(GameObject target)
        {
            currentTarget = target;
        }

        public void ClearTarget()
        {
            currentTarget = null;
        }

        public bool isAttacking
        {
            get
            {
                return currentTarget != null;
            }
        }

        public float healthAsPercentage
        {
            get
            {
                return currentHealthPoints / (float)maxHealthPoints;
            }
        }

        public void TakeDamage(int damagePoints)
        {
            var newHealthPoints = currentHealthPoints - damagePoints;
            currentHealthPoints = Mathf.Clamp(newHealthPoints, 0, maxHealthPoints);
        }

        void Update()
        {
            if (!isAlive) { return; }

            if (currentHealthPoints == 0)
            {
                isAlive = false;
                AudioSource.PlayClipAtPoint(PickRandomAudioClip(deathSounds), transform.position);
                playerSkin.material = ghostMaterial;
                gameObject.SetActive(false);
            }
        }

        void DoRadialDamage()
        {
            LayerMask layerMask = 1 << (int)Layer.Enemy;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radialDamageRadius, layerMask);
            int i = 0;
            while (i < hitColliders.Length)
            {
                hitColliders[i].SendMessage("DoFixedDamage", radialDamagePerSecond);
                i++;
            }
        }

        AudioClip PickRandomAudioClip(AudioClip[] audioClips)
        {
            return audioClips[Random.Range(0, audioClips.Length)];
        }
    }
}