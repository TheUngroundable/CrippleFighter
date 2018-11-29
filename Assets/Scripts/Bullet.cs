using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject owner;
    public int damage = 1;
    public float speed = 100;
    public int code = 0;
    public int knockback = 10;
    public int despawnTime = 1;

    void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.tag == "Player" && col.gameObject!=owner)
        {

            col.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*knockback, ForceMode.Impulse);
            col.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(GetComponent<Bullet>());
            Destroy(this.gameObject, despawnTime);

        }
    }

}
