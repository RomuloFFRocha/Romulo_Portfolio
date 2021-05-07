using UnityEngine;

public class HideImage : MonoBehaviour
{
    public GameObject showUpImageObject;

    public void Hide()
    {
        showUpImageObject.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
    }
}
