using UnityEngine;
 
/// <summary>
/// Attach to a manager object in the Main Menu scene.
/// Wire howToPlayPanel to a UI panel containing your controls
/// and goal explanation. The panel starts hidden and is toggled
/// by the "How to Play" and "Close" buttons.
/// </summary>
public class HowToPlayUI : MonoBehaviour
{
    public GameObject howToPlayPanel;
 
    void Start()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }
 
    public void Show()
    {
        howToPlayPanel.SetActive(true);
    }
 
    public void Hide()
    {
        howToPlayPanel.SetActive(false);
    }
}