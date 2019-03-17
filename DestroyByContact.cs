using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject player;

    public int scoreValue;
    private GameController gameController;
    // Use this for initialization

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();

        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' Script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy1" || other.tag == "Enemy2")
        {
            return;
        }

        if (other.tag == "Wep1" && tag == "Enemy1")
        {
            //Debug.Log("Enemy1 not hit with wep1");
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            return;
        }

        if (other.tag == "Wep2" && tag == "Enemy2")
        {
            //Debug.Log("Enemy2 not hit with wep2");
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            return;
        }
        //Debug.Log("exploded");
        //Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Damage(5);
            Destroy(gameObject);
            //Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver();
        }

        //gameController.AddScore(10);
        //Destroy(other.gameObject);
        //Destroy(gameObject);
        /*
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        */
    }
	
}
