using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    public Move CurMove { get; set;}
    public bool isPlayScene = false;
    
    // Start is called before the first frame update

    public void Jump(InputAction.CallbackContext context)
        {
            if(isPlayScene) CurMove.Jump(context.ReadValueAsButton());
        }
    
    public void DashFunc(InputAction.CallbackContext context)
        {
            if(isPlayScene) CurMove.DashFunc(context.ReadValueAsButton());
        }
        
    public void Crouch(InputAction.CallbackContext context)
        {
            if(isPlayScene) CurMove.Crouch(context.ReadValueAsButton());
        }
    

    public void OnMove(InputAction.CallbackContext context)
    {
        if(isPlayScene) CurMove.OnMove(context.ReadValue<float>());
    }
}
