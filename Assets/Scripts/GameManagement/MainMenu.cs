using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("Amoguses"))
        {
            PlayerPrefs.SetInt("Amoguses", 0);
        }

        if (!PlayerPrefs.HasKey("AmogusRed"))
        {
            PlayerPrefs.SetInt("AmogusRed", 1);
        }

        if (!PlayerPrefs.HasKey("AmogusSelected"))
        {
            PlayerPrefs.SetString("AmogusSelected", "AmogusRed");
        }
    }

    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Skins()
    {
        SceneManager.LoadScene(2);
    }
}
