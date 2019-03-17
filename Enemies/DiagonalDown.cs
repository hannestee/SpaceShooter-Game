using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalDown : MonoBehaviour {
    public bool updateOn = true;
    private new Rigidbody rigidbody;
    public Vector3 Aasd = Quaternion.Euler(0, 45, 0) * Vector3.right;

    public float speed;
    public float durationToStop;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(updateOff());
        
    }
	
	// Update is called once per frame
	void Update () {
        if (updateOn == true)
        {
            //rigidbody.velocity = transform.forward * speed;
            rigidbody.AddForce(Aasd * speed);
        }
    }

    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(durationToStop);
        updateOn = false;
        rigidbody.velocity = transform.forward * speed * 0.1f;
        yield return new WaitForSeconds(0.3f);
        rigidbody.velocity = transform.forward * speed * 0.05f;
        yield return new WaitForSeconds(0.2f);
        rigidbody.velocity = transform.forward * speed * 0.03f;
        yield return new WaitForSeconds(0.1f);
        rigidbody.velocity = transform.forward * speed * 0;
        rigidbody.velocity = transform.forward * 0;
        GetComponent<EnemyRotator>().enabled = true;
        GetComponent<WeaponController>().enabled = true;
    }
    
}
