using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusText : MonoBehaviour
{
    public GameObject particle;

    [SerializeField]
    private bool particleOnUpdate; //更新时是否生成粒子，简单来说就是懒了，懒得给上下两个text分类了

    private void Start()
    {
        GetComponentInParent<Abacus>().onFinishGame += OnComplete;
        GetComponentInParent<Abacus>().onFinishLevel += OnUpdateText;
    }

    private void OnUpdateText()
    {
        if(particleOnUpdate)
            Instantiate(particle,transform.position,particle.transform.rotation);
    }

    private void OnComplete()
    {
        Instantiate(particle, transform.position, particle.transform.rotation);
        Destroy(gameObject);
    }
}
