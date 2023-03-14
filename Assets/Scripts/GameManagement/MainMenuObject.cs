using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuObject : MonoBehaviour
{
    [SerializeField] private bool IsPlayer;

    void Start()
    {
        if (IsPlayer)
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
    }

    void Update()
    {
        if (!IsPlayer)
        {
            transform.Rotate(new Vector3(0, 0, 0.01f));
        }
    }
}
