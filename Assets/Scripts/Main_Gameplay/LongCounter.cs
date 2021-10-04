using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCounter : Counter
{

    public LongCounter(double theTimeStamp, double theEndTimeStamp, bool longNote, float theNoteLength)
    {
        timeStamp = theTimeStamp;
        endTimeStamp = theEndTimeStamp;
        isLongNote = longNote;
        noteLength = theNoteLength;
    }

}
