using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOnce : MonoBehaviour {

    private Animator animator;
    public float Speed = 1;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.speed = Speed;
    }

    void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
        if (info.normalizedTime >= 1.0f)
        {
            this.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
