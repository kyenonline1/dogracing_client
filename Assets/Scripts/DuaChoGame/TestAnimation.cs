using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour {

    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        animator.PlayInFixedTime("BackgrounAnim", 0, 15f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
