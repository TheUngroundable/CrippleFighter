using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject owner;
    public int damage = 1;
    public float speed = 100;
    public int code = 0;

    void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.tag == "Player" && col.gameObject!=owner)
        {

            col.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject, 0.5f);

        }
    }

}
