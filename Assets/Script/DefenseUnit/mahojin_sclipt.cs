using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class mahojin_sclipt : MonoBehaviour
{
    private bool canWarp = true; 
    public float warpCooldown = 10f; 

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("");
            if (canWarp == true)
            {
                Debug.Log("");


                GameObject startObject = GameObject.FindWithTag("start");

                if (startObject != null)
                {
                    
                    Vector3 startPosition = startObject.transform.position;


                 
                    Vector3 warpPosition = new Vector3(startPosition.x, startPosition.y + 20.0f, startPosition.z);

                    NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

               

                    other.transform.position = warpPosition; // 



                 
                    StartCoroutine(WarpCooldown());
                }
                else
                {
                    Debug.LogError("");
                }
            }
        }
    }


    private IEnumerator WarpCooldown()
    {
        canWarp = false; 
        yield return new WaitForSeconds(warpCooldown); 
        canWarp = true; 
    }
}