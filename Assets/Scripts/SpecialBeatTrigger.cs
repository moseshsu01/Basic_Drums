using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBeatTrigger : MonoBehaviour
{
    public int[] pattern;
    private Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.Find("DrumPlayer").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<DrumPlayer>().playDrum();
        }
    }

    private void Update()
    {
        if (playerPos.position.x - transform.position.x > 20)
        {
            Destroy(gameObject);
        }
    }
}
