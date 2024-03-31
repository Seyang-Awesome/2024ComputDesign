using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPattern : MonoBehaviour
{
    public SpriteRenderer[] sprites; // 存储所有的精灵
    public float fadeInDuration = 1.0f; // 淡入持续时间
    public float fadeOutDuration = 1.0f; // 淡出持续时间
    public float intervalBetweenSprites = 2.0f; // 每个精灵之间的间隔时间

    private int currentIndex = 0;

    void Start()
    {
        foreach(var s in sprites)
            s.material.color = s.material.color.GetTransparent();
        StartCoroutine(FadeInFadeOutLoop());
    }

    IEnumerator FadeInFadeOutLoop()
    {
        sprites[currentIndex].material.DOFade(1f, fadeInDuration);
        currentIndex++;

        yield return new WaitForSeconds(intervalBetweenSprites);

        sprites[currentIndex].material.DOFade(1f, fadeInDuration);

        yield return new WaitForSeconds(intervalBetweenSprites);

        while (true)
        {
            sprites[(currentIndex + sprites.Length - 1) % sprites.Length].material.DOFade(0f, fadeOutDuration);
            yield return new WaitForSeconds(fadeOutDuration);
            sprites[(currentIndex + sprites.Length + 1) % sprites.Length].material.DOFade(1f, fadeInDuration);
            currentIndex = (currentIndex + 1) % sprites.Length;

            print((currentIndex + sprites.Length - 1) + "  " + currentIndex + "  " + (currentIndex + sprites.Length + 1));

            // 等待淡入完成
            yield return new WaitForSeconds(intervalBetweenSprites);
        }
    }
}

