using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour {

    public List<GameObject> players;
    public List<GameObject> spawnersPrefab;
    public List<GameObject> spawners;
    public GameObject camera;
    private SphereCollider collider;
    public int spawnersCooldown = 4;
    public GameObject victory;
    public GameObject[] turrets;

	// Use this for initialization
	void Start () {
	
        Camera();
        spawners = new List<GameObject>();
        collider = GetComponent<SphereCollider>();

	}
	

	// Update is called once per frame
	void Update () {

        Check();
        if (spawners.Count < 5)
        {
            StartCoroutine(CheckSpawners());

        }

        if (players.Count == 1)
        {

            Win();

        }
       
    }
    void Check()
    {

        for (int i = 0; i < players.Count; i++)
        {

            if (players[i] == null)
            {

                players.RemoveAt(i);

            }

        }

    }
    void Win() {

        foreach(GameObject turret in turrets)
        {

            turret.GetComponent<Turret>().enabled = false;

        }

        victory.GetComponent<Text>().text = "Winner: "+players[0].transform.name;
        victory.gameObject.SetActive(true);
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
