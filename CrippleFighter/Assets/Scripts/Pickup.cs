using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public GameObject bullet;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            //Add this bullet to player inventory
            col.gameObject.GetComponent<Player>().AddBullet(bullet);
        
        }
    }
}
