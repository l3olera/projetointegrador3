using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Garante que apenas uma instância exista
            return;
        }
        Instance = this;
    }

    public bool IsInteractKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z);
    }

    public bool IsSmellKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.X);
    }
}