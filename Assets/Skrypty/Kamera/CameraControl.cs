using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { get; private set; }

    public float minOgraniczenieX;
    public float maxOgraniczenieX;
    public float minOgraniczenieY ;
    public float maxOgraniczenieY;


    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float orthographicSize;
    private float targetOrthographicSize;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void Update()
    {
        Poruszanie();
        Przyblizenie();
    }
    private void Poruszanie()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float edgeScrollingSize = 55;
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
            {
                x = +1f;
            }
            if (Input.mousePosition.x < edgeScrollingSize)
            {
                x = -1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
            {
                y = +1f;
            }
            if (Input.mousePosition.y < edgeScrollingSize)
            {
                y = -1f;
            }
        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;
        Vector3 holder;

             holder = transform.position += (moveDir * moveSpeed * Time.unscaledDeltaTime); // anty przyspieszenie kamery


        Vector3 clampedPosition = Vector3.zero;
        clampedPosition.x = Mathf.Clamp(holder.x,minOgraniczenieX, maxOgraniczenieX);
        clampedPosition.y = Mathf.Clamp(holder.y, minOgraniczenieY, maxOgraniczenieY);
        transform.position = clampedPosition;
    }

    private void Przyblizenie()
    {
        float zoomAmount = 2f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;

        float minOrthographicSize = 10;
        float maxOrthographicSize = 30;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
