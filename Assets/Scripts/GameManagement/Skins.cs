using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{
    [SerializeField] private Text TextAmoguses;
    [SerializeField] private Camera Camera;
    [SerializeField] private float LimitTop;
    [SerializeField] private float LimitBottom;
    [SerializeField] private Text TextAmogus;
    
    public GameObject Skin;

    private float MousePositionY;
    private float PositionY;
    private bool IsHolding = false;
    private readonly int MovementDivider = 80;

    private void Start()
    {
        UpdateAmoguses();
        Advertisement.Banner.Hide();
        TextAmogus.text = Skin.GetComponent<Skin>().Name + " selected";
        Skin.GetComponent<Outline>().enabled = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePositionY = Input.mousePosition.y;
            PositionY = Camera.transform.position.y;
            IsHolding = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                Skin skin;
                if (hit.transform.TryGetComponent(out skin))
                {
                    Skin.GetComponent<Outline>().enabled = false;
                    Skin = skin.gameObject;
                    Skin.GetComponent<Outline>().enabled = true;
                    UpdateButton();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsHolding = false;
        }
        if (IsHolding == true)
        {
            Camera.transform.position = new Vector3(Camera.transform.position.x,
                PositionY - (Input.mousePosition.y - MousePositionY) / MovementDivider,
                Camera.transform.position.z);
        }
        Camera.transform.position = new Vector3(Camera.transform.position.x,
            Mathf.Clamp(Camera.transform.position.y, LimitBottom, LimitTop),
            Camera.transform.position.z);
    }

    private void UpdateAmoguses()
    {
        TextAmoguses.text = PlayerPrefs.GetInt("Amoguses").ToString() + " Little amoguses";
    }

    private void UpdateButton()
    {
        Skin skin = Skin.GetComponent<Skin>();
        string name = "Amogus" + skin.Name;
        TextAmogus.text = "Purchase " + skin.Name;
        if (PlayerPrefs.HasKey(name))
        {
            TextAmogus.text = skin.Name + " selected";
            PlayerPrefs.SetString("AmogusSelected", name);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Purchase()
    {
        Skin skin = Skin.GetComponent<Skin>();
        if (PlayerPrefs.HasKey("Amogus" + skin.Name))
        {
            return;
        }
        string name = "Amogus" + skin.Name;
        if (PlayerPrefs.GetInt("Amoguses") > skin.Cost)
        {
            PlayerPrefs.SetInt("Amoguses", PlayerPrefs.GetInt("Amoguses") - skin.Cost);
            PlayerPrefs.SetInt(name, 1);
        }
        UpdateAmoguses();
        UpdateButton();
    }
}
