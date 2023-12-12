using System.Collections.Generic;
using UnityEngine;

// This script checks if any other cars are inside the two large trigger boxes whilst one of the AI cars are inside the small trigger box by the yield sign
public class ThreeWayJunctionManager : MonoBehaviour
{
    private AICarMovement[] AIScripts;

    private List<TriggerBoxCheck> largeTriggerScripts;
    private TriggerBoxCheck yieldTriggerScript;
    
    private GameObject AICarObject;
    
    private void Awake()
    {
        InitializeScripts();
    }

    private void InitializeScripts()
    {
        // Initialises trigger scripts
        largeTriggerScripts = new List<TriggerBoxCheck>();
        TriggerBoxCheck[] triggerBoxScripts = GetComponentsInChildren<TriggerBoxCheck>();

        for (int i = 0; i < triggerBoxScripts.Length; i++)
        {
            if (triggerBoxScripts[i].CompareTag("YieldTrigger"))
                yieldTriggerScript = triggerBoxScripts[i];
            
            if (triggerBoxScripts[i].CompareTag("LargeTrigger"))
                largeTriggerScripts.Add(triggerBoxScripts[i]);
        }
        
        // Initialises AI scripts
        AICarObject = GameObject.Find("AI Cars");
        
        if (AICarObject != null)
        {
            AIScripts = AICarObject.GetComponentsInChildren<AICarMovement>();
        }
    }

    private void Update()
    {
        // If AI car is inside the yield trigger box
        if (yieldTriggerScript.triggered)
        {
            if (largeTriggerScripts[0].triggered || largeTriggerScripts[1].triggered)
                AIScripts[ReturnAIScriptIndex()].PauseAnimation();
            
            if (!largeTriggerScripts[0].triggered && !largeTriggerScripts[1].triggered)
                AIScripts[ReturnAIScriptIndex()].PlayAnimation();
        }
    }

    // Returns index for AI script depending on the name of the object
    private int ReturnAIScriptIndex()
    {
        string otherObjectName = yieldTriggerScript.otherObjectName;
        
        if (otherObjectName == "AI Car")
        {
            return 0;
        }
        
        // Checks if second AI car has entered trigger box
        if (otherObjectName == "AI Car (1)")
        {
            return 1;
        }
        
        // Checks if third AI car has entered trigger box
        if (otherObjectName == "AI Car (2)")
        {
            return 2;
        }

        return int.MaxValue;
    }
}
