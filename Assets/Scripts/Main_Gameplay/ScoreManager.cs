using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public AudioSource music;
    public TMPro.TextMeshPro scoreText;
    public TMPro.TextMeshPro comboText;

    public static float totalNotes;
    public static float normalHits;
    public static float goodHits;
    public static float perfectHits;
    public static float missHits;
    public static float totalHits;

    static int comboScore;
    public static int score;
    public static int highestComboScore;
    void Start()
    {
        Instance = this;
        score = 0;
        comboScore = 0;
        highestComboScore = 0;

    }
    public static void Hit()
    {
        DoHit(25);
        normalHits++;
    }

    public static void GoodHit()
    {
        DoHit(50);
        goodHits++;
    }

    public static void PerfectHit()
    {
        DoHit(100);
        perfectHits++;
    }

    public static void Miss()
    {
        if (comboScore > highestComboScore)
        {
            highestComboScore = comboScore;
        }
        comboScore = 0;
        missHits++;
        totalNotes++;
        Instance.missSFX.Play();
    }
    private void Update()
    {
        comboText.text = "Combo: " + comboScore.ToString();
        scoreText.text = "Score: " + score.ToString();

        if (!music.isPlaying)
        {
            if (comboScore > highestComboScore)
            {
                highestComboScore = comboScore;
            }
        }
    }

    public static void DoHit(int addScore)
    {
        comboScore += 1;
        score += addScore;
        totalHits++;
        totalNotes++;
        Instance.hitSFX.Play();
    }
}
