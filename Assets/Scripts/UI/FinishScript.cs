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
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
