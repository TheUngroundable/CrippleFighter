using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    public int health = 3;
    public int playerNumber = 0;


    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public float deadzone = 0.4f;
    private float LxDirection;
    private float RxDirection;
    private float LzDirection;
    private float RzDirection;

    // Use this for initialization

    // Update is called once per frame
    void Update(){

        if (Input.GetButtonUp("Fire1")){

            Fire();
        
        }
        //Death conditions
        if (health<=0){

            Die();

        }

        if (transform.position.y < 0){

            Destroy(this.gameObject, 2f);

        }

        //Left Analog
 
        LxDirection = Input.GetAxis("LHorizontal") * speed;
        LzDirection = Input.GetAxis("LVertical") * speed;

        if (Mathf.Abs(Input.GetAxis("RHorizontal") )>= deadzone || Mathf.Abs(Input.GetAxis("RVertical")) >= deadzone) 
        {

            RxDirection = Input.GetAxis("RHorizontal") * rotationSpeed;
            RzDirection = Input.GetAxis("RVertical") * rotationSpeed;

        }

       

        // Make it move 10 meters per second instead of 10 meters per frame...
        LzDirection *= Time.deltaTime;
        LxDirection *= Time.deltaTime;
        //rotation *= Time.deltaTime;
        Vector3 movementDirection = new Vector3(LxDirection, 0, LzDirection);
        // Move translation along the object's z-axis
        transform.Translate(movementDirection, Space.World);


        Vector3 targetRotation = new Vector3(RxDirection, 0, RzDirection);
        //transform.LookAt(targetRotation);
        transform.rotation = Quaternion.LookRotation(targetRotation);
        Debug.Log("Lx: " + LxDirection + "; Lz: " + LzDirection + " - Rx: " + RxDirection + "; Rz: " + RzDirection);
  
    }

    void Die(){

        Destroy(this.gameObject);

    }

    void Fire(){

        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
        bulletPrefab,
        bulletSpawn.position,
        bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().speed;
        bullet.GetComponent<Bullet>().owner = this.gameObject;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 10.0f);

    }

}

