using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] Skins Skins;
    [SerializeField] TextMesh TextCost;

    public string Name;
    public int Cost;

    void Start()
    {
        TextCost.text = Cost.ToString();
        if (PlayerPrefs.GetString("AmogusSelected") == "Amogus" + Name)
        {
            Skins.Skin = gameObject;
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0));
    }

    public void Highlight()
    {
        
    }
}
