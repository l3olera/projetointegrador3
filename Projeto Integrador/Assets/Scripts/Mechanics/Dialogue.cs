using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;

    public LayerMask playerLayer;
    public float radious;

    private DialogueControl dc;
    bool onRadious;

    void Start()
    {
        dc = FindFirstObjectByType<DialogueControl>();
    }

    void FixedUpdate()
    {
        Interact();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z)) && onRadious && dc.canInteract)
        {
            dc.Speech(profile, speechText, actorName);
        }
    }

    public void Interact(){
        Collider[] hits = Physics.OverlapSphere(transform.position, radious, playerLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player")) // Mantém a verificação para evitar problemas futuros
            {
                onRadious = true;
                break; // Evita chamadas repetidas
            }else{
                onRadious = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
