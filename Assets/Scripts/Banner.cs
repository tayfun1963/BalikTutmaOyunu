using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Banner : MonoBehaviour
{


    [SerializeField] BannerPosition _bannerPosition = BannerPosition.CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; 

    void Start()
    {
        
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        

        
        Advertisement.Banner.SetPosition(_bannerPosition);

        
        LoadBanner();
        ShowBannerAd();
    }

    
    public void LoadBanner()
    {
        
        BannerLoadOptions options = new BannerLoadOptions
        {

            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        
        Advertisement.Banner.Load(_adUnitId, options);
    }

    
    void OnBannerLoaded()
    {
        Debug.Log("Banner Yüklendi");

        
    }

    
    void OnBannerError(string message)
    {
        Debug.Log($"Hata: {message}");
        
    }

    
    void ShowBannerAd()
    {
        
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        
        Advertisement.Banner.Show(_adUnitId, options);
    }

   
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {
        
    }

    private void OnDisable()
    {
        HideBannerAd();
    }
}