using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneAnim : MonoBehaviour
{
    [SerializeField]
    private float duration = 30f;

    [SerializeField]
    [Multiline(20)]
    private string lines;

    private Text backGroundWord;
    
    private void Start()
    {
        backGroundWord = GetComponent<Text>();
        backGroundWord.text = "";
        backGroundWord.DOText(lines, duration).SetEase(Ease.Linear);
    }
}
