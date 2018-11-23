using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    //Shooting Attributes
    public Transform bulletSpawn;

    //Player Attributes
    public int health = 3;
    public int playerNumber = 0;
    public Stack<GameObject> inventory;

    //Movement Attributes
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    //Controller Attributes
    public float deadzone = 0.4f;
    private float LxDirection;
    private float RxDirection;
    private float LzDirection;
    private float RzDirection;

    // Use this for initialization
    private void Start()
    {
        inventory = new Stack<GameObject>();
    }

    void Update(){
        //Fire Button
        if (Input.GetButtonUp("Fire1")){

            Fire();
        
        }
       
        //Falling System
        if (transform.position.y < 0){

            Destroy(this.gameObject, 2f);

        }

        //Movement System
 
        LxDirection = Input.GetAxis("LHorizontal") * speed;
        LzDirection = Input.GetAxis("LVertical") * speed;

        if (Mathf.Abs(Input.GetAxis("RHorizontal") )>= deadzone || Mathf.Abs(Input.GetAxis("RVertical")) >= deadzone) 
        {

            RxDirection = Input.GetAxis("RHorizontal") * rotationSpeed;
            RzDirection = Input.GetAxis("RVertical") * rotationSpeed;

        }
 
        LzDirection *= Time.deltaTime;
        LxDirection *= Time.deltaTime;
        Vector3 movementDirection = new Vector3(LxDirection, 0, LzDirection);
        transform.Translate(movementDirection, Space.World);


        Vector3 targetRotation = new Vector3(RxDirection, 0, RzDirection);
        transform.rotation = Quaternion.LookRotation(targetRotation);
    
    }

    void Die(){

        Destroy(this.gameObject);

    }

    public void AddBullet(GameObject bullet)
    {

        inventory.Push(bullet);

    }

    void Fire(){

        if (inventory.Count>0)
        {

            GameObject bulletPrefab = inventory.Pop();

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

    public void TakeDamage(int damage)
    {

        health -= damage;

        if (health <= 0)
        {

            Die();

        }

    }

}

