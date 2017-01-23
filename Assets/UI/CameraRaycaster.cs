using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    float maxDistance = 100f;
    Camera viewCamera;

    private RaycastHit? GetHighlighted(Layer layerName)
    {
        foreach (Layer currentLayerName in layerPriorities)
        {
            var hit = RaycastForLayer(currentLayerName);
            if (hit != null)
            {
                if (currentLayerName != layerName)
                {
                    return null;
                }
                else
                {
                    return hit;
                }
            }
        }
        return null;
    }

    public bool GetHighlighted(out RaycastHit hit, Layer layerName)
    {
        var potentialHit = GetHighlighted(layerName);
        hit = potentialHit ?? new RaycastHit();
        return potentialHit != null;
    }

    RaycastHit? RaycastForLayer(Layer layerName)
    {
        int layerMask = 1 << (int)layerName;
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