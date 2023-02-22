using AudienceNetwork;
using Facebook.Unity;
using UnityEngine;

public class FM : MonoBehaviour
{
    void OnApplicationPause(bool pSS)
    {
        if (!pSS)
        {
            if (FB.IsInitialized) {
                FB.ActivateApp();
            }else
            {
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            FB.Mobile.SetAdvertiserTrackingEnabled(true);
            AudienceNetworkAds.Initialize();
        }
        else
        {
            FB.Init(() =>
            {
                FB.ActivateApp();
                AudienceNetworkAds.Initialize();
            });
        }
    }
}
