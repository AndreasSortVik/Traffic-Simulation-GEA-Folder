using UnityEngine;

public class TrafficLightControl : MonoBehaviour
{
    private MeshRenderer[] _meshRenderers;
    private float _time;

    private const float SwitchToYellow = 5f;
    private const float SwitchToGreen = 7f;
    private const float SwitchToRed = 25f;

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _time = 0f;

        // Changes color of child object to black
        foreach (var meshRenderer in _meshRenderers)
            meshRenderer.material.color = Color.black;
    }

    private void Update()
    {
        Timer(Time.deltaTime);
    }

    private void Timer(float deltaTime)
    {
        _time += deltaTime;
        
        if (_time < SwitchToYellow)
            ChangeTrafficLight("red");
        
        if (_time is >= SwitchToYellow and < SwitchToGreen)
            ChangeTrafficLight("yellow");
        
        if (_time is >= SwitchToGreen and < SwitchToRed)
            ChangeTrafficLight("green"); 
        
        if (_time >= SwitchToRed)
            _time = 0;
    }

    private void ChangeTrafficLightsToBlack()
    {
        foreach (var meshRenderer in _meshRenderers)
            meshRenderer.material.color = Color.black;
    }
    
    private void ChangeTrafficLight(string col)
    {
        switch (col)
        {
            case "red":
                ChangeTrafficLightsToBlack();
                _meshRenderers[1].material.color = Color.red;
                break;
            case "yellow":
                ChangeTrafficLightsToBlack();
                _meshRenderers[2].material.color = Color.yellow;
                break;
            case "green":
                ChangeTrafficLightsToBlack();
                _meshRenderers[3].material.color = Color.green;
                break;
        }
    }
}
