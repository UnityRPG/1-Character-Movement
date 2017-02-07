using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBlob : MonoBehaviour {

    Animator animator;
    SkinnedMeshRenderer skinnedMeshRenderer;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        skinnedMeshRenderer.SetBlendShapeWeight(0, animator.GetFloat("Key 1"));
        skinnedMeshRenderer.SetBlendShapeWeight(1, animator.GetFloat("Key 2"));
    }
}
