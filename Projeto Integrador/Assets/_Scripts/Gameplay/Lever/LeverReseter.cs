using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverReseter : LeverBase
{
    [SerializeField] private List<Lever> _levers;
    [SerializeField] private float _animDuration = 1f;
    protected override void LeverEvent()
    {
        SetOwnAnimation();
        Reset();
    }

    protected override void SetOwnAnimation()
    {
        base.SetOwnAnimation();
        StartCoroutine(ChangeAnimation());
    }

    IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(_animDuration);

        if (state && canChange)
        {
            SetOwnAnimation();
        }
    }

    void Reset()
    {
        foreach (var lever in _levers)
        {
            lever?.ResetToDefault(); // Chama o método de reset em cada lever
        }
    }
}