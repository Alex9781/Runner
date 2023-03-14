using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ChunksNightTheme;
    [SerializeField] private List<GameObject> ChunksMorningTheme;
    [SerializeField] private Slider Slider;
    [SerializeField] private Player Player;
    [SerializeField] private CinemachineVirtualCamera CameraMain;
    [SerializeField] private GameObject CanvasFinish;
    [SerializeField] private Material SkyboxMorningMaterial;
    [SerializeField] private Material SkyboxNightMaterial;
    [SerializeField] private GameObject CanvasMenu;
    [SerializeField] private AdManager AdManager;
    [SerializeField] private GameObject ButtonAd;

    public Material ItemMaterial;
    public Material ImpostorMaterial;

    private float MapLength = 0;

    private void Start()
    {
        Advertisement.Banner.Hide();
        int themeIndex = Random.Range(1, 3);
        List<GameObject> chunks;
        if (themeIndex == 1)
        {
            RenderSettings.skybox = SkyboxNightMaterial;
            chunks = ChunksNightTheme;
        }
        else
        {
            RenderSettings.skybox = SkyboxMorningMaterial;
            chunks = ChunksMorningTheme;
        }
        float lastChunkPositionZ = 0;
        int chunkCount = Random.Range(1, 6);
        for (int i = 0; i < chunkCount; i++)
        {
            GameObject spawnedChunk = Instantiate(chunks[Random.Range(0, chunks.Count - 1)]);
            spawnedChunk.transform.position = new Vector3(0, 0, lastChunkPositionZ);
            lastChunkPositionZ += 100;
        }
        MapLength = lastChunkPositionZ;
        GameObject endChunk = Instantiate(chunks[chunks.Count - 1]);
        endChunk.transform.position = new Vector3(0, 0, lastChunkPositionZ - 50);
    }

    private void Update()
    {
        Slider.value = (Player.transform.position.z + 42.5f) / MapLength;
    }

    public void Finish()
    {
        CameraMain.gameObject.SetActive(false);
        CanvasFinish.SetActive(true);
        Player.UpdateAmoguses();
        if (Random.Range(1, 101) <= 25)
        {
            AdManager.ShowAd();
        }
        ButtonAd.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleMenu()
    {
        CanvasMenu.SetActive(!CanvasMenu.activeInHierarchy);
    }

    public void Vent(GameObject cameraVent)
    {
        CameraMain.gameObject.SetActive(false);
        /*CameraMain.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 10;*/
        cameraVent.SetActive(true);
        StartCoroutine(nameof(UnVent), cameraVent);
    }

    IEnumerator UnVent(GameObject cameraVent)
    {
        yield return new WaitForSeconds(1);
        CameraMain.gameObject.SetActive(true);
        /*CameraMain.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0.3f;*/
        cameraVent.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [System.Obsolete]
    public void EarnReward()
    {
        AdManager.ShowRewardedAd();
        ButtonAd.SetActive(false);
    }

    public void DoubleAmoguses()
    {
        if (Player.Amoguses < 20)
        {
            Player.Amoguses += 20;
        }
        PlayerPrefs.SetInt("Amoguses", PlayerPrefs.GetInt("Amoguses") + Player.Amoguses);
        Player.Amoguses *= 2;
        Player.UpdateAmoguses();
    }
}
