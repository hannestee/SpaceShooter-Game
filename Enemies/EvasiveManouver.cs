﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManouver : MonoBehaviour
{

    public Vector2 startWait;
    public float dodge;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;
    public float tilt;
    public float smoothing;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;
	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	    currentSpeed = rb.velocity.z;
	    StartCoroutine(Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x,maneuverWait.y));
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver,0,currentSpeed);
        rb.position = new Vector3
            (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0,0,rb.velocity.x *-tilt);
	}
}
