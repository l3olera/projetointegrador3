using Unity.Cinemachine;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; } // Instância única do ReferenceManager

    [Header("Referências Globais")]
    // Referências a outros componentes ou objetos do jogo
    public DialogueControl dialogueControl;
    public PlayerMovement playerMovement;
    public CinemachineInputAxisController cinemachineCameraIn;
    public InventoryController inventoryController;
    public ObjectivesController objectivesController;

    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return;
        }

        Instance = this; // Define a instância atual como a única instância
    }
}
