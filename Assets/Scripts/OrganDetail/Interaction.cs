using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    private Camera camera; 
    private GameObject gyroscope; 

    private float perspectiveZoomSpeed = 0.05f; 
    private float orthoZoomSpeed = 0.05f; 

    private float eigen = 0.005f; 

    touch touchZero; 
    touch touchOne; 
    Vector2 touchZeroPrevPos; 
    Vector2 touchOnePrevPos; 
    float prevTouchDeltaMag; 
    float touchDeltaMag; 
    float deltaMagnitudeDiff; 

    Touch touch; 
    float horizontal; 
    float vertical; 

    private float Azi = 0f; 
    private float Ele = 0f; 
    float sensitivity = 5f; 

    Gesture currentGes; 

    enum Gesture {
        None,      // không có cử chỉ 
        Stationary,   // đứng yên, bất động
        Pres,      // 
        Swipe     // vuốt 
    }

    Vector2 oriPosition;

    // Start is called before the first frame update
    void Start()
    {
        gyroscope = GameObject.Find("Gyroscope"); 
        camera = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        currentGes = Gesture.None; 
        if(Input.touchCount < 1)
        {
            return; 
        }
        Touch touch = Input.GetTouch(0); 

        if (Input.touchCount == 1 && (Helper.GetObjectOnTouchByTag(touch.position, ObjectTag.mainView) != null))
        {
            HandleSingleTouch(touch); 
        }

        else if (Input.touches.Length == 2)
        {
            HandleMultiTouch(touch); 
        }
    }

    private void HandleSingleTouch(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                oriPosition = touch.position; 
                break; 

            case TouchPhase.Moved: 
                Vector2 delta = touch.position - oriPosition; 
                if(delat.magnitude > sensitivity)
                    Move(touch, delta); 
                break; 
            
            case TouchPhase.Stationary: 
                currentGes = Gesture.Stationary; 
                break; 

            case TouchPhase.End:

            case TouchPhase.Canceled: 
                currentGes = Gesture.None; 
                break; 
        }
    }

    private void HandleMultiTouch(Touch touch)
    {
        touchZero = Input.GetTouch(0); 
        touchOne = Input.GetTouch(1); 

        touchZeroPrevPos = touchZero.position - touchOnePrevPos.deltaPosition; 
        touchOnePrevPos = touchOne.position - touchOnePrevPos.deltaPosition; 

        prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; 
        touchDeltaMag = (touchZero.position - touchOne.position).magnitude; 

        // Find the difference in the distance between each frame
        // Tìm sự khác biệt về khoảng cách giữa mỗi frame
        deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag; 

        // If the camera is orthographic.
        // 
        if(camera.orthographic)
        {
            //change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed; 

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 10f); 
 
        }
        else 
        {
            //otherwise change the field of view based on the change in between the touches.
            camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed; 

            // Clamp the field of view to make sure it's between 10 and 90:
            camera.fieldOfView = Math.Clamp(camera.fieldOfView, 10f, 90f); 

        }
    }

    // private void HandleInteraction(Touch touch)
    // {

    // }

    private void HandleInteraction (Gesture ges)
    {

    }

    private void Pres (Touch touch)
    {

    }

    private void Move (Touch touch, Vector2 data)
    {
        if(OrganManager.IsMoving)
            PerformTranform(delta); 

        else
            PerformRotate(delta); 
    
    }

    private void PerformTranform(Vector2 delta)
    {
        Vector3 translate = new Vector3 (
            camera.transform.position.x - delta[0] * eigen * 0.02f * Mathf.Cos(gyroscope.transform.eulerAngles.y * Math.Deg2Rad), 
            camera.transform.position.y - delta[0] * eigen * 0.02f, 
            camera.transform.position.z + delta[0] * eigen * 0.02f * Mathf.Sin(gyroscope.transform.eulerAngles.y * Mathf.Deg2Rad)); 

        if(10 < translate.magnitude && translate.magnitude < 12)
            camera.transform.position = translate; 
    }

    private void PerformRotate(Vector2 delta)
    {
        Ele -= delta[1] * eigen; 
        if (Mathf.Abs(Ele) < 80f)
            gyroscope.transform.eulerAngles += new Vector3(-delta[1], delta[0], 0) * eigen; 
        else 
            gyroscope.transform.eulerAngles += new Vector3(0, delta[0], 0) * eigen; 

    }
}


