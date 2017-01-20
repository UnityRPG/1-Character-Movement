using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Cursor : MonoBehaviour {

    public string[] layerPriorities { get; set; }

    public float maxDistance = 100f;
    Camera viewCamera;

    public RaycastHit? GetHighlighted(string layerName)
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
        layerPriorities = new string[] {
            Layers.Enemy,
            Layers.Walkable
        };
        viewCamera = Camera.main;
    }

    public bool GetHighlighted(out RaycastHit hit, string layerName)
    {
        var potentialHit = GetHighlighted(layerName);
        hit = potentialHit ?? new RaycastHit();
        return potentialHit != null;
    }
}