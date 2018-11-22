using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public int health = 3;

    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update(){

        if (Input.GetButtonUp("Fire1")){

            Fire();
        
        }

        if (health<=0){

            Die();

        }

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
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        bullet.GetComponent<Bullet>().owner = this.gameObject;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 10.0f);

    }
}
