
using UnityEngine;

public class CursorControl : MonoBehaviour {

    private void Start () {

        LockCursor();
    }
    private void OnDestroy () {

        UnlockCursor();
    }

    //from here: https://github.com/harroo/endeavor

    // Locks the Cursor and disabled it's visibility.
    public void LockCursor ( ) {

        Cursor.visible = false ;

        // Locks the Cursor to the center of the Screen.
        Cursor.lockState = CursorLockMode.Locked ;

    }

    // Enables the Cursor and it's visibility.
    public void UnlockCursor ( ) {

        Cursor.visible = true ;

        // Releases the Cursor.
        Cursor.lockState = CursorLockMode.None ;

    }
}
