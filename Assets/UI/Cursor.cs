using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Cursor : MonoBehaviour, IRaycaster, IPriorityController {

    public string[] layerPriorities { get; set; }

    public float maxDistance = 100f;
    Camera viewCamera;

    ActionPrioritiser actionPrioritiser;

    RaycastHit? IRaycaster.RaycastForLayer(string layerName)
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
        actionPrioritiser = new ActionPrioritiser();
        actionPrioritiser.priorityController = this;
        actionPrioritiser.raycaster = this;
        layerPriorities = new string[] {
            Action.Enemy,
            Action.Walkable
        };
        viewCamera = Camera.main;
    }

    public bool GetHighlighted(out RaycastHit hit, string layerName)
    {
        var potentialHit = actionPrioritiser.GetHighlighted(layerName);
        hit = potentialHit ?? new RaycastHit();
        return potentialHit != null;
    }
}