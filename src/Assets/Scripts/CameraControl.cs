using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public bool lockY = true;
	private Vector3 offset;
	private Vector3 tempVect;
    private Transform cameraTransform;

    public bool FollowOnStart = false;

    private bool isFollowing = false;

	void Start ()
	{
        if(FollowOnStart)
        {
            OnStartFollowing();
        }
	}

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        offset = cameraTransform.position - transform.position; // initial camera offest

        Apply();
    }

    void LateUpdate ()
	{
        if(cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }

        if(isFollowing)
        {
            Apply();
        }
	}

    void Apply()
    {
        tempVect = transform.position + offset;
        if (lockY)
            tempVect.y -= transform.position.y + 4.7f; // remove y component of player position
        cameraTransform.position = tempVect;
    }

}
