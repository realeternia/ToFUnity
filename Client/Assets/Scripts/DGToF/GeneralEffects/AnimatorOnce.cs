using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOnce : MonoBehaviour {

    private Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
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
