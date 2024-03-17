using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TimeRevertManager : MonoSingleton<TimeRevertManager>
{
    public float decorationMinY;
    public float decorationMaxY;
    public bool isRevert = false;

    public Action<float> onTimeScaleChange;
    public Action onTrigger;

    [SerializeField]
    private TextMeshProUGUI tmp;
    [SerializeField]
    private float startDistance;
    [SerializeField]
    private float endDistance;
    [SerializeField]
    private Vector3 dir;
    [SerializeField]
    private Vector3 center;

    private void Start()
    {
        center = center + transform.position;
    }

    private void Update()
    {
        if(!isRevert)
            SetTimeScale();
    }

    private void SetTimeScale()
    {
        // 计算投影向量差值
        float value = Vector3.Dot(Player.Instance.transform.position - center, dir.normalized);
        value = (value - endDistance) / (startDistance - endDistance);
        value = Mathf.Clamp(value, 0, 1);

        if (value == 1)
            tmp.text = "1";
        else if (value == 0) 
            tmp.text = "0";
        else
            tmp.text = value.ToString("F2");
        onTimeScaleChange?.Invoke(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onTrigger?.Invoke();
    }

    private void OnDrawGizmos()
    {
        // 绘制边框线框
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center + transform.position + decorationMaxY * Vector3.up, new Vector3(40f, 1f, 20f));
        Gizmos.DrawWireCube(center + transform.position + decorationMinY * Vector3.up, new Vector3(40f, 1f, 20f));

        // 绘制球体
        Gizmos.DrawSphere(center + transform.position, 1f);
        Vector3 startSpherePos = center + transform.position + dir.normalized * startDistance;
        Vector3 endSpherePos = center + transform.position + dir.normalized * endDistance;
        Gizmos.DrawSphere(startSpherePos, 1f);
        Gizmos.DrawSphere(endSpherePos, 1f);

        Gizmos.DrawLine(center + transform.position, startSpherePos);
        Gizmos.DrawLine(center + transform.position, endSpherePos);
        Gizmos.DrawLine(startSpherePos, endSpherePos);
    }

    public void RevertTime()
    {
        isRevert = true;
        StartCoroutine(AnimateNumber());
    }

    float currentValue = 0;

    private IEnumerator AnimateNumber()
    {
        float duration = 1f;
        float startTime = Time.time;
        float startValue = 0f;
        float endValue = -1f;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            currentValue = Mathf.Lerp(startValue, endValue, t);
            onTimeScaleChange?.Invoke(currentValue);
            UpdateNumber();
            yield return null;
        }

        currentValue = endValue;
        UpdateNumber();
        onTimeScaleChange?.Invoke(-1);
    }

    private void UpdateNumber()
    {
        if (currentValue == 0)
            tmp.text = "0";
        else if (currentValue == -1)
            tmp.text = "-1";
        else
            tmp.text = currentValue.ToString("F2");
    }
}
