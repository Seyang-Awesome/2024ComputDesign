using DG.Tweening;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellaCrossFade : MonoBehaviour
{
    public GameObject[] stars;
    public SpriteRenderer lineSR;
    public SpriteRenderer BGSR;

    private GameObject moveStar;
    private Rigidbody msRb;

    private bool isStartFly = false;
    private bool isStartLine = false;
    private bool isStartBG = false;
    private int index = 1;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveStar = Instantiate(stars[0], stars[0].transform.position, Quaternion.identity);
        msRb = moveStar.AddComponent<Rigidbody>();
        BGSR.material.DOFade(0f, 0.1f);
        lineSR.material.DOFade(0f, 0.1f);
        msRb.useGravity = false;
        moveStar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartFly)
        {
            if(index >= stars.Length)
            {
                isStartFly = false;
                Destroy(moveStar);
                isStartLine = true;
                return;
            }
            FlyAllStar(index);
            if (Vector3.Distance(moveStar.transform.position, stars[index].transform.position) <= 0.1f)
            {
                index++;
            }
        }
        if (isStartLine)
        {
            lineSR.gameObject.SetActive(true);
            lineSR.material.DOFade(1f, 3f);
            timer += Time.deltaTime;
            if (timer >= 2.5f)
            {
                isStartLine = false;
                isStartBG = true;
            }
        }
        if (isStartBG)
        {
            BGSR.gameObject.SetActive(true);
            BGSR.material.DOFade(0.1f, 3f);
            isStartBG = false;
        }
    }
    private void OnMouseEnter()
    {
        moveStar.SetActive(true);
        //遍历飞行
        isStartFly = true;
    }
    private void FlyAllStar(int index)
    {
        moveStar.transform.position = Vector3.MoveTowards(moveStar.transform.position, stars[index].transform.position, Time.fixedDeltaTime * 2);
    }
}
