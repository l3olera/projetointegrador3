using UnityEngine;

public class FadeController : MonoBehaviour
{
    public int transitionDuration;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void StartFade()
    {
        _anim.SetTrigger("Start");
    }

    public void EndFade()
    {
        _anim.SetTrigger("End");
    }
}