using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VacuumShaders
{
    namespace CurvedWorld
    {
        [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Move Element")]
        public class PathMover : MonoBehaviour
        {
            //////////////////////////////////////////////////////////////////////////////
            //                                                                          // 
            //Unity Functions                                                           //                
            //                                                                          //               
            //////////////////////////////////////////////////////////////////////////////

            void Update()
            {
                //transform.Translate(Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime);
            }

            void FixedUpdate()
            {
                transform.position += Vector3.forward * Time.deltaTime * -20;

                if (transform.position.z < -100)
                {
                    //Runner_SceneManager.get.DestroyChunk(this);
                }
            }
        }
    }
}
