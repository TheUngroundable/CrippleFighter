using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

	private string code;
    
    public GameObject[] proiettili = new GameObject[3];

   
    public GameObject Bullet(Stack<GameObject> inventory)
    {
        foreach(GameObject bullet in inventory)
        {
            /*3214 = 3214*/
            code += bullet.GetComponent<Bullet>().code;

        }

        proiettili[0] = Resources.Load("Prefabs/Bullets/PurpleBullet") as GameObject;
        proiettili[1] = Resources.Load("Prefabs/Bullets/YellowBullet") as GameObject;

        switch (code)
        {

            case "000":

            case "001":

            case "010":
            
            case "011":
                return proiettili[0];
            case "100":

            case "101":

            case "110":

            case "111":
                return proiettili[1];
               
        }
        
        return proiettili[0];

    }

}
