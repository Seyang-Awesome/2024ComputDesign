using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Abacus : MonoBehaviour
{
    [SerializeField]
    private int currentValue;
    [SerializeField]
    private List<int> questionValues;
    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI valueText;

    public Action onFinishLevel;
    public Action onFinishGame;

    private int finishCount;

    private void Start()
    {
        questionText.text = questionValues[finishCount].ToString();
        valueText.text = "0";
    }

    public void OnValueChange(int changeValue)
    {
        currentValue += changeValue;
        valueText.text = currentValue.ToString();

        if(currentValue == questionValues[finishCount]) 
        {
            OnFinishLevel();
        }
    }

    private void OnFinishLevel()
    {
        finishCount++;
        if(finishCount >= questionValues.Count)
        {
            OnFinished();
            return;
        }

        onFinishLevel?.Invoke();
        questionText.text = questionValues[finishCount].ToString();

    }

    private void OnFinished()
    {
        onFinishGame?.Invoke();
    }
}
