using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public List<GameObject> inRange;
    public Transform target;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public bool canShoot = true;
    public GameObject turretHead;
    public float delayTime = 3f;
    public bool enabled = true;


    private void Start()
    {
        inRange = new List<GameObject>();
    }

    private void Update()
    {
        if (enabled)
        {

            Check();
            //Begin Countdown
            //target = Get inRange which is not owner
            target = GetClosestEnemy();
            if (target != null)
            {

                turretHead.transform.LookAt(target);
                if (canShoot)
                {

                    Shoot();
                }
            }

        }
            
    }

    void Shoot()
    {
        var bullet = (GameObject)Instantiate(
        bulletPrefab,
        bulletSpawn.position,
        bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().speed;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 4.0f);

        StartCoroutine(waitToFireAgain());
    }

    IEnumerator waitToFireAgain()
    {

        canShoot = false;
        yield return new WaitForSeconds(Random.Range(1, delayTime));

        canShoot = true;


    }

    void Check()
    {

        for (int i = 0; i < inRange.Count; i++)
        {

            if (inRange[i] == null)
            {

                inRange.RemoveAt(i);

            }

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
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

   

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player") {
            inRange.Add(other.gameObject);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange.Remove(other.gameObject);

        }
            
    }
}
