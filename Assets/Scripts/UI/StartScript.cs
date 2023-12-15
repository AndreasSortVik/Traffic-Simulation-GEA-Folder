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
        Application.Quit();
    }
}
