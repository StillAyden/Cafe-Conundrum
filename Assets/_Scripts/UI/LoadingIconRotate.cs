using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIconRotate : MonoBehaviour
{
    public GameObject loadingScreenIcon;
    [SerializeField] private float rotationSpeed = 50f;

    void Update()
    {
        loadingScreenIcon.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
