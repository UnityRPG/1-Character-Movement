using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class PlayerCombat : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent agent;
    CameraRaycaster cameraRaycaster;
    MeshRenderer previousEnemyRenderer;
    Material previousEnemyMaterial;
    public Material highlightMaterial;
	Camera mainCamera;

	[SerializeField]
	private float currentRange = 3.0F;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, currentRange);
	}

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
		mainCamera = FindObjectOfType<Camera> ();
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
				UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
				agent.CalculatePath (hit.point, path);
				if (PathGoesOffScreen (path)) {
					Debug.LogWarning ("Won't pathfind off screen");
				} else {
					agent.SetPath (path);
				}

                // Perform an attack on the enemy.
            }
        }
        else
        {
            // Unhighlight previously highlighted enemy.
            UnhighlightEnemy();
        }
    }

	private bool PathGoesOffScreen(NavMeshPath path) {
		Vector3[] pathCorners = new Vector3[4];
		path.GetCornersNonAlloc (pathCorners);
		foreach (Vector3 corner in pathCorners) {
			var screenPoint = mainCamera.WorldToScreenPoint (corner);
			if (screenPoint.x < 0f || screenPoint.y < 0f) { // Trying to route off screen
				return true;
			}
		}
		return false;
	}


	// Gets the position from where the agent should range attach
	private Vector3 GetRangeAttackPosition()
	{
		return Vector3.zero; // TODO impliment
		
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
