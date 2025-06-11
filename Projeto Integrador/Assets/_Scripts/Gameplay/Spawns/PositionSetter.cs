using System;
using System.Collections;
using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    [Header("Configuração do Spawn")]
    public float rigidbodyResetDelay = 0.1f;

    private Rigidbody _rb;

    public void Teleport(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target Object não foi atribuído.");
            return;
        }

        target.transform.SetPositionAndRotation(transform.position, target.transform.rotation);

        _rb = target.GetComponent<Rigidbody>();
        if (_rb != null)
        {
            _rb.isKinematic = true;
            StartCoroutine(ResetKinematicAfterDelay());
        }
    }

    private IEnumerator ResetKinematicAfterDelay()
    {
        yield return new WaitForSeconds(rigidbodyResetDelay);
        if (_rb != null)
            _rb.isKinematic = false;
    }
}
