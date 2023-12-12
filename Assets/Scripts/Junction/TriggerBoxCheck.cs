using System;
using UnityEngine;

public class TriggerBoxCheck : MonoBehaviour
{
    //[HideInInspector]
    public bool triggered;
    public string otherObjectName;
    
    private void OnTriggerStay(Collider other)
    {
        // For the trigger box by the yield sign
        if (CompareTag("YieldTrigger"))
        {
            if (!other.CompareTag("AI"))
                return;
            
            triggered = true;
            otherObjectName = other.name;
        }
        
        // For the large trigger boxes that check if the road is clear
        if (CompareTag("LargeTrigger"))
        {
            //print(other.name + " has triggered large trigger box");
            
            // if (!other.CompareTag("AI") || !other.CompareTag("Player"))
            //     return;
            
            if (other.CompareTag("AI") || other.CompareTag("Player"))
                triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CompareTag("YieldTrigger"))
        {
            if (!other.CompareTag("AI"))
                return;

            triggered = false;
        }

        if (CompareTag("LargeTrigger"))
        {
            // if (!other.CompareTag("AI") || !other.CompareTag("Player"))
            //     return;
            
            if (other.CompareTag("AI") || other.CompareTag("Player"))
                triggered = false;
        }
    }
}
