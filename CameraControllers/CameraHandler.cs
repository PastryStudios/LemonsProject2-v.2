using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minOrthoSize, maxOrthSize;

    [SerializeField] private CinemachineVirtualCamera cinemamachineVirtualCamera;
    private float orthographicSize;
    private float targetOrthographicSize;
    void Start()
    {
        //xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");
        orthographicSize = cinemamachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(xInput, yInput).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomSpeed;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthoSize, maxOrthSize);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemamachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
