using UnityEngine;

public class ParkingManager : MonoBehaviour
{
    private ParkingTriggerCheck[] parkingTriggerScripts;
    private ManageScene manageSceneScript;

    private void Awake()
    {
        parkingTriggerScripts = GetComponentsInChildren<ParkingTriggerCheck>();
        
        GameObject sceneObject = GameObject.Find("SceneManager");
        if (sceneObject != null)
            manageSceneScript = sceneObject.GetComponent<ManageScene>();
    }

    private void Update()
    {
        // Checks if both trigger boxes have been entered, meaning player has parked car at correct spot
        if (parkingTriggerScripts[0].triggered && parkingTriggerScripts[1].triggered)
        {
            manageSceneScript.FinishScene();
        }
    }
}
