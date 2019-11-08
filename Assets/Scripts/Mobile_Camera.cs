using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_Camera : MonoBehaviour
{
    Gyroscope gyro;
    Quaternion quatMult;
    Quaternion quatMap;
    //UILabel ul;
    public GameObject camParent;



    public void InitTuoLuoYi()
    {
        // find the current parent of the camera's transform
        // instantiate a new transform
        // match the transform to the camera position
        camParent.transform.position = transform.position;
        // make the new transform the parent of the camera transform
        transform.parent = camParent.transform;

        gyro = Input.gyro;

        gyro.enabled = true;
        camParent.transform.eulerAngles = new Vector3(90, 0, 0);
        quatMult = new Quaternion(0, 0, 1, 0);

    }

    void Start()
    {
        InitTuoLuoYi();
    }
    void Update()
    {
        quatMap = new Quaternion(gyro.attitude.x, gyro.attitude.y, gyro.attitude.z, gyro.attitude.w);
        Quaternion qt = quatMap * quatMult;

        transform.localRotation = qt;
    }
}
