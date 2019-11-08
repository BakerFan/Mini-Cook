using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCamera : MonoBehaviour
{
    //这个变量用来记录单指双指的变换
    private bool m_IsSingleFinger;

    public float scaleSpeed = 5.0f;
    public float minScale = 1.0f;
    public float maxScale = 150.0f;
    public float currentScale;
    private Vector2 lastSingleTouchPosition;

    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;
    void Start()
    {
        //根据当前摄像机是正交还是透视进行对应赋值
        if (GetComponentInChildren<Camera>().orthographic == true)
        {
            currentScale = GetComponentInChildren<Camera>().orthographicSize;
        }
        else
        {
            currentScale = GetComponentInChildren<Camera>().fieldOfView;
        }
    }

    // Update is called once per frame

    void Update()
    {
        //获取鼠标滚轮的值，向前大于0，向后小于0，并设置放大缩小范围值
        currentScale += Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
        currentScale = Mathf.Clamp(currentScale, minScale, maxScale);

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
            {
                //在开始触摸或者从两字手指放开回来的时候记录一下触摸的位置
                lastSingleTouchPosition = Input.GetTouch(0).position;
                Debug.Log("in!");
            }
            m_IsSingleFinger = true;

        }
        if (Input.touchCount > 1)
        {
            //当从单指触摸进入多指触摸的时候,记录一下触摸的位置
            //保证计算缩放都是从两指手指触碰开始的
            if (m_IsSingleFinger)
            {
                oldPosition1 = Input.GetTouch(0).position;
                oldPosition2 = Input.GetTouch(1).position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                scaleCamera();
            }

            m_IsSingleFinger = false;
        }

        //根据当前摄像机是正交还是透视进行对应赋值，放大缩小
        if (GetComponentInChildren<Camera>().orthographic == true)
        {
            GetComponentInChildren<Camera>().orthographicSize = currentScale;
        }

        else
        {
            GetComponentInChildren<Camera>().fieldOfView = currentScale;
        }
    }

    private void scaleCamera()
    {
        //计算出当前两点触摸点的位置
        var tempPosition1 = Input.GetTouch(0).position;
        var tempPosition2 = Input.GetTouch(1).position;


        float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
        float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);

        //计算上次和这次双指触摸之间的距离差距
        //然后去更改摄像机的距离
        currentScale -= (currentTouchDistance - lastTouchDistance)*0.01f * scaleSpeed * Time.deltaTime;


        //把距离限制住在min和max之间
        currentScale = Mathf.Clamp(currentScale, minScale, maxScale);


        //备份上一次触摸点的位置，用于对比
        oldPosition1 = tempPosition1;
        oldPosition2 = tempPosition2;
    }
}
