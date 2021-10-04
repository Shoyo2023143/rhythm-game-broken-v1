using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsDisplay : MonoBehaviour
{

    public float percentHits;
    public Text percentHit, hitCount, goodCount, perfectCount, missCount, finalScore, rank, highestComboScore;

    // Start is called before the first frame update
    void Start()
    {  

    }

    // Update is called once per frame
    void Update()
    {
        hitCount.text = ScoreManager.normalHits.ToString();
        goodCount.text = ScoreManager.goodHits.ToString();
        perfectCount.text = ScoreManager.perfectHits.ToString();
        missCount.text = ScoreManager.missHits.ToString();
        finalScore.text = ScoreManager.score.ToString();
        highestComboScore.text = ScoreManager.highestComboScore.ToString();

        percentHits = (ScoreManager.totalHits / ScoreManager.totalNotes) * 100f;
        percentHit.text = ((ScoreManager.totalHits / ScoreManager.totalNotes) * 100f).ToString("F1") + "%";

        rank.text = "F";
        if (percentHits > 60f)
        { 
            rank.text = "D";
            if (percentHits > 70f)
            {
                rank.text = "C";
                if (percentHits > 80f)
                {
                    rank.text = "B";
                    if (percentHits > 85f)
                    {
                        rank.text = "B+";
                        if (percentHits > 90f)
                        {
                            rank.text = "A";
                            if (percentHits > 95f)
                            {
                                rank.text = "A+";
                                if (percentHits > 97f)
                                {
                                    rank.text = "S";
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
