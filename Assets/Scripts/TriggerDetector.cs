using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    public UnityEvent<GameObject> triggerEntered;
    public UnityEvent<GameObject> triggerExited;

    private void OnTriggerEnter(Collider other)
    {
        triggerEntered.Invoke(other.gameObject); 
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExited.Invoke(other.gameObject);
    }
}
