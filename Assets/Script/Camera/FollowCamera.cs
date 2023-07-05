using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField]
    Vector3 CameraPos = new Vector3(0, 7, -7);


    [Header("카메라 거리"), SerializeField]
    float CameraDist = 6;
    [Header("카메라 X축 속도"), SerializeField]
    float xSpeed = 250.0f;
    [Header("카메라 Y축 속도"), SerializeField]
    float ySpeed = 120.0f;
    [Header("카메라 Y축 최저점"), SerializeField]
    float yMinLimit = -20.0f;
    [Header("카메라 Y축 최고점"), SerializeField]
    float yMaxLimit = 40.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    float distance;


    private void FixedUpdate()
    {

       if (GameManager.Instance.IsSkill == false && GameManager.Instance.IsUi == false)
        {

            if (Input.GetKey(KeyCode.Mouse1))
            {

                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            }
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 6, -distance) + PlayerManager.Instance.transform.position;

            transform.rotation = rotation;
            transform.position = position;
            distance = CameraDist;
            transform.LookAt(PlayerManager.Instance.transform);
        }
    }

    

    float ClampAngle(float ag, float min, float max)
    {
        if (ag < -360)
            ag += 360;
        if (ag > 360)
            ag -= 360;
        return Mathf.Clamp(ag, min, max);
    }
}

