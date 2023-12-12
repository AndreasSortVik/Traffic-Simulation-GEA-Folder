using UnityEngine;

public class TrafficLightControl : MonoBehaviour
{
    [HideInInspector] public float time;

    [SerializeField] private AICarMovement[] AIScripts;
    private ManageScene manageSceneScript;
    
    private MeshRenderer[] _meshRenderers;
    private string _trafficLightColor;
    
    private void Awake()
    {
        // Initialises the children's mesh renderers
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        
        // Initialises scripts
        GameObject otherObject = GameObject.Find("AI Cars");
        if (otherObject != null)
            AIScripts = otherObject.GetComponentsInChildren<AICarMovement>();
        
        otherObject = GameObject.Find("SceneManager");
        if (otherObject != null)
            manageSceneScript = otherObject.GetComponent<ManageScene>();
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
        if (!other.CompareTag("Player"))
            return;
        
        // Checks if the traffic light is red, if so player has ran the red light
        if (_trafficLightColor == "red")
        {
            manageSceneScript.SetResetVariables(true, "You ran a red light.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Checks if first AI car has entered trigger box
        if (other.name == "AI Car")
        {
            if (_trafficLightColor is "red" or "yellow")
                AIScripts[0].PauseAnimation();
            
            if (_trafficLightColor == "green")
                AIScripts[0].PlayAnimation();
        }
        
        // Checks if second AI car has entered trigger box
        if (other.name == "AI Car (1)")
        {
            if (_trafficLightColor is "red" or "yellow")
                AIScripts[1].PauseAnimation();
            
            if (_trafficLightColor == "green")
                AIScripts[1].PlayAnimation();
        }
        
        // Checks if third AI car has entered trigger box
        if (other.name == "AI Car (2)")
        {
            if (_trafficLightColor is "red" or "yellow")
                AIScripts[2].PauseAnimation();
            
            if (_trafficLightColor == "green")
                AIScripts[2].PlayAnimation();
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
