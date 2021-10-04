using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager
{
    public static float ToSecWithFixedTempo(float beat, float tempo)
    {
        return beat / (tempo / 60f);
    }    

    public static float ToBeatwithFixedTempo(float sec, float tempo)
    {
        return sec * (tempo / 60f);
    }
}
