using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]private float maxUpwardAngle = 45f;

    //private
    private Camera mainCamera;

    #region Unity Methods
    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        //sprite face the camera
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        //Get the rotation angles
        Vector3 targetEulerAngles = targetRotation.eulerAngles;

        //Clamp the upward angle to the maxUpwardAngle
        if (targetEulerAngles.x > 180f)
        {
            targetEulerAngles.x -= 360f; 
        }
        targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, -maxUpwardAngle, maxUpwardAngle);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(targetEulerAngles.x, targetEulerAngles.y, 0f);
    }
    #endregion
}
