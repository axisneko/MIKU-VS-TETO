using UnityEngine;
using System.Collections.Generic;

public class LagFixer : MonoBehaviour
{
    int maxSleeves = 50;
    //int maxDecals = 50;
    List<GameObject> sleeves = new List<GameObject>();

    public void AddSleeve(GameObject sleeve) 
    {
        if (sleeves.Count >= maxSleeves)
        {
            sleeves.RemoveAt(maxSleeves - 1);
        }
        sleeves.Insert(0, sleeve);
        Debug.Log(sleeves.Count);
    }
}
