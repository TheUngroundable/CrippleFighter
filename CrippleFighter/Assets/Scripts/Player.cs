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
    public bool hasSpecial = false;

    //Movement Attributes
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    //Utility
    public Combo combo;
    public GameObject turret;
    public Animator anim;
    public Camera cam;

    //Controller Attributes
    public float deadzone = 0.5f;
    private float LxDirection;
    private float RxDirection;
    private float LzDirection;
    private float RzDirection;

    // Use this for initialization
    private void Start()
    {
        inventory = new Stack<GameObject>();
        combo = new Combo();
        anim = GetComponent<Animator>();
        cam = (Camera)FindObjectOfType(typeof(Camera));
    }  

    void Update(){
        //Fire Button
        if (Input.GetButtonUp("Fire1")){

            Fire();
            anim.SetTrigger("shooting");
        }

        if (Input.GetButtonUp("Evaluate"))
        {

            Evaluate();

        }

        if (Input.GetButtonUp("Activate"))
        {

            if(turret != null)
                Activate();

        }

        //Falling System
        if (transform.position.y < -1){

            Destroy(this.gameObject, 2f);
            cam.GetComponent<CameraController>().targets.Remove(this.transform);

        }

        //Movement System
 
        anim.SetBool("isRunning", false);

        if (Mathf.Abs(Input.GetAxis("RHorizontal") )>= deadzone || Mathf.Abs(Input.GetAxis("RVertical")) >= deadzone) 
        {

            RxDirection = Input.GetAxis("RHorizontal") * rotationSpeed;
            RzDirection = Input.GetAxis("RVertical") * rotationSpeed;

        }

        if (Mathf.Abs(Input.GetAxis("LVertical")) >= deadzone || Mathf.Abs(Input.GetAxis("LHorizontal")) >= deadzone)
        {

            LxDirection = Input.GetAxis("LHorizontal") * speed * Time.deltaTime;
            LzDirection = Input.GetAxis("LVertical") * speed * Time.deltaTime;
            Vector3 movementDirection = new Vector3(LxDirection, 0, LzDirection);
            transform.Translate(movementDirection, Space.World);
            anim.SetBool("isRunning", true);

        }

        


        Vector3 targetRotation = new Vector3(RxDirection, 0, RzDirection);
        transform.rotation = Quaternion.LookRotation(targetRotation);
    
    }

    void Die(){

        Destroy(this.gameObject);

    }

    public void AddBullet(GameObject bullet)
    {
        if(!hasSpecial)
            inventory.Push(bullet);
    }

    public void Evaluate()
    {
        if (inventory.Count == 3)
        {

            Debug.Log("Evaluate");
            GameObject bullet = combo.Bullet(inventory);
            //clear stack
            inventory.Clear();
            //push bullet
            inventory.Push(bullet);
            hasSpecial = true;

        }
    }

    void Fire(){

        if (inventory.Count>0)
        {
            
            GameObject bulletPrefab = inventory.Pop();

            if (hasSpecial)
            {

                hasSpecial = false;

            }

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

    void Activate()
    {

        turret.GetComponent<Turret>().owner = this.gameObject;

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

