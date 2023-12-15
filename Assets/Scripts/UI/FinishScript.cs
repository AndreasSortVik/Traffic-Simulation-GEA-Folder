using UnityEngine;

public class FinishScript : MonoBehaviour
{
    [HideInInspector] public bool restartGame;
    
    public void OnClickRestart()
    {
        restartGame = true;
    }
    
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
