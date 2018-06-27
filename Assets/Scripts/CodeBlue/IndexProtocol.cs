using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** IndexProtocol
 *  A protocol containing the line numbers containing 
 *  lines of dialogue from PatientScript.txt; primarily 
 *  created to avoid cluttering the class that records 
 *  the script.
 **/

public class IndexProtocol : MonoBehaviour
{
    // Indexes of beginning and end of each grouping of dialogue
    public const int StartIntro = 1;
    public const int EndIntro = 3;

    // If nurse introduces themselves multiple times
    public const int MultiIntro = 4;

    // Check ID band
    public const int CheckID = 6;

    // If nurse checks ID band multiple times
    public const int MultiID = 8;

    // If nurse checks ID band without introduction
    public const int IDNoIntro = 10;

    // Assess pain
    public const int AssessPain = 12;

    // If nurse assesses pain multiple times
    public const int MultiAssess = 14;

    // If nurse triggers code blue too early
    public const int EarlyBlue = 16;
}
