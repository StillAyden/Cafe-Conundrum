using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public PlayerInputs Inputs;

    [Space]
    [SerializeField] public Vector2 moveInput;

    public delegate void PlayerHandler();
    public event PlayerHandler Interacted;

    private void Awake()
    {
        Instance = this;
        Inputs = new PlayerInputs();

        //Temp
        Inputs.Enable();
    }

    private void Start()
    {
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
