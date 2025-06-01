using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    static private bool isOnAD = false;

    void Awake()
    {
        
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    
    public void LoadAd()
    {
        
        if (isOnAD) { return; }
        StartCoroutine(OnAD());
        Advertisement.Load(_adUnitId, this);
    }

    
    public void ShowAd()
    {
        
        if (isOnAD) { return; }
        StartCoroutine(OnAD());
        Advertisement.Show(_adUnitId, this);
    }

    
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (isOnAD) { return; }
        StartCoroutine(OnAD());
        Advertisement.Show(_adUnitId, this);
        
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Yükleme Hatası: {adUnitId} - {error.ToString()} - {message}");
        
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Yükleme Hatası {adUnitId}: {error.ToString()} - {message}");
        
    }

    public void OnUnityAdsShowStart(string adUnitId) {
    }
    public void OnUnityAdsShowClick(string adUnitId) {
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) {
    }

    IEnumerator OnAD()
    {
        isOnAD = true;
        yield return new WaitForSeconds(3 * 60);
        isOnAD = false;
    }
}