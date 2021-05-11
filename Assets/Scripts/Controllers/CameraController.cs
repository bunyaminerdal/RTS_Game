using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cameraMain;
    private float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public Vector3 maxZoomAmount;
    public Vector3 minZoomAmount;


    public Vector3 loadPosition;
    public Vector3 loadZoom;
    public Vector3 loadRotation;

    private Vector3 newPosition;
    private Vector3 newZoom;
    private Quaternion newRotation;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;

    private bool isGameMenuOpened;
    
    public float timecatch = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraMain.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {   if(loadPosition!=Vector3.zero)
        {            
            transform.position = loadPosition;
            transform.eulerAngles = loadRotation;
            cameraMain.transform.localPosition = loadZoom;
            newPosition = loadPosition;
            newRotation.eulerAngles = loadRotation;
            newZoom = loadZoom;
            
            loadPosition=Vector3.zero;
        }else{
            if(!isGameMenuOpened)
            {
                if(Time.timeScale==0)
                {
                    timecatch=Time.fixedDeltaTime/2;
                }else
                {
                    timecatch=Time.deltaTime;
                }
                movementSpeed = 0.05f-(newZoom.z/500f);
                HandleMouseInput();
                HandleMovementInput();
                //Lerp olduğu için menüler kıpırdaşıyor
                transform.position = Vector3.Lerp(transform.position, newPosition, timecatch* movementTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, timecatch* movementTime);
                cameraMain.transform.localPosition = Vector3.Lerp(cameraMain.transform.localPosition,newZoom, timecatch * movementTime);
            }
            
        }
        
    }

    void HandleMouseInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            if(newZoom.y > minZoomAmount.y && Input.mouseScrollDelta.y >0)
            {
            newZoom += Input.mouseScrollDelta.y *  zoomAmount * 3f;
            }
            if(newZoom.y < maxZoomAmount.y && Input.mouseScrollDelta.y <0)
            {
            newZoom += Input.mouseScrollDelta.y *  zoomAmount * 3f;
            }
        }

        if(Input.GetMouseButtonDown(2) && !Input.GetKey(KeyCode.LeftShift))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if(Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftShift))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
        if(Input.GetMouseButtonDown(2) && Input.GetKey(KeyCode.LeftShift))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftShift))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3  difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x/5f));
        }
    }
    void HandleMovementInput()
    {        
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition  += (transform.forward * movementSpeed);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition  += (transform.forward * -movementSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition  += (transform.right * movementSpeed);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition  += (transform.right * -movementSpeed);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if(Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if(Input.GetKey(KeyCode.R))
        {
            if(newZoom.y > minZoomAmount.y)
            {
                newZoom += zoomAmount;
            }            
        }
        if(Input.GetKey(KeyCode.F))
        {
            if(newZoom.y < maxZoomAmount.y)
            {
            newZoom -= zoomAmount;
            }
        }

                
    }

    public void gameMenuIsOpened(bool isOpened)
    {
        isGameMenuOpened = isOpened;
    }
}
