using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance {  get; private set; }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float orthographicSize;
    private float targetOrthographicSize;
    private bool edgeScrolling;

    private void Awake()
    {
        Instance = this;

        edgeScrolling = PlayerPrefs.GetInt("edgeScrolling", 0) == 1f;
    }

    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 50f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y* zoomAmount;

        float minOrthographicSize = 10f;
        float maxOrthographicSize = 30f;

        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime* zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;

        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        
        if (edgeScrolling)
        {
            float edgeScrollingSize = 30;
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize){
                x = +1f;
            }
            if (Input.mousePosition.x < edgeScrollingSize){
                x = -1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollingSize){
                x = +1f;
            }
            if (Input.mousePosition.y < edgeScrollingSize){
                x = -1f;
            }
        }
        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    public void SetEdgeScrolling(bool edgeScrolling)
    {
        this.edgeScrolling = edgeScrolling;
        PlayerPrefs.SetInt("edgeScrolling", edgeScrolling ? 1 : 0);
    }

    public bool GetEdgeScrolling()
    {
        return edgeScrolling;
    }
}
