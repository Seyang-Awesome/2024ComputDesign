using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusPatternManager : MonoBehaviour
{
    private SpriteRenderer[] patterns;
    private int index = 0;

    [SerializeField]
    private float duration = 5f;

    private void Start()
    {
        patterns = GetComponentsInChildren<SpriteRenderer>();
        GetComponentInParent<Abacus>().onFinishLevel += OnFinishedLevel;
        GetComponentInParent<Abacus>().onFinishGame += OnFinishedLevel;

        foreach (var pattern in patterns) 
        {
            pattern.material.color -= new Color(0, 0, 0, 1);
        }
    }

    private void OnFinishedLevel()
    {
        if (index >= patterns.Length)
            return;

        patterns[index].material.DOColor(Color.white, duration).SetEase(Ease.InQuad);
        index++;
    }
}
