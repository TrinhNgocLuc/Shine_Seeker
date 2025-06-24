using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GoalEffect : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float duration = 1f;

    void Start()
    {
        Vector3 startPos = transform.position;
        transform.DOMoveY(startPos.y + moveDistance, duration)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
}
