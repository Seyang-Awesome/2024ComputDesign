using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbacusLines : MonoBehaviour
{
    [SerializeField]
    private int mult = 1;

    [SerializeField]
    private GameObject destroyParticle;

    private int value = 0;
    private int Value
    {
        get { return value; }
        set 
        {
            parent.OnValueChange((value - this.value) * mult);
            this.value = value;
        }
    }
    
    private Abacus parent;

    private AbacusChilds[] childs;
    private Stack<AbacusChilds> upStack = new();
    private Stack<AbacusChilds> downStack = new();
    private AbacusChilds highDigitChild;

    //初始化，找到高位算珠，将低位算珠入栈
    private void Start()
    {
        parent = GetComponentInParent<Abacus>();

        childs = GetComponentsInChildren<AbacusChilds>();
        childs.Sort((o1,o2) => { return (int)Mathf.Sign(o2.transform.position.y - o1.transform.position.y); });

        highDigitChild = childs[0];
        highDigitChild.isHighDigit = true;

        for(int i = childs.Length - 1;  i >= 1; i--) 
        {
            downStack.Push(childs[i]);
        }

        parent.onFinishGame += OnComplete;
    }

    public void MoveUp(bool isHighDigit)
    {
        if(!isHighDigit)
        {
            var c = downStack.Pop();
            upStack.Push(c);
            c.MoveUp();
            Value += 1;
        }
        else
        {
            highDigitChild.MoveUp();
            Value += 5;
        }
    }

    public void MoveDown(bool isHighDigit)
    {
        if (!isHighDigit)
        {
            var c = upStack.Pop();
            downStack.Push(c);
            c.MoveDown();
            Value -= 1;
        }
        else
        {
            highDigitChild.MoveDown();
            Value -= 5;
        }
    }

    private void OnComplete()
    {
        StartCoroutine(EndAnim());
    }

    private IEnumerator EndAnim()
    {
        for(int i = 0; i < childs.Length; i++) 
        {
            yield return new WaitForSeconds(0.2f);
            var c = childs[i];
            Instantiate(destroyParticle, c.transform.position, Quaternion.identity);
            Destroy(c.gameObject);
        }
    }
}
