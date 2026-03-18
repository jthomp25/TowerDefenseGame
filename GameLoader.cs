using UnityEngine;

public class GameLoader : MonoBehaviour
{
    void Start()
    {
        SaveManager.instance.ApplyPendingLoad();
    }
}