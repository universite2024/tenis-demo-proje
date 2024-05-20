using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class top_01 : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 initialPos;
    public string hitter;
    int playerScore;
    int botScore;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text botScoreText;

    public bool playing = true;

    private void Start()
    {

        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("duvar"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //transform.position = initialPos;

            GameObject.Find("player_obje_01").GetComponent<player_01>().Reset();

            if (playing)
            {
                if (hitter == "player_obje_01")
                {
                    playerScore++;
                }
                else if (hitter == "player_obje_bot")
                {
                    botScore++;
                }

                playing = false;
                updateScores();
            }
            
        }

        else if (collision.transform.CompareTag("saha_not"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //transform.position = initialPos;

            GameObject.Find("player_obje_01").GetComponent<player_01>().Reset();

            if (playing)
            {
                if (hitter == "player_obje_01")
                {
                    playerScore++;
                }
                else if (hitter == "player_obje_bot")
                {
                    botScore++;
                }

                playing = false;
                updateScores();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("saha_not") && playing)
        {
            if(hitter == "player_obje_01")
            {
                botScore++;
            }else if(hitter == "player_obje_bot")
            {
                playerScore++;
            }

            playing = false;
            updateScores();
        }
    }

    void updateScores()
    {
        playerScoreText.text = "Player : " + playerScore;
        botScoreText.text = "Bot : " + botScore;
    }
}
