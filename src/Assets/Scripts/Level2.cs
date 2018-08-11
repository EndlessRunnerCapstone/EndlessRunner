using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Photon.MonoBehaviour
{

     private const float GroundPos = -4.715f;
     private const float ZPosition = -0.01708984f;

     public GameObject Goomba;
     public GameObject Turtle;
     public GameObject RedTurtle;

     private IList<Vector3> GoombaPositions = new List<Vector3>
    {
        new Vector3 { x = 50.149f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 50.35997f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 53.05033f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 59.0047f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 59.35165f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 60.87473f, y = -3.199269f, z = ZPosition}, 
        new Vector3 { x = 61.45645f, y = -3.993397f, z = ZPosition },
        new Vector3 { x = 61.753f, y = -3.993397f, z = ZPosition },
        new Vector3 { x = 65.014f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 65.2224f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 65.43056f, y = GroundPos, z = ZPosition },
        new Vector3 { x = 71.01164f, y = -4.22789f, z = ZPosition },
        new Vector3 { x = 71.25837f, y = -4.069833f, z = ZPosition }
    };

     private IList<Vector3> TurtlePositions = new List<Vector3>
     {
          new Vector3 { x = 56.0648f, y = GroundPos, z = 0 },
          new Vector3 { x = 56.28454f, y = GroundPos, z = 0 },
          new Vector3 { x = 58.665f, y = GroundPos, z = 0 }
     };

     Vector3 redTurtlePosition = new Vector3 { x = 73.63828f, y = -4.681434f, z = 0 };

     Vector3 turtlePosition = new Vector3(10.60481f, -4.683582f, 0);

     // Use this for initialization
     void Start()
     {

          Debug.Log("Level controller is online!");

          CreateGoombas(GoombaPositions);
          CreateTurtles(TurtlePositions);
          CreateRedTurtle(redTurtlePosition);
     }

     // Update is called once per frame
     void Update()
     {

     }

     private void CreateGoombas(IList<Vector3> positions)
     {
          foreach (var position in positions)
          {
               if (!PhotonNetwork.connected)
               {
                    Instantiate(Goomba, position, Quaternion.identity);
               }
               else if (PhotonNetwork.isMasterClient)
               {
                    PhotonNetwork.Instantiate(Goomba.name, position, Quaternion.identity, 0);
               }
          }
     }

     private void CreateTurtles(IList<Vector3> positions)
     {
          foreach (var position in positions)
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

     private void CreateRedTurtle(Vector3 position)
     {
          if (!PhotonNetwork.connected)
          {
               Instantiate(RedTurtle, position, Quaternion.identity);
          }
          else if (PhotonNetwork.isMasterClient)
          {
               PhotonNetwork.Instantiate(RedTurtle.name, position, Quaternion.identity, 0);
          }
     }
}
