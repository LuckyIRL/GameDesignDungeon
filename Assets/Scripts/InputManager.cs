using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputMaster inputMaster;



    // Start is called before the first frame update
    private void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
}
