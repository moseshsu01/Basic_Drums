using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBackAlpha : MonoBehaviour
{

    bool started = false;
    float countdown = 0.15f;
    float alpha;
    private Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.Find("DrumPlayer").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha = GetComponent<SpriteRenderer>().color.a;
        if (!started && alpha < 1)
        {
            started = true;
        }

        if (started)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0)
        {
            started = false;
            countdown = 0.15f;
            Color c = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1);
        }

        if (playerPos.position.x - transform.position.x > 20)
        {
            Destroy(gameObject);
        }
    }
}
