using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float animSpeed = 1f;

    void Start()
    {
        _animator.SetFloat("animSpeed", animSpeed);
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        // If playing forward and finished, reverse
        if (animSpeed > 0.5f && stateInfo.normalizedTime >= 1f)
        {
            animSpeed = -1f;
            _animator.SetFloat("animSpeed", animSpeed);
        }
        // If playing backward and finished, forward
        else if (animSpeed < -0.5f && stateInfo.normalizedTime <= 0f)
        {
            animSpeed = 1f;
            _animator.SetFloat("animSpeed", animSpeed);
        }
    }
}