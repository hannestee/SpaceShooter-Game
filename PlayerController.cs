using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HardShellStudios.CompleteControl;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    public GameObject shot2;
    public Transform shotSpawn2;
    public float fireRate2;

    public GameObject shot3;
    public Transform shotSpawn3;
    public float fireRate3;

    public new Rigidbody rigidbody;
    private float nextFire;

    public GameObject item1;
    private bool showItem1;

    public GameObject item2;
    private bool showItem2;

    public GameObject item3;
    private bool showItem3;

    [SerializeField]
    public Stat health;

    private GameController gameController;
    public GameObject explosion;
    public GameObject playerExplosion;

    public Renderer rend;
    private Color originalColor;

    public Text wep1Text;
    public Text wep2Text;
    public Text wep3Text;


    private void Awake()
    {
        health.Initialize();
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();

        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' Script");
        }

        showItem1 = false;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (showItem1 == false)
        {
            item1.SetActive(false);
        }

        if (showItem1 == true)
        {
            item1.SetActive(true);
        }

        if (showItem2 == false)
        {
            item2.SetActive(false);
        }

        if (showItem2 == true)
        {
            item2.SetActive(true);
        }

        if (showItem3 == false)
        {
            item3.SetActive(false);
        }

        if (showItem3 == true)
        {
            item3.SetActive(true);
        }

        if (hInput.GetButton("WepSelect1") && showItem1 == false)
        {
            showItem1 = true;
            showItem2 = false;
            showItem3 = false;
        }

        if (hInput.GetButton("WepSelect2") && showItem2 == false)
        {
            showItem2 = true;
            showItem1 = false;
            showItem3 = false;
        }

        if (hInput.GetButton("WepSelect3") && showItem3 == false)
        {
            showItem3 = true;
            showItem1 = false;
            showItem2 = false;
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire || Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (showItem1 == true)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }

            if (showItem2 == true)
            {
                Instantiate(shot2, shotSpawn2.position, shotSpawn2.rotation);
            }

            if (showItem3 == true)
            {
                Instantiate(shot3, shotSpawn3.position, shotSpawn3.rotation);
            }
        }

        /*if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate2;
            Instantiate(shot2, shotSpawn2.position, shotSpawn2.rotation);
        }
        */
    }

    void FixedUpdate () 
	{
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler(0, 0, rigidbody.velocity.x * -tilt);
	}

    public void Damage(float damage)
    {
        health.CurrentVal -= damage;
        StartCoroutine(efekt());
        string[] fruits = { "Z", "X", "C", "V","B","N","M"};
        string newWep = fruits.RandomItem();
        string newWep2 = fruits.RandomItem();
        string newWep3 = fruits.RandomItem();

        while (newWep2 == newWep)
        {
            newWep2 = fruits.RandomItem();
        }

        while (newWep3 == newWep || newWep3 == newWep2)
        {
            newWep3 = fruits.RandomItem();
        }

        //Debug.Log(fruits.RandomItem());
        KeyCode Wep1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), newWep);
        KeyCode Wep2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), newWep2);
        KeyCode Wep3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), newWep3);
        showItem1 = false;
        showItem2 = false;
        showItem3 = false;
        hInput.SetKey("WepSelect1", Wep1);
        hInput.SetKey("WepSelect2", Wep2);
        hInput.SetKey("WepSelect3", Wep3);
        wep1Text.text = newWep;
        wep2Text.text = newWep2;
        wep3Text.text = newWep3;

        if (health.CurrentVal <= 0)
        {
            Instantiate(playerExplosion, rigidbody.transform.position, rigidbody.transform.rotation);
            Destroy(gameObject);
            gameController.GameOver();
        }
    }

    IEnumerator efekt()
    {
        originalColor = rend.material.color;
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = originalColor;
    }
}


public static class ArrayExtensions
{
    // This is an extension method. RandomItem() will now exist on all arrays.
    public static T RandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}