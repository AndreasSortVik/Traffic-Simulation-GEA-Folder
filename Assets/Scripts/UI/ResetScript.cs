using TMPro;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    private TMP_Text TMPText;

    private void Awake()
    {
        TMPText = GetComponent<TMP_Text>();
    }

    public void ChangeText(string displayMessage, int time)
    {
        TMPText.text = displayMessage + "\n\nResetting driver's test in " + time.ToString();
    }
}
