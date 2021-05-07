using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    public float sensitivity = 1;

    Transform head;

    Vector3 rotationHead = Vector3.zero;

    private void Start()
    {
        head = transform.GetChild(0);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Rotacação do corpo
        Vector3 rotationBody = transform.localEulerAngles;
        rotationBody.y += Input.GetAxis("Mouse X") * sensitivity;
        transform.localEulerAngles = rotationBody;

        //Rotatação da cabeça
        rotationHead.x -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationHead.x = Mathf.Clamp(rotationHead.x,-60, 60);
        head.localEulerAngles = rotationHead;
    }
}
