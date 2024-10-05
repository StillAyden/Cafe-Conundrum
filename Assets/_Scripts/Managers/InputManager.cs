using UnityEngine;

public class InputManager : MonoBehaviour
{

    #region Variables

    [Header("References")]
    public static InputManager Instance;
    public PlayerInputs Inputs;

    //[Header("Delegates")]
    public delegate void PlayerHandler();
    public event PlayerHandler Interacted;

    public delegate void UIHandler();
    public event UIHandler Selected;
    public event UIHandler Backed;

    [Space]
    [Header("Movements")]
    [SerializeField] public Vector2 moveInput;

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
        //Input Subscriptions
        //Player
        Inputs.Player.Interact.performed += x => Interact();

        //UI
        Inputs.UI.Select.performed += x => Select();
        Inputs.UI.Back.performed += x => Back();
    }

    public void DisablePlayerMovement()
    {
        Inputs.Player.Disable();
    }

    private void Update()
    {
        moveInput = GetMoveAxis();
    }

    #region Player
    void Interact()
    { 
        Interacted?.Invoke();
    }

    Vector2 GetMoveAxis()
    {
        return Inputs.Player.Move.ReadValue<Vector2>();
    }

    #endregion

    #region User Interface
    void Select()
    {
        Selected?.Invoke();
    }

    void Back()
    {
        Backed?.Invoke();
    }
    
    #endregion

}
