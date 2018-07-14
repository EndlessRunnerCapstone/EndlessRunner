using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject player;
	public bool lockY = true;
	private Vector3 offset;
	private Vector3 tempVect;

	void Start ()
	{
		offset = transform.position - player.transform.position; // initial camera offest

	}

	void LateUpdate ()
	{
		tempVect = player.transform.position + offset;
		if(lockY) 
			tempVect.y -= player.transform.position.y + 4.7f; // remove y component of player position
		transform.position = tempVect;
	}

}
