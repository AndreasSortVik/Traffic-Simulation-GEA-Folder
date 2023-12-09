using System;
using UnityEngine;

public class TrafficLightControl : MonoBehaviour
{
    public float time;
    
    private MeshRenderer[] _meshRenderers;
    private string _trafficLightColor;
    
    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Start()
    {
        // Changes color of child object to black
        foreach (var meshRenderer in _meshRenderers)
            meshRenderer.material.color = Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if player has entered trigger box
        if (other.name != "Cartoon_SportCar_B01")
            return;
        
        if (_trafficLightColor == "red")
        {
            print("Player ran a red light!");
        }
    }

    // Changes the color of traffic light
    public void TrafficLightLoop()
    {
        time += Time.deltaTime;

        if (time < 15f)
            _trafficLightColor = "red";

        if (time >= 15f && time < 28f)
            _trafficLightColor = "green";

        if (time >= 28f && time < 30f)
            _trafficLightColor = "yellow";
            
        if (time >= 30f)
            time = 0;
        
        ChangeTrafficLight(_trafficLightColor);
    }
    
    private void ChangeTrafficLight(string col)
    {
        switch (col)
        {
            case "green":
                TurnOffLights();
                _meshRenderers[1].material.EnableKeyword("_EMISSION");
                _meshRenderers[4].material.EnableKeyword("_EMISSION");
                _meshRenderers[1].material.SetColor("_EmissionColor", Color.green);
                _meshRenderers[4].material.SetColor("_EmissionColor", Color.green);
                break;
            case "yellow":
                TurnOffLights();
                _meshRenderers[2].material.EnableKeyword("_EMISSION");
                _meshRenderers[5].material.EnableKeyword("_EMISSION");
                _meshRenderers[2].material.SetColor("_EmissionColor", Color.yellow);
                _meshRenderers[5].material.SetColor("_EmissionColor", Color.yellow);
                break;
            case "red":
                TurnOffLights();
                _meshRenderers[3].material.EnableKeyword("_EMISSION");
                _meshRenderers[6].material.EnableKeyword("_EMISSION");
                _meshRenderers[3].material.SetColor("_EmissionColor", Color.red);
                _meshRenderers[6].material.SetColor("_EmissionColor", Color.red);
                break;
        }
    }
    
    private void TurnOffLights()
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            meshRenderer.material.DisableKeyword("_EMISSION");
            meshRenderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            meshRenderer.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
