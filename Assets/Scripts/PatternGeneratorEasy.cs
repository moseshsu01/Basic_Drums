using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGeneratorEasy : MonoBehaviour
{
    public GameObject kick;
    public GameObject hiHat;
    public GameObject snare;
    public GameObject crash;
    public GameObject kickDark;
    public GameObject hiHatDark;
    public GameObject snareDark;
    public GameObject crashDark;

    private int lastPattern = 1;

    private List<GameObject> drums = new List<GameObject>();
    private List<GameObject> drumsDark = new List<GameObject>();

    private long[] beatBank = new long[15];

    public GameObject specialBeatTrigger;

    public DrumPlayer dp;
    public Transform playerPos;

    public int lastPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        drums.Add(kick);
        drums.Add(kick);
        drums.Add(hiHat);
        drums.Add(snare);
        drums.Add(crash);
        drumsDark.Add(kickDark);
        drumsDark.Add(kickDark);
        drumsDark.Add(hiHatDark);
        drumsDark.Add(snareDark);
        drumsDark.Add(crashDark);

        // easy beats
        beatBank[0] = 12321111;
        beatBank[1] = 12322222;
        beatBank[2] = 13231111;
        beatBank[3] = 23232211;
        beatBank[4] = 13231201;
        beatBank[5] = 21312111;
        beatBank[6] = 32141211;
        beatBank[7] = 12132111;
        beatBank[8] = 12312210;
        beatBank[9] = 42310200;
        beatBank[10] = 23142110;
        beatBank[11] = 12431110;
        beatBank[12] = 42130111;
        beatBank[13] = 22222221;
        beatBank[14] = 32122221;


    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.x > lastPos - 8)
        {
            newPattern();
        }
    }

    private void newPattern()
    {
        int currentPattern = Random.Range(0, beatBank.Length - 1);
        while (currentPattern == lastPattern)
        {
            currentPattern = Random.Range(0, beatBank.Length - 1);
        }
        long pattern = beatBank[currentPattern];
        lastPattern = currentPattern;


        for (long k = 10000000; k >= 10000; k = k / 10)
        {
            Instantiate(drumsDark[(int)(pattern / k) % 10], new Vector3(lastPos + (9 - Mathf.Floor(Mathf.Log10(k) + 1)), 0, 0), Quaternion.identity);
        }

        int[] newPattern = new int[4];
        for (int k = 1000; k >= 1; k = k / 10)
        {
            newPattern[(int)(4 - Mathf.Floor(Mathf.Log10(k) + 1))] = (int)(pattern % 10000) / k % 10;

        }

        specialBeatTrigger.GetComponent<SpecialBeatTrigger>().pattern = newPattern;
        for (int k = 0; k < 4; k++)
        {
            float toAdd = 1f / newPattern[k];
            for (int j = 0; j < newPattern[k]; j++)
            {
                Instantiate(specialBeatTrigger, new Vector3(lastPos + k + 0.7f + toAdd * j, 0, 0), Quaternion.identity);
            }
        }

        for (long k = 10000000; k >= 10000; k = k / 10)
        {
            Instantiate(drums[(int)(pattern / k) % 10], new Vector3(lastPos + (9 - Mathf.Floor(Mathf.Log10(k) + 1)) + 4, 0, 0), Quaternion.identity);
        }

        lastPos += 8;

    }
}
