using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EndAnimStepInfo
{
    public GameObject stepPrefab;
    public Vector3 offsetBetween;
    public float intervalBetween;
    public float appearTime;
}

[Serializable]
public class EndAnimStateInfo
{
    public EndAnimState[] states;
    public float distanceBetween;
}

[Serializable]
public class EndAnimState
{
    public Material skyBox;
    public GameObject[] letters;
}


public class EndAnim : Gear, IAnimatable
{
    
    [SerializeField]
    private EndAnimStepInfo stepInfo;
    [SerializeField]
    private EndAnimStateInfo stateInfo;

    private Transform player;
    private Vector3 origin;
    private Canvas worldCanvas;

    private float WalkDistance => player.position.x - origin.x;
    private Canvas WorldCanvas
    {
        get 
        {
            if (worldCanvas == null || worldCanvas.IsUnityNull())
                worldCanvas = new GameObject("WorldCanvas").AddComponent<Canvas>();
            return worldCanvas;
        }
    }

    private void Start()
    {
        player = Player.Instance.transform;
        origin = transform.position;
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();
        AnimateAction();
    }

    public void AnimateAction()
    {
        StartCoroutine(StepAction());
        StartCoroutine(StateUpdateAction());
    }

    private IEnumerator StepAction()
    {
        var interval = new WaitForSeconds(stepInfo.intervalBetween);
        var pos = origin;

        //生成台阶
        for (int i = 0; i < 1000; i++)
        {
            var step = Instantiate(stepInfo.stepPrefab,transform);

            step.transform.position = pos + Quaternion.Euler(-45 ,0,0) * Vector3.down * 40;
            step.transform.DOMove(pos,stepInfo.appearTime).SetEase(Ease.OutQuad);
            pos += stepInfo.offsetBetween;

            yield return interval;
        }
    }

    private IEnumerator StateUpdateAction() //更新场景状态信息
    {
        float nextDistance = stateInfo.distanceBetween * 0.5f;
        int stateIndex = 0;

        while(true)
        {
            if(WalkDistance > nextDistance) 
            {
                nextDistance += stateInfo.distanceBetween;
                if (!ReachNewState(stateIndex++))
                    break;
            }
            yield return null;
        }

        print("已更新最后一个");
    }

    private bool ReachNewState(int index) //更新场景阶段
    {
        if (index >= stateInfo.states.Length)
            return false;

        var state = stateInfo.states[index];

        RenderSettings.skybox = state.skyBox;
        Camera.main.GetComponent<Skybox>().material = state.skyBox;

        print("开始生成字符");

        for (int i = 0; i < state.letters.Length; i++)
        {
            print("生成字符");
            var letter = Instantiate(state.letters[i], WorldCanvas.transform);

            float rand = Random.value;
            var target = player.position + Vector3.forward * (-20 + (float)i/ state.letters.Length * 40) + Vector3.right * (10 * rand + 10) + Vector3.up * (7 * rand + 8);
            letter.transform.position = target + Vector3.down * 60;
            letter.transform.DOMove(target, 1f + 2 * rand);
            letter.AddComponent<Billboard>();

        }

        return true;
    }
}
