using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumPlayerEasy : MonoBehaviour
{
    public bool isEnabled;
    private GameObject currentDrum;
    public float speed;

    public GameObject gameMenu;

    public int[] currentBeat;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        isEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (((int)((transform.position.x - 0.5f) / 4)) % 2 == 0)
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
            int drum = (int)(transform.position.x - 0.5f) % 4;
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
