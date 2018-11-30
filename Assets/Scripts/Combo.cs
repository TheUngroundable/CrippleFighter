using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

	private string code;
    
    public Player player;
    private int steroidsCount = 0;
    public GameObject sleepingPill;
    
    void Start()
    {

        sleepingPill = Resources.Load("Prefabs/Bullets/SleepingBullet") as GameObject;

    }

    public void Special(Stack<GameObject> inventory)
    {
        foreach(GameObject bullet in inventory)
        {

            code = bullet.GetComponent<Bullet>().code + "" +code ;

        }

        Debug.Log("Code: "+code);
        switch (code)
        {

            case "000":
                //SUPERSIZE
                
                StartCoroutine(Supersize(4f));
                player.ClearInventory();
                break;

            case "001":
                //STEROIDI
                player.Heal(4);
                if (steroidsCount == 1)
                {

                    player.Die();

                } else
                {

                    steroidsCount++;

                }
                break;

            case "010":
                //SLEEPING PILLS
                //player.hasSpecial = true
                player.ClearInventory();
                player.AddBullet(sleepingPill);
                player.hasSpecial = true;
                break;

            case "011":
                //FAST FOOD
               
            case "100":
                //BALANCED BREAKFAST

            case "101":
                //DIARREA

            case "110":
                //PUKE FRAG

            case "111":
                //DON'T DO IT KIDS
                
               break;
        }
        
    code = "";

    }

    IEnumerator Supersize(float delayTime)
    {
        player.gameObject.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
        yield return new WaitForSeconds(delayTime);
        player.gameObject.transform.localScale -= new Vector3(1.5f, 1.5f, 1.5f);
        
    }

}
