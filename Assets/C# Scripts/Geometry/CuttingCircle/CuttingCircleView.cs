using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCircleView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI areaText;
    [SerializeField] private Transform lineRoot;
    [SerializeField] private LineRenderer linePrefab;
    [SerializeField] private float radius;
    [SerializeField] private HexagonCylinder hexagonCylinder;
    private float startFontSize;
    private List<LineRenderer> lines = new();
    
    public event Action OnClickPanel;

    private void Start()
    {
        startFontSize = areaText.fontSize;
    }

    public void OnPointerClick(PointerEventData _)
    {
        OnClickPanel?.Invoke();
    }

    public void OnModelUpdate(CuttingCircleData data)
    {
        areaText.DOBounceFontSize(startFontSize, data.area.ToString(), Consts.UIChangeDuration);

        // 六边形没有偏移角度，补六条边，十二边型偏移30度，补六条边，二十四边形偏移15度，补12条边以此类推
        float offsetAngle = data.lineNum == 6 ? 0f : 360f / data.lineNum;
        // 此次需要多生成的线的条数，第一次6，第二次6，第三次12，第四次24
        int lineNum = data.lineNum == 6 ? 6 : data.lineNum / 2;
        // 此次生成的线与圆心相连形成的角的角度
        float cellAngle = 360f / lineNum;
        
        hexagonCylinder.SetSides(lineNum, () =>
        {
            for (int i = 0; i < lineNum; i++)
            {
                float angle = (offsetAngle + cellAngle * i) * Mathf.Deg2Rad;
                Vector3 localPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                LineRenderer newLine = Instantiate(linePrefab, lineRoot);
                newLine.transform.localPosition = localPos;

                Vector3 target = newLine.GetPosition(1);
                newLine.SetPosition(1, Vector3.zero);

                DOTween.To(
                    () => newLine.GetPosition(1),
                    pos => newLine.SetPosition(1, pos), 
                    target,
                    Consts.CuttingCircleLineDuration);

                // 改scale表现上好像不是很强
                // newLine.transform.localScale = Vector3.zero;
                // newLine.transform.DOScale(Vector3.one, Consts.ObjectsChangeDuration);
            }
        });
    }
}

