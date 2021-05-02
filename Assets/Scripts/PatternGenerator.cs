using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator : MonoBehaviour
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
        beatBank[0] = 1232123211111111;
        beatBank[1] = 1232123222222222;
        beatBank[2] = 2313231321112111;
        beatBank[3] = 1232213211111111;
        beatBank[4] = 1332113210101110;
        beatBank[5] = 3131214212102110;
        beatBank[6] = 1232323213111111;
        beatBank[7] = 1232213211113111;
        beatBank[8] = 1232123211212221;
        beatBank[9] = 2313231321102110;

        // ph
        beatBank[10] = 1231214212112110;


        // medium beats

        // cup song
        beatBank[11] = 3131313123212221;

        beatBank[12] = 2323232321212221;
        beatBank[13] = 3213213222222210;

        // hard
        beatBank[14] = 1222222212000200;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.x > lastPos - 16)
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

        for (long k = 1000000000000000; k >= 100000000; k = k / 10)
        {
            Instantiate(drumsDark[(int)(pattern / k) % 10], new Vector3(lastPos + (17 - Mathf.Floor(Mathf.Log10(k) + 1)), 0, 0), Quaternion.identity);
        }

        int[] newPattern = new int[8];
        for (int k = 10000000; k >= 1; k = k / 10)
        {
            newPattern[(int)(8 - Mathf.Floor(Mathf.Log10(k) + 1))] = (int)(pattern % 100000000) / k % 10;

        }
        specialBeatTrigger.GetComponent<SpecialBeatTrigger>().pattern = newPattern;
        for (int k = 0; k < 8; k++)
        {
            float toAdd = 1f / newPattern[k];
            for (int j = 0; j < newPattern[k]; j++)
            {
                Instantiate(specialBeatTrigger, new Vector3(lastPos + k + 0.7f + toAdd * j, 0, 0), Quaternion.identity);
            }
        }

        for (long k = 1000000000000000; k >= 100000000; k = k / 10)
        {
            Instantiate(drums[(int)(pattern / k) % 10], new Vector3(lastPos + (17 - Mathf.Floor(Mathf.Log10(k) + 1)) + 8, 0, 0), Quaternion.identity);
        }

        lastPos += 16;

    }
}
