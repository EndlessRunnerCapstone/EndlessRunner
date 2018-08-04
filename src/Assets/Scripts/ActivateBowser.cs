using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBowser : MonoBehaviour {
     [SerializeField] GameObject bowser;

	// Use this for initialization
	void Start () {
          
	}
	
	// Update is called once per frame

     private void OnTriggerEnter2D(Collider2D collision)
     {
          bowser.GetComponent<BowserController>().enabled = true;
     }
}
