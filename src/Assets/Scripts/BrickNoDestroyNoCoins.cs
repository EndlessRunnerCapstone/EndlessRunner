using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for bricks that only bounce - no coins, no destroy, no power ups
/// </summary>
public class BrickNoDestroyNoCoins : MonoBehaviour {

    Vector3 originalPos;

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
    }

    // bounce position up and down
    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {  
            transform.position += Vector3.up * Time.deltaTime; 
            yield return new WaitForSeconds(0.1f);
            transform.position = originalPos;      
        }

    }
}
