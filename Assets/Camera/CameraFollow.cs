using UnityEngine;
using System.Collections;

namespace RPG
{
    public class CameraFollow : MonoBehaviour
    {

        GameObject player;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        void LateUpdate() // Modelled from UnityStandardAssets.Utility FollowTarget
        { 
            transform.position = player.transform.position;
        }
    }
}