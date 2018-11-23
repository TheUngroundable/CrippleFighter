using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public GameObject bullet;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            Player player = col.gameObject.GetComponent<Player>();
            //Add this bullet to player inventory

            if (player.inventory.Count < 3 && !player.hasSpecial)
            {

                player.AddBullet(bullet);
                Destroy(this.gameObject);

            }
        }
    }
}
