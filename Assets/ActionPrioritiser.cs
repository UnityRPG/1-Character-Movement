using UnityEngine;

public class ActionPrioritiser {
    internal IRaycaster raycaster;
    internal IPriorityController priorityController;

    public RaycastHit? GetHighlighted(string layerName)
    {
        foreach (string currentLayerName in priorityController.layerPriorities)
        {
            var hit = raycaster.RaycastForLayer(currentLayerName);
            if (hit != null)
            {
                if (currentLayerName != layerName)
                {
                    return null;
                } else
                {
                    return hit;
                }
            }
        }
        return null;
    }

}
