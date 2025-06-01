using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARFilterPlanes : MonoBehaviour
{
    public static ARFilterPlanes instance;

<<<<<<< HEAD
    
=======
    // [SerializedField] private Vector2 dimensionsForBigPlane;
>>>>>>> da68dd2e469b9e8b3aa924e7f09a8b4ef45d6a22
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
<<<<<<< HEAD
       
=======
        // HideAllUIs();
>>>>>>> da68dd2e469b9e8b3aa924e7f09a8b4ef45d6a22
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
<<<<<<< HEAD
       arPlaneManager.planesChanged -= OnPlanesChanged; 
=======
       arPlaneManager.planesChanged -= OnPlanesChanged; // 더 이상 이벤트를 받지 않음 
>>>>>>> da68dd2e469b9e8b3aa924e7f09a8b4ef45d6a22
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
<<<<<<< HEAD
            
            SystemInfo.instance.SetSystemInfo("Önce zemin oluşturunuz");
=======
            // 인식된 평면이 없을 경우
            SystemInfo.instance.SetSystemInfo("Recognize the plane first!");
>>>>>>> da68dd2e469b9e8b3aa924e7f09a8b4ef45d6a22
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
<<<<<<< HEAD
        
=======
        // RectTransform[] childObjs = gameObject.GetComponentsInChildren<RectTransform>();
>>>>>>> da68dd2e469b9e8b3aa924e7f09a8b4ef45d6a22
        int size = childUis.Length;
        for( int i=0; i<size;i++){
            childUis[i].SetActive(true);
        }
     
        HideBtns();
    }

     private void HideAllUIs(){
<<<<<<< HEAD
        
=======
        // RectTransform[] childObjs = gameObject.GetComponentsInChildren<RectTransform>();
>>>>>>> da68dd2e469b9e8b3aa924e7f09a8b4ef45d6a22
        int size = childUis.Length;
        for( int i=0; i<size;i++){
            childUis[i].SetActive(false);
        }
    }


}
