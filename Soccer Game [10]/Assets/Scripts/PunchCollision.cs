using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.JeremyLau.AvatarLeague
{
    public class PunchCollision : MonoBehaviour
    {



        #region Variables 

        public GameObject player;
        private GameObject soccerBall;


        #endregion


        #region Monobehaviour Callbacks

        private void Start()
        {
            soccerBall = GameObject.FindGameObjectWithTag("TempObjTag");
        }

        //controls, states, and jumping in update; controls and states in FixedUpdate
        private void Update()
        {

        }





        void OnTriggerStay(Collider other)
        {
            Vector3 aimDirection = this.transform.position - player.transform.position;
            //aimDirection = new Vector3(aimDirection[0], aimDirection[1]*-1, aimDirection[2]);             //COMMENT OR UNCOMMENT (to change viewing angle --> shot angle)

            bool punch = Input.GetKeyDown(KeyCode.Mouse0);
            if (punch)
            {
                if (other.gameObject.tag == "TempObjTag") // this string is your newly created tag
                {
                    Debug.Log("Hitting Ball");
                    Debug.Log(aimDirection);


                    soccerBall.GetComponent<Rigidbody>().AddForce(aimDirection * 1000);
                }
                if (other.gameObject.tag == "PlayerTag")
                {
                    other.GetComponent<Rigidbody>().AddForce(aimDirection * 1000);
                }
            }
        }







        void FixedUpdate()
        {

        }

        #endregion
    }
}