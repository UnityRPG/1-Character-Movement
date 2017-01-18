using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
    NavMeshAgent agent;
    Cursor cursor;
    MeshRenderer previousEnemyRenderer;
    Material previousEnemyMaterial;
    public Material highlightMaterial;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cursor = FindObjectOfType<Cursor>();
    }

    void Update () {
        // When moused over an enemy:
        RaycastHit hit;
        if (cursor.GetHighlighted(out hit, Action.Enemy))
        {
            // The enemy should be highlighted
            var enemy = hit.rigidbody.gameObject;
            HighlightEnemy(enemy);

            // If we click on an enemy, our character should:
            if (Input.GetButtonDown(Button.PrimaryAction))
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
