using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    float maxDistance = 100f;
    Camera viewCamera;

    public struct TopHitResult
    {
        public TopHitResult(Layer layer, RaycastHit hit)
        {
            this.layer = layer;
            this.raycastHit = hit;
        }
        public Layer layer;
        public RaycastHit raycastHit;
    }

    public TopHitResult? LookForPriorities()
    {
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue) {
                return new TopHitResult(layer, hit.Value); ;
            }
        }
        return null;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, maxDistance, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }

    void Start ()
    {
        viewCamera = Camera.main;
    }


}