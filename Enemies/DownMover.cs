using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMover : MonoBehaviour {

    public bool updateOn = true;
    private new Rigidbody rigidbody;

    public float speed;
    public float durationToStop;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(updateOff());
    }

    void Update()
    {
        if (updateOn == true)
        {
            rigidbody.velocity = transform.forward * speed;
        }
        // if you want certain parts of update to work at all times write them here.
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
