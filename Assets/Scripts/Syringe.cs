using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
