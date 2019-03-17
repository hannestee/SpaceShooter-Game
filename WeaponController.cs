using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

	// Use this for initialization
	void Start ()
	{
	    Invoke("Fire", fireRate);
	}

    void Fire()
    {
        float randomTime = Random.Range(0.5f, 0.7f);
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Invoke("Fire", randomTime); 
    }

}
