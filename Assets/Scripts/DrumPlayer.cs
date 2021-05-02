using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrumPlayer : MonoBehaviour
{
    public bool isEnabled;
    private GameObject currentDrum;
    public float speed;
    private int gameMode; // either 8 or 4

    public GameObject gameMenu;

    public int[] currentBeat;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        isEnabled = false;
        if (SceneManager.GetActiveScene().name == "GameRegular")
        {
            gameMode = 4;
        } else
        {
            gameMode = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (((int)((transform.position.x - 0.5f) / gameMode)) % 2 == 0)
        {
            if (isEnabled)
            {
                if (currentBeat.Length > 0)
                {
                    foreach (int num in currentBeat)
                    {
                        if (num != 0)
                        {
                            gameMenu.GetComponent<GameMenu>().gameOver = true;
                        }
                    }
                }
            }
            isEnabled = false;
        } else
        {
            isEnabled = true;
        }

        if (isEnabled)
        {
            int drum = (int)(transform.position.x - 0.5f) % gameMode;
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetMouseButtonDown(0) && currentDrum != null)
            {
                playDrum();
                currentBeat[drum]--;
            }   
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpecialBeatTrigger"))
        {
            currentBeat = collision.gameObject.GetComponent<SpecialBeatTrigger>().pattern;
        } else
        {
            currentDrum = collision.gameObject;
        }
       
    }

    public void playDrum()
    {
        Color c = currentDrum.GetComponent<SpriteRenderer>().color;
        currentDrum.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 0.6f);
        currentDrum.GetComponent<AudioSource>().Play();
    }
}
