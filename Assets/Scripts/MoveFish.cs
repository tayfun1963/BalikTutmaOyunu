using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    
    float direction = 1;
    float rightLimit, leftLimit;
    float frontLimit, backLimit;
    float topLimit, bottomLimit;
    public float offset = 0.8f;
    public float zInterval=10; 
    public float speed=50;
    private Vector3 moveVector;
    private GameObject waterWall;
    private GameObject target;
    private GameObject dlgFish; 
    private  ManageFishDlg fishDlgManager;
    Camera waterCam;
    
    void Start()
    {
        
        speed = Random.Range(10,200);
        moveVector= Vector3.right * speed;

        dlgFish = GameObject.Find("Canvas").transform.Find("Dialog Fish").gameObject;

        fishDlgManager =  dlgFish.GetComponent<ManageFishDlg>();
        InitLimitCoords(); 
        InitObjectPosition(); 
    }

    private void InitLimitCoords(){
        waterWall = GameObject.Find( "WaterWall" );
        waterCam = GameObject.Find("WaterCamera").GetComponent<Camera>();
        
        float height = Camera.main.orthographicSize;

        
        rightLimit = waterCam.ScreenToWorldPoint(((float)Screen.width)*Vector3.right).x*offset;
        leftLimit = waterCam.ScreenToWorldPoint(Vector3.zero).x*offset;

        
        topLimit = waterWall.transform.localScale.y/2 + waterWall.transform.position.y;
        bottomLimit = waterCam.ScreenToWorldPoint((height * -1 + waterCam.gameObject.transform.position.x)*Vector3.up).y*offset;

        
        frontLimit = waterWall.transform.position.z/offset;
        backLimit = frontLimit - zInterval;
      
    }

    private void InitObjectPosition(){

        float x = Random.Range(leftLimit,rightLimit);
        float y = Random.Range(bottomLimit,topLimit);
        float z = Random.Range(frontLimit,backLimit);

        Vector3 pos = new Vector3(x,y,z);
        gameObject.transform.position = pos;

    }

    
    void Update()
    {
         
        if(gameObject.transform.position.x > rightLimit && direction > 0 ){
            direction=-1;
            transform.Rotate(Vector3.up, 180.0f, Space.World); 
           
        } 
        else if(gameObject.transform.position.x < leftLimit && direction < 0 ){
            direction =1;
            transform.Rotate(Vector3.up, 180.0f, Space.World);
        }
        transform.position += moveVector*Time.deltaTime*direction;


        if(Input.GetMouseButtonDown(0)){
            target = GetClickedObject();

            if(target!=null && target.Equals(gameObject)){
                int itemType,itemCode;
                int i = GetItemIndexFromName();
                itemType = Fish.GetItemType(i,out itemCode);
                fishDlgManager.GetClickedFish(gameObject);
                fishDlgManager.SetItemInfo(itemType,itemCode);
                fishDlgManager.OpenDlg();
            }

        }
    }

    private GameObject GetClickedObject(){
        RaycastHit hit;
        GameObject target =  null;

        Ray ray = waterCam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray.origin,ray.direction*10,out hit)){
            target = hit.collider.gameObject;
        }
        return target;
    }


    public void RemoveFish(){
        gameObject.SetActive(false);
    }

    public int GetItemIndexFromName(){
        string[] split_data = gameObject.name.Split(' ');
        int itemIndex =  int.Parse(split_data[1]);
        return itemIndex;
    }



    




}
