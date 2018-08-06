using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : Photon.MonoBehaviour
{

     private const float GroundPos = -4.715f;
     private const float ZPosition = -0.01708984f;

     public GameObject Goomba;
     public GameObject Turtle;
     public GameObject FlyingTurtle;

     private IList<Vector3> GoombaPositions = new List<Vector3>
    {
          new Vector3 { x = -1.617331f, y = -3.529118f, z = ZPosition },
          new Vector3 { x = -1.110802f, y = -3.532588f, z = ZPosition }, 
          new Vector3 { x = 4.107178f, y = -3.671422f, z = ZPosition }
    };

     private IList<Vector3> TurtlePositions = new List<Vector3>
     {
          new Vector3 { x = -3.870456f, y = -3.488186f, z = 0 },
          new Vector3 { x = 13.25133f, y = -4.675782f, z = 0 }
     };

     Vector3 flyingTurtlePosition = new Vector3 { x = 8.8f, y = -3.664514f, z = 0 };

     // Use this for initialization
     void Start()
     {

          Debug.Log("Level controller is online!");

          CreateGoombas(GoombaPositions);
          CreateTurtles(TurtlePositions);
          CreateFlyingTurtle(flyingTurtlePosition);
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

     private void CreateFlyingTurtle(Vector3 position)
     {
          if (!PhotonNetwork.connected)
          {
               Instantiate(FlyingTurtle, position, Quaternion.identity);
          }
          else if (PhotonNetwork.isMasterClient)
          {
               PhotonNetwork.Instantiate(FlyingTurtle.name, position, Quaternion.identity, 0);
          }
     }
}
