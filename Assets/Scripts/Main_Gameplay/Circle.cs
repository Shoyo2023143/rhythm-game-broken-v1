using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private float timer, timerScale, scaler, limit;

    private void Start()
    {
        SetTimerScale(1.2f, 0.08f);
    }

    private void SetTimerScale(float time, float lim)
    {
        timer = time;
        timerScale = time;
        limit = lim;
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        if (timer > limit)
        {
            timer -= Time.deltaTime;
            scaler = timer / timerScale;
        }
        else
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(scaler, scaler, scaler);
    }
}
