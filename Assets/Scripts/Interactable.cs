using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string message;
    public UnityEvent onInteract;

    public void Interact()
    {
        onInteract.Invoke();
    }
    
}
