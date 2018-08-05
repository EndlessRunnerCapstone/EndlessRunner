using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickNoDestroyNoCoins : MonoBehaviour {

    Vector3 originalPos;

    // Use this for initialization
    void Start () {
        originalPos = transform.position;
    }

    IEnumerator OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {  
            transform.position += Vector3.up * Time.deltaTime; //possibly change this to make it more dramatic
            yield return new WaitForSeconds(0.1f);
            transform.position = originalPos;      
        }

    }
}
