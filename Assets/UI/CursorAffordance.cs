using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG
{
    public class CursorAffordance : MonoBehaviour
    {
        public Material highlightMaterial;
        [SerializeField] private Texture2D walkCursor, targetCursor, unknownCursor;

        CameraRaycaster cameraRaycaster;
        MeshRenderer previousEnemyRenderer;
        Material previousEnemyMaterial;
        Camera mainCamera;

        void Start()
        {
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            mainCamera = FindObjectOfType<Camera>();
        }

        void FixedUpdate()
        {
            var hit = cameraRaycaster.hit; // Read current state
            UnhighlightEnemy();

            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    Cursor.SetCursor(walkCursor, Vector2.zero, CursorMode.Auto);
                    break;
                case Layer.Enemy:
                    HighlightEnemy(cameraRaycaster.hit.collider.gameObject);
                    Cursor.SetCursor(targetCursor, Vector2.zero, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(unknownCursor, Vector2.zero, CursorMode.Auto);
                    break;
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
}