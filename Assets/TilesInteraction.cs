using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesInteraction : MonoBehaviour
{
    public Camera camera;
    private bool flag = false;
    //destination point
    private Vector3 endPoint;
    //alter this to change the speed of the movement of player / gameobject
    public float duration = 50.0f;
    //vertical position of the gameobject
    private float yAxis;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //check if the screen is touched / clicked
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {
            RaycastHit hit;
            //Create a Ray on the tapped / clicked position
            Ray ray;

            ray = camera.ScreenPointToRay(Input.mousePosition);
            // ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            //Check if the ray hits any collider
            if (Physics.Raycast(ray, out hit))
            {               
                //set a flag to indicate to move the gameobject
                flag = true;
                //save the click / tap position
                endPoint = hit.point;
                Debug.Log(endPoint);
            }
            Debug.Log(hit.transform.name);

        }
        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
        if (flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        { //&& !(V3Equal(transform.position, endPoint))){
            //move the gameobject to the desired position
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal
        else if (flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            flag = false;
            Debug.Log("I am here");
        }

    }
}
