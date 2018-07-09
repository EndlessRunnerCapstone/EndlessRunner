using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This needs to be added to player class once created **************************

// Controls bounce of question mark block
public class QuestionBlock : MonoBehaviour {

	public float bounceHeight = 0.5f;
	public float bounceSpeed = 4f;
	private bool canBounce = true; 
	private Vector2 originalPosition;

	// Use this for initialization
	void Start () {
		originalPosition = transform.localPosition;
	}

	public void QuestionBlockBounce ()
	{

		if (canBounce) {
			canBounce = false;
			StartCoroutine(Bounce());	
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Bounce ()
	{
		while (true) {

			transform.localPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);

			if (transform.localPosition.y >= originalPosition.y + bounceHeight) {
				break; // if height has changed -> break

				yield return null;
			}
		}

		while (true) {
			transform.localPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime);

			if (transform.localPosition.y <= originalPosition.y) {
				transform.localPosition = originalPosition; // put block back to original position
				break;
			}

			yield return null;
		}
	}
}
