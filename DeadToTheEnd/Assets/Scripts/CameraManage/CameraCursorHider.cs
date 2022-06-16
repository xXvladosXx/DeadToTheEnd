

using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraCursorHider : MonoBehaviour
{
    bool cursorLocked = false;
    Transform cam;

    // void Start(){
    //     Cursor.visible = false;
    //     Cursor.lockState = CursorLockMode.Locked;
    //     cam = UnityEngine.Camera.main.transform;
    // }
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape)){
        //     if(cursorLocked){
        //         Cursor.visible= true;
        //         Cursor.lockState = CursorLockMode.None;
        //     }else{
        //         Cursor.visible= false;
        //         Cursor.lockState = CursorLockMode.Locked;
        //     }
        // }
        //
    }
}
