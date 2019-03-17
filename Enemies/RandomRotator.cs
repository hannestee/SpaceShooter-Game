using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble;
    public new Rigidbody rigidbody;
	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody>();
	    rigidbody.angularVelocity = Random.insideUnitSphere*tumble;
	}
	
}
