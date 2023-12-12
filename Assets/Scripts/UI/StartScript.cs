using UnityEngine;

public class StartScript : MonoBehaviour
{
    [HideInInspector] public bool startGame;
    
    public void StartGame()
    {
        startGame = true;
    }
    
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
