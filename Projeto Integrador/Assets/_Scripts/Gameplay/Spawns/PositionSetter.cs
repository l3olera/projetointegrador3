using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    [Header("Configuração do Spawn")]
    public float ResetDelay = 1f;

    private Rigidbody _rb;
    [SerializeField] private CinemachineDecollider _cinemachineDecollider;

    public void Teleport(GameObject target)
    {
        if (target == null)
        {
            return;
        }

        _rb = target.GetComponent<Rigidbody>();

        if (_rb != null)
            _rb.isKinematic = true;

        if (_cinemachineDecollider != null)
            _cinemachineDecollider.enabled = false; // Desativa o decollider antes de teleportar

        target.transform.SetPositionAndRotation(transform.position, target.transform.rotation);

        StartCoroutine(CompareTeleporting(target));

    }

    IEnumerator CompareTeleporting(GameObject target)
    {
        // Aguarda até o objeto estar praticamente no mesmo ponto
        while (Vector3.Distance(target.transform.position, transform.position) > 0.1f)
        {
            Debug.Log(Vector3.Distance(target.transform.position, transform.position));
            yield return null; // Espera um frame
        }

        if (_rb != null)
            _rb.isKinematic = false;

        if (_cinemachineDecollider != null)
            _cinemachineDecollider.enabled = true;
    }
}
