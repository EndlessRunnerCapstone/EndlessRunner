using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAxe : MonoBehaviour {

     [SerializeField] private GameObject[] bridgeBlocks = new GameObject[17];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.gameObject.tag == "Player")
          {
               StartCoroutine(DestroyBridge());
          }
     }

     private IEnumerator DestroyBridge()
     {
          for (int i = 0; i < bridgeBlocks.Length; i++)
          {
               Destroy(bridgeBlocks[i]);
               yield return new WaitForSeconds(0.08f);
          }
     }
}
