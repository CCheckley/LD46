using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    [SerializeField] Searchable[] searchableObjects;

    public void Randomize()
    {
        foreach(Searchable searchableObject in searchableObjects)
        {
            //TODO - RANDOMLY ASSIGN HOUSEHOLDITEM SO's HERE
        }
    }
}
