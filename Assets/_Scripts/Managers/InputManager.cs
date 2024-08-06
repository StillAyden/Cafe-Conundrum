using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    #region Variables

    [Header("References")]
    public static InputManager Instance;
    public PlayerInputs Inputs;

    [Space]
    [Header("Movements")]
    [SerializeField] public Vector2 moveInput;

    [Header("Delegates")]
    public delegate void PlayerHandler();
    public event PlayerHandler Interacted;
    #endregion

    private void Awake()
    {
        Instance = this;
        Inputs = new PlayerInputs();

        //Temp
        Inputs.Enable();
    }

    private void Start()
    {
        //Subscriptions
        Inputs.Player.Interact.performed += x => Interact();
    }

    private void Update()
    {
        moveInput = GetMoveAxis();
    }

    void Interact()
    { 
        Interacted?.Invoke();
    }

    Vector2 GetMoveAxis()
    {
        return Inputs.Player.Move.ReadValue<Vector2>();
    }
}
