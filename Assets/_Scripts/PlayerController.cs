/* PlayerController.cs
 * 
 *  This script controls all of the movement and interaction from the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("References")]
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector3(InputManager.Instance.moveInput.x * moveSpeed * Time.fixedDeltaTime, 
                                  rb.velocity.y,
                                  InputManager.Instance.moveInput.y * moveSpeed * Time.fixedDeltaTime);
    }

}
