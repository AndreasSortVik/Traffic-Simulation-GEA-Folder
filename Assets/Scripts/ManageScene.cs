using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    [HideInInspector] public bool disablePlayerInput;
    
    // Game objects
    [SerializeField] private GameObject startObject;
    [SerializeField] private GameObject directionsObject;
    [SerializeField] private GameObject resetObject;
    [SerializeField] private GameObject finishObject;
    
    // Scripts
    [SerializeField] private ResetScript resetScript;
    [SerializeField] private StartScript startScript;
    [SerializeField] private FinishScript finishScript;
    

    private float time;
    private bool resetScene;
    private string displayMessage;
    
    private void Start()
    {
        StartScene();
    }
    
    private void Update()
    {
        if (resetScene)
            ResetScene();
        else
            time = 5f;
        
        if (startScript.startGame)
            ActivateDirections();
        
        // Resets scene immediately 
        if (finishScript.restartGame)
            ResetSceneNoDelay();
    }

    private void StartScene()
    {
        // Pauses game
        Time.timeScale = 0;
        
        // Disables all other UI game objects
        directionsObject.SetActive(false);
        resetObject.SetActive(false);
        finishObject.SetActive(false);
        
        // Enables the start game object
        startObject.SetActive(true);
    }

    private void ActivateDirections()
    {
        // Resumes game
        Time.timeScale = 1;
        
        // Disables all other UI game objects
        startObject.SetActive(false);
        resetObject.SetActive(false);
        finishObject.SetActive(false);
        
        // Enables the directions game object
        directionsObject.SetActive(true);
    }

    // Gets called when car is parked in correct area
    public void FinishScene()
    {
        // Pauses game
        Time.timeScale = 0;
        
        // Disables all other UI game objects
        startObject.SetActive(false);
        resetObject.SetActive(false);
        directionsObject.SetActive(false);
        
        // Enables the finish game object
        finishObject.SetActive(true);
    }

    public void SetResetVariables(bool reset, string resetMessage)
    {
        resetScene = reset;
        displayMessage = resetMessage;
        startScript.startGame = false;
    }
    
    private void ResetScene()
    {
        // Disables player's ability to control car
        disablePlayerInput = true;
        
        // Disables all other UI game objects
        startObject.SetActive(false);
        directionsObject.SetActive(false);
        finishObject.SetActive(false);
        
        // Enables the reset game object
        resetObject.SetActive(true);
        
        time -= Time.deltaTime;

        // Changes the text on screen
        resetScript.ChangeText(displayMessage, time.ConvertTo<int>());
        
        // Resets scene after 5 seconds
        if (time <= 0f)
        {
            resetScene = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void ResetSceneNoDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
