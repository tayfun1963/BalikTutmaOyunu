using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARFilterPlanes : MonoBehaviour
{
    public static ARFilterPlanes instance;

    
    [SerializeField] ARSession arSession =null;
    private ARPlaneManager arPlaneManager;
    private List<ARPlane> arPlanes;
    public ARPlane arPlane;
    private GameObject waterPlane;
    private CreateWaterMesh waterScript;
    public GameObject fishingRod;

    public GameObject createBtn;
    public GameObject resetBtn;

    public GameObject[] childUis;
    RectTransform[] childObjs;

    public void Start(){
        instance = this;
        childObjs = gameObject.GetComponentsInChildren<RectTransform>();
       
    }

  private void OnEnable()
  {
        arPlanes = new List<ARPlane>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        arPlaneManager.planesChanged += OnPlanesChanged;
        waterScript = gameObject.GetComponent<CreateWaterMesh>();
        arSession.Reset();
        waterScript.RemovePlane();
        arPlane = null;
  }
  void OnDisable()
  {
       arPlaneManager.planesChanged -= OnPlanesChanged; 
  }

  private void OnPlanesChanged(ARPlanesChangedEventArgs args){
        if(arPlane==null && args.added !=null && args.added.Count > 0 ){
            arPlane = args.added[0];
        }

        foreach(ARPlane plane in args.added){
            plane.gameObject.SetActive(false);
        }

        arPlane.gameObject.SetActive(true);

  }

    public void ResetDetection(){
        arSession.Reset();
        arPlaneManager.enabled = true;
        waterScript.RemovePlane();
        createBtn.SetActive(true);
        
    }

    public void CreateWater(){
        if (arPlane == null)
        {
            
            SystemInfo.instance.SetSystemInfo("Önce zemin oluşturunuz");
            return;
        }

        arPlane.gameObject.SetActive(false);
        arPlaneManager.enabled = false;
        waterScript.CreatePlane(arPlane.extents.x*1.5f,arPlane.extents.y*1.5f, arPlane.center);
        ShowUIs();
        HideBtns();
        arPlaneManager.enabled = false;
        // OnDisable();
    }


    private void HideBtns(){
        createBtn.SetActive(false);
        resetBtn.SetActive(false);
    }

    private void ShowUIs(){
        
        int size = childUis.Length;
        for( int i=0; i<size;i++){
            childUis[i].SetActive(true);
        }
     
        HideBtns();
    }

     private void HideAllUIs(){
        
        int size = childUis.Length;
        for( int i=0; i<size;i++){
            childUis[i].SetActive(false);
        }
    }


}
