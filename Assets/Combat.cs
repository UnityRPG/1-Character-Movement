using UnityEngine;
using System.Collections;


public class Combat : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent agent;
    CameraRaycaster cameraRaycaster;
    MeshRenderer previousEnemyRenderer;
    Material previousEnemyMaterial;
    public Material highlightMaterial;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
    }

    void Update () {
        // When moused over an enemy:
        if (cameraRaycaster.layerHit == Layer.Enemy)
        {
            var hit = cameraRaycaster.hit;
            var enemyRigidBody = hit.rigidbody; // TODO should this be here?
            if (!enemyRigidBody)
            {
                Debug.LogWarning("Check enemy has rigid body");
                return;
            }

            var enemy = enemyRigidBody.gameObject;
            HighlightEnemy(enemy);
 
            // If we click on an enemy, our character should:
            if (Input.GetButtonDown(Buttons.PrimaryAction))
            {
                // Move within range of the enemy.
                agent.SetDestination(hit.point);
                // Perform an attack on the enemy.
            }
        }
        else
        {
            // Unhighlight previously highlighted enemy.
            UnhighlightEnemy();
        }
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
