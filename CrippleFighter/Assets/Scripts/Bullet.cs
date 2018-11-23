using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject owner;
    public int damage = 1;
    public float speed = 10;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.tag == "Player" && col.gameObject!=owner)
        {

            col.gameObject.GetComponent<Player>().health-=damage;

        }
    }

}
