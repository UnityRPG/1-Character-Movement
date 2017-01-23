using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    public string[] layerPriorities = {
        Layers.Enemy,
        Layers.Walkable
    };

    float maxDistance = 100f;
    Camera viewCamera;

    private RaycastHit? GetHighlighted(string layerName)
    {
        foreach (string currentLayerName in layerPriorities)
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

    public bool GetHighlighted(out RaycastHit hit, string layerName)
    {
        var potentialHit = GetHighlighted(layerName);
        hit = potentialHit ?? new RaycastHit();
        return potentialHit != null;
    }

    RaycastHit? RaycastForLayer(string layerName)
    {
        int layerMask = LayerMask.GetMask(layerName);
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