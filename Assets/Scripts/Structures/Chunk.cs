using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private List<GameObject> Grounds;
    public float Length = 0;

    private void Start()
    {
        for (int i = 0; i < Grounds.Count; i++)
        {
            Length += Grounds[i].transform.localScale.z;
        }
    }
}
