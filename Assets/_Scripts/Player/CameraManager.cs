using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera cam;
    [SerializeField] Transform target;
    [Space]
    [SerializeField] float xOffset = 0;
    [SerializeField] float yOffset = 0;
    [SerializeField] float zOffset = 0;
    [Space]
    [SerializeField] float xEulerRotation = 0;
    [SerializeField] float yEulerRotation = 0;
    [SerializeField] float zEulerRotation = 0;
    [Space]
    [SerializeField] float moveSpeed = 10f;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(target.position.x + xOffset, yOffset, target.position.z + zOffset), moveSpeed * Time.deltaTime);
        cam.transform.localRotation = Quaternion.Euler(xEulerRotation, yEulerRotation, zEulerRotation);
    }
}
