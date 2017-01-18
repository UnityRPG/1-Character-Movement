using UnityEngine;

interface IRaycaster
{
    RaycastHit? RaycastForLayer(string layerName);
}