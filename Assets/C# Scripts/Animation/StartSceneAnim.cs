using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneAnim : MonoBehaviour
{
    [SerializeField]
    private float duration = 5f;

    [SerializeField]
    [Multiline(5)]
    private string[] lines;

    private int currentIndex;

    private Text backGroundWord;

    
    private void Start()
    {
        backGroundWord = GetComponent<Text>();
        backGroundWord.text = "";
    }

    public void MoveNextLine()
    {
        backGroundWord.text = "";
        backGroundWord.DOText(lines[currentIndex++], duration).SetEase(Ease.Linear);
    }
}
