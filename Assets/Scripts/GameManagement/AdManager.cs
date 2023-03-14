using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    [SerializeField] GameObject GameManager;
    
    public string GameID;
    public string PlacementVideoID;
    public string PlacementRewardedVideoID;
    private bool TestMode = false;

    public void ShowAd()
    {
        Advertisement.Initialize(GameID, TestMode);
        StartCoroutine(ShowAdWhenReady());
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady(PlacementVideoID))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show(PlacementVideoID);
    }

    [System.Obsolete]
    public void ShowRewardedAd()
    {
        Advertisement.Initialize(GameID, TestMode);
        StartCoroutine(ShowRewardedAdWhenReady());
    }

    private void RewardedAdCallback(ShowResult result)
    {
        if (result.ToString() == "Finished")
        {
            GameManager.GetComponent<GameManager>().DoubleAmoguses();
        }
    }

    [System.Obsolete]
    IEnumerator ShowRewardedAdWhenReady()
    {
        while (!Advertisement.IsReady(PlacementRewardedVideoID))
        {
            yield return new WaitForSeconds(0.5f);
        }
        ShowOptions options = new ShowOptions();
        options.resultCallback = RewardedAdCallback;
        Advertisement.Show(PlacementRewardedVideoID, options);
    }
}
