using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour{

    //Components
    private Animator anim;

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
    private Combo combo;
    public GameObject turret;
    public Camera cam;

    //Controller Attributes
    public float deadzone = 0.5f;
    private float LxDirection;
    private float RxDirection;
    private float LzDirection;
    private float RzDirection;

    public Text playerStats;

    // Use this for initialization
    private void Start()
    {
        inventory = new Stack<GameObject>();
        combo = new Combo();
        anim = GetComponent<Animator>();
        cam = (Camera)FindObjectOfType(typeof(Camera));
    }  

    void Update(){

        if (health <= 0)
        {
            health = 0;
            Die();

        }

        //Fire Button
        if (Input.GetButtonUp(playerNumber+"Fire")){
            Fire();
            anim.SetTrigger("shooting");
        }

        if (Input.GetButtonUp(playerNumber+"Evaluate"))
        {
            Evaluate();

        }
        playerStats.text = "" + transform.name + ": LifePoints: " + health + " ; AMMOS: " + inventory.Count;

        //Falling System
        if (transform.position.y < -3){

            Die();
        }




        //Movement System

        //anim.SetBool("isRunning", false);



        /*if (Mathf.Abs(Input.GetAxis("LVertical")) >= deadzone || Mathf.Abs(Input.GetAxis("LHorizontal")) >= deadzone)
        {

            LxDirection = Input.GetAxis("LHorizontal") * speed * Time.deltaTime;
            LzDirection = Input.GetAxis("LVertical") * speed * Time.deltaTime;
            
            
            

            anim.SetBool("isRunning", true);

        }*/

        var x = Input.GetAxis(playerNumber+"LHorizontal");
        var y = Input.GetAxis(playerNumber+"LVertical");


        Move(x,y);

        if (Mathf.Abs(Input.GetAxis(playerNumber+"RHorizontal")) >= deadzone || Mathf.Abs(Input.GetAxis(playerNumber+"RVertical")) >= deadzone)
        {
            RxDirection = Input.GetAxis(playerNumber+"RHorizontal") * rotationSpeed;
            RzDirection = Input.GetAxis(playerNumber+"RVertical") * rotationSpeed;

        }

        Vector3 targetRotation = new Vector3(RxDirection, 0, RzDirection);
        transform.rotation = Quaternion.LookRotation(targetRotation);
    

    }

    void Die(){

        RemoveFromCamera();
        Destroy(this.gameObject);

    }

    void RemoveFromCamera()
    {

        cam.GetComponent<CameraController>().targets.Remove(this.transform);

    }

    void Move(float x, float y)
    {


        /*transform.position += (Vector3.forward * speed) * y * Time.deltaTime;
        transform.position += (Vector3.right * speed) * x * Time.deltaTime;*/
        Vector3 movementDirection = new Vector3(x * speed * Time.deltaTime, 0, y * speed * Time.deltaTime);
        transform.Translate(movementDirection, Space.World);

        Vector2 direction = new Vector2(x,y);
       
        direction = Rotate(direction, transform.rotation.eulerAngles.y);
        //Direction =Quaternion.AngleAxis(-45, Vector3.up) * Direction;
        //print("Direction is: " + direction);

        anim.SetFloat("VelX", direction.x);
        anim.SetFloat("VelY", direction.y);
    }

    
    //  Direction = Quaternion.Euler(0, transform.rotation.eulerAngles.y , 0) * Direction;



    public Vector2 Rotate(Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, (degrees + -transform.rotation.eulerAngles.y)) * v;

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

            //Debug.Log("Evaluate");
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
            Destroy(bullet, 3.0f);

        }

    }


    public void TakeDamage(int damage)
    {

        health -= damage;

    }

}

