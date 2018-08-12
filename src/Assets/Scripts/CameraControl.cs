using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public bool lockY = false;
	private Vector3 offset;
	private Vector3 tempVect;
    private Transform cameraTransform;
    public bool FollowOnStart = false;
    private bool isFollowing = false;
    public static bool lockX = false;

	void Start ()
	{
        if(FollowOnStart)
        {
            OnStartFollowing();
        }
	}

    public void setYLock()
    {
        if(lockY)
        {
            lockY = false;
            Debug.Log("lock off");
        }
        else
        {
            lockY = true;
            Debug.Log("lock on");
        }
    }

    public void setXLock()
    {
        if (lockX)
        {
            lockX = false;
            Debug.Log("lock off");
        }
        else
        {
            lockX = true;
            Debug.Log("lock on");
        }
    }

    public void OnStartFollowing()
    {
       cameraTransform = Camera.main.transform;
       isFollowing = true;
       offset = cameraTransform.position - transform.position; // initial camera offset                                       
       Apply();
    }

    void LateUpdate ()
	{
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }
         
        if (isFollowing)
        {
            Apply();
        }
	}

    void Apply()
    {
        tempVect = transform.position + offset;

        if(!lockX)
        {
            tempVect.y -= transform.position.y + 4.7f; // remove y component of player position
            cameraTransform.position = tempVect;
        }
    
    }

}
