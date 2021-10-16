using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenager : MonoBehaviour
{

    public float vertical;
    public float horizontal;
    public float cameraSpeed;
    public float zoomSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(1))
        //{
        //    float y = Input.GetAxis("Mouse X");
        //    float x = Input.GetAxis("Mouse Y");
        //   //Debug.Log(x + ":" + y);
        //    Vector3 rotateValue = new Vector3(x, y * -1, 0);
        //    transform.eulerAngles = transform.eulerAngles - rotateValue;
        //}
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontal,  0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) * cameraSpeed * Time.deltaTime);
        Quaternion actualRot = transform.rotation;
        transform.rotation = Quaternion.identity;
        transform.Translate(new Vector3(0, 0,-vertical) * cameraSpeed * Time.deltaTime);
        transform.rotation = actualRot;

    }
}
