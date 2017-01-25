using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG
{
    public class PlayerCombat : MonoBehaviour
    {
        public Material highlightMaterial;

        [SerializeField]
        float currentRange = 3.0F;
        [SerializeField]
        float damagePointsPerHit = 5f;
        [SerializeField]
        float hitDelayInSeconds = 0.5f;

        GameObject currentTarget;
        UnityEngine.AI.NavMeshAgent agent;
        CameraRaycaster cameraRaycaster;
        MeshRenderer previousEnemyRenderer;
        Material previousEnemyMaterial;
        Camera mainCamera;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            mainCamera = FindObjectOfType<Camera>();
        }

        void Update()
        {
            // When moused over an enemy:
            var hit = cameraRaycaster.hit;
            if (cameraRaycaster.layerHit == Layer.Enemy)
            {
                currentTarget = cameraRaycaster.hit.collider.gameObject;
                HighlightEnemy(currentTarget);

                // If we click on an enemy, our character should:
                if (Input.GetButtonDown(Buttons.PrimaryAction))
                {
                    // Move within range of the enemy.
                    //				UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
                    //				agent.CalculatePath (hit.point, path);
                    //				agent.radius = currentRange;
                    //				if (PathGoesOffScreen (path)) {
                    //					Debug.LogWarning ("Won't pathfind to enemy via off-screen route");
                    //					agent.Move (new Vector3 (1, 1, 1));
                    //				} else {
                    //					agent.SetPath (path);
                    //				}

                    // Perform an attack on the enemy.
                    CancelInvoke();
                    InvokeRepeating("DealDamage", 0f, hitDelayInSeconds);
                }
            }
            else
            {
                // Unhighlight previously highlighted enemy.
                UnhighlightEnemy();
            }
        }

        private void DealDamage()
        {
            currentTarget.GetComponentInParent<Health>().DealDamage(damagePointsPerHit);
        }

        private bool PathGoesOffScreen(NavMeshPath path)
        {
            Vector3[] pathCorners = new Vector3[4];
            path.GetCornersNonAlloc(pathCorners);
            foreach (Vector3 corner in pathCorners)
            {
                var screenPoint = mainCamera.WorldToScreenPoint(corner);
                if (screenPoint.x < 0f || screenPoint.y < 0f)
                { // Trying to route off screen
                    return true;
                }
            }
            return false;
        }

        private void UnhighlightEnemy()
        {
            if (previousEnemyRenderer != null)
            {
                previousEnemyRenderer.material = previousEnemyMaterial;
                previousEnemyRenderer = null;
            }
        }

        private void HighlightEnemy(GameObject enemy)
        {
            if (previousEnemyRenderer == null)
            {
                var previousEnemy = enemy;
                previousEnemyRenderer = previousEnemy.GetComponentInChildren<MeshRenderer>();
                previousEnemyMaterial = previousEnemyRenderer.material;
                previousEnemyRenderer.material = highlightMaterial;
            }
        }
    }
}