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

    public void setYLock()
    {
        if(lockY)
        {
            lockY = false;
            Debug.Log("lock off");
       //     tempVect = cameraTransform.position;
      //      tempVect.x -= cameraTransform.position.x + 4.7f;
     //       offset = tempVect.position;// + transform.position;
     //       Apply();
        }
        else
        {
            lockY = true;
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

    public void SetEnabled(bool trueOrFalse)
    {
        enabled = trueOrFalse;
    }

    void OnEnabled()
    {
     //   Debug.Log(name + " was enabled.");

    }

    void OnDisabled()
    {
     //   Debug.Log(name + " was disabled.");

    }

    void LateUpdate ()
	{
     //   Debug.Log("We are on frame " + Time.frameCount);

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

        if (lockY)
        {
            tempVect.y -= transform.position.y + 4.7f; // remove y component of player position
            cameraTransform.position = tempVect;
        }
        else
        {
            cameraTransform.position = tempVect;
        }
        
    }

}
