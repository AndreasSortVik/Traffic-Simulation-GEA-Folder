using UnityEngine;

public class ParkingTriggerCheck : MonoBehaviour
{
    [HideInInspector] public bool triggered;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            triggered = false;
    }
}
