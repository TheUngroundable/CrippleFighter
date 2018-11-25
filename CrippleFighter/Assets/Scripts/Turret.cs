using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public List<GameObject> inRange;
    public GameObject owner;
    public Transform target;
    public int deactivationTime = 6; 
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public bool hasShot = false;

    public Coroutine shooting;

    private void Start()
    {
        inRange = new List<GameObject>();
    }

    private void Update()
    {
        if(owner != null)
        {
            //Begin Countdown
            StartCoroutine(Deactivate(deactivationTime));
            //target = Get inRange which is not owner
            target = GetClosestEnemy();
            if(target != null)
            {

                transform.LookAt(target);
                
                Shoot();

            }
        }

    }

    void Shoot()
    {
        if (!hasShot)
        {

            var bullet = (GameObject)Instantiate(
               bulletPrefab,
               bulletSpawn.position,
               bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().speed;
            bullet.GetComponent<Bullet>().owner = this.gameObject;
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 4.0f);
            hasShot = true;

        }

    }

    Transform GetClosestEnemy()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in inRange)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist && t != owner)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    private IEnumerator Deactivate(int deactivationTime)
    {

        yield return new WaitForSeconds(deactivationTime);
        this.owner = null;
        this.target = null;
        hasShot = false;

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().turret = this.gameObject;
            inRange.Add(other.gameObject);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().turret = null;
            inRange.Remove(other.gameObject);

        }
            
    }
}
