using TMPro;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public int transitionDuration;
    public float animVelocity = 1f;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void StartFade()
    {
        _anim.speed = animVelocity;
        _anim.SetTrigger("Start");
    }

    public void EndFade()
    {
        _anim.speed = animVelocity;
        _anim.SetTrigger("End");
    }
}