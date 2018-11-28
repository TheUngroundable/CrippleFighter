using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public List<GameObject> players;
    public List<GameObject> spawnersPrefab;
    public List<GameObject> spawners;
    public GameObject camera;
    private SphereCollider collider;
    public int spawnersCooldown = 4;
	// Use this for initialization
	void Start () {
	
        Camera();
        spawners = new List<GameObject>();
        collider = GetComponent<SphereCollider>();

	}
	

	// Update is called once per frame
	void Update () {
        if (spawners.Count < 5)
        {
            StartCoroutine(CheckSpawners());
        }
    }

    IEnumerator CheckSpawners()
    {

        Debug.Log(collider.radius);
        //transform.position = Random.insideUnitSphere * 5;

        Vector2 position = Random.insideUnitCircle;

        GameObject spawner = Instantiate(spawnersPrefab[0], new Vector3(position.x, 0, position.y) * collider.radius, Quaternion.identity);
        spawners.Add(spawner);
        spawner.GetComponent<Pickup>().manager = this.gameObject;


        yield return new WaitForSeconds(spawnersCooldown);

    }

    void Camera()
    {
        foreach (GameObject player in players)
        {

            camera.GetComponent<CameraController>().targets.Add(player.transform);

        }
    }

}
