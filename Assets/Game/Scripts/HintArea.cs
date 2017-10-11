using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Hint area object, it will destory automatically.
/// </summary>
public class HintArea : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        transform.DOScale(1.2f, 0.8f).From().SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(AutoDestory());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator AutoDestory()
    {
        yield return new WaitForSeconds(4);

        transform.DOKill();
        yield return transform.GetComponent<CanvasGroup>().DOFade(0, 0.5f).WaitForCompletion();

        Destroy(gameObject);
    }
}
