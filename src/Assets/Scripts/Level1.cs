using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Photon.MonoBehaviour {

    private const float GroundPos = -4.723f;

    public GameObject Goomba;
     public GameObject Turtle;

    private IList<Vector3> GoombaPositions = new List<Vector3>
    {
        new Vector3 { x = -5.58f, y = -3.92f, z = 0 },
        new Vector3 { x = -1.023f, y = GroundPos, z = 0 },
        new Vector3 { x = .843f, y = GroundPos, z = 0 },
        new Vector3 { x = 5.786997f, y = -3.278714f, z = -.01708984f },
        new Vector3 { x = 22.691f, y = -4.705f, z = 0 },
        new Vector3 { x = 17.797f, y = -4.697f, z = 0 },
        new Vector3 { x = 23.475f, y = -4.697f, z = 0 },
    };

     Vector3 turtlePosition = new Vector3(10.60481f, -4.683582f, 0);

	// Use this for initialization
	void Start () {

        Debug.Log("Level controller is online!");

        CreateGoombas(GoombaPositions);
          CreateTurtle(turtlePosition);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateGoombas(IList<Vector3> positions)
    {
        foreach (var position in positions)
        {
            if (!PhotonNetwork.connected)
            {
                Instantiate(Goomba, position, Quaternion.identity);
            }
            else if(PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Instantiate(Goomba.name, position, Quaternion.identity, 0);
            }
        }
    }

     private void CreateTurtle(Vector3 position)
     {
          if (!PhotonNetwork.connected)
          {
               Instantiate(Turtle, position, Quaternion.identity);
          }
          else if (PhotonNetwork.isMasterClient)
          {
               PhotonNetwork.Instantiate(Turtle.name, position, Quaternion.identity, 0);
          }
     }
}
