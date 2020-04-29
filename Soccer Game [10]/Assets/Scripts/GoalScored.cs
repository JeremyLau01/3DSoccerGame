using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for text and other UI components
using UnityEngine.Audio; //for sound effects

namespace Com.JeremyLau.AvatarLeague
{
    public class GoalScored : MonoBehaviour
    {



        #region Variables 

        public GameObject soccerBall;

        public Text scoredTextBlue;
        public Text scoredTextRed;
        public Text scoreboardBlue;
        public Text scoreboardRed;
        public Text winTextBlue;
        public Text winTextRed;


        private AudioSource audioSource;
        public AudioClip goalSound;
        public AudioClip winSound;
        public AudioClip backgroundMusic;

        private int redScore = 0;
        private int blueScore = 0;
        // ToString()

        #endregion


        #region Monobehaviour Callbacks

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            scoredTextBlue.enabled = false;
            scoredTextRed.enabled = false;

            winTextBlue.enabled = false;
            winTextRed.enabled = false;

            scoreboardBlue.text = blueScore.ToString();
            scoreboardRed.text = redScore.ToString();

            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }

        //controls, states, and jumping in update; controls and states in FixedUpdate
        private void Update()
        {
            if (redScore == 5)
            {
                winTextRed.enabled = true;
                ClearScores();
                StartCoroutine("WinRestart");
            }
            if (blueScore == 5)
            {
                winTextBlue.enabled = true;
                ClearScores();
                StartCoroutine("WinRestart");
            }
        }

        public void ClearScores()
        {
            audioSource.clip = winSound;
            audioSource.Play();
            redScore = 0;
            scoreboardRed.text = redScore.ToString();
            blueScore = 0;
            scoreboardBlue.text = blueScore.ToString();
        }


        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "TempObjTag")
            {
                audioSource.clip = goalSound;
                audioSource.Play();
                BallBackToMiddle();
                if (this.gameObject.tag == "Blue")
                {
                    redScore += 1;
                    scoreboardRed.text = redScore.ToString();
                    scoredTextRed.enabled = true;
                }
                else
                {
                    blueScore += 1;
                    scoreboardBlue.text = blueScore.ToString();
                    scoredTextBlue.enabled = true;
                }
                StartCoroutine("WaitBeforeDestroyScoredText");
            }
        }

        
        public void BallBackToMiddle()
        {
            //Move the ball back to the middle
            soccerBall.transform.position = new Vector3(0, 100, 0);

            //Making the ball stop moving
            soccerBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            soccerBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            soccerBall.GetComponent<Rigidbody>().Sleep();
        }


        void FixedUpdate()
            {

            }

        IEnumerator WaitBeforeDestroyScoredText()
        {
            yield return new WaitForSeconds(3);
            scoredTextBlue.enabled = false;
            scoredTextRed.enabled = false;
        }

        IEnumerator WinRestart()
        {
            yield return new WaitForSeconds(6);
            winTextBlue.enabled = false;
            winTextRed.enabled = false;
        }

        #endregion
    }
}