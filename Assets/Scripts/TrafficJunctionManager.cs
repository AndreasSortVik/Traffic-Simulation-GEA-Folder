using UnityEngine;

public class TrafficJunctionManager : MonoBehaviour
{
    private TrafficLightControl[] _trafficLightScripts;
    
    private void Start()
    {
        _trafficLightScripts = GetComponentsInChildren<TrafficLightControl>();

        // Sets start time in traffic light loop for each traffic light in junction
        _trafficLightScripts[0].time = 0f;
        _trafficLightScripts[1].time = 15f;
        _trafficLightScripts[2].time = 0;
        _trafficLightScripts[3].time = 15f;
    }

    private void Update()
    {
        foreach (var trafficLightScript in _trafficLightScripts)
            trafficLightScript.TrafficLightLoop();
    }
}
