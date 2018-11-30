using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingBullet : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject != this.GetComponent<Bullet>().owner)
        {
            StartCoroutine(SlowDown(3f, col));

        }
    }

    IEnumerator SlowDown(float delayTime, Collision col)
    {
        
        Debug.Log("SlowedDown");
        col.gameObject.GetComponent<Player>().speed -= 5f ;
        col.gameObject.GetComponent<Player>().rotationSpeed -= 50f;
        print(Time.time);
        yield return new WaitForSeconds(3f);
        print(Time.time);
        col.gameObject.GetComponent<Player>().speed += 5f;
        col.gameObject.GetComponent<Player>().rotationSpeed += 50f;
        
    }
}
