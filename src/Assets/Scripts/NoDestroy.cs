using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroy : MonoBehaviour {

	public float jump = 5f; 
 	private int hitCount = 0; //can be used to limit hits
 	Vector3 originalPos;

 // Use this for initialization
 void Start () {
		originalPos = transform.position;

 }



 IEnumerator OnCollisionEnter2D (Collision2D coll)
	{
		Debug.Log ("Collision in edge");
		if ((coll.gameObject.tag == "Player") && (hitCount == 0)) {
			Debug.Log ("Player in edge!");
			//transform.Translate(0, jump * Time.deltaTime, 0);
			transform.position += Vector3.up * Time.deltaTime; //possibly change this to make it more dramatic
			yield return new WaitForSeconds (0.1f);
			transform.position = originalPos;
			//hitCount++;
		}

//		if(coll is EdgeCollider2D)
//			Debug.Log ("Edge Collider");
		
	}
}
