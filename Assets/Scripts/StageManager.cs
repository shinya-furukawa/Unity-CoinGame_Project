using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class StageManager : SerializedMonoBehaviour
{
    [SerializeField] GameObject counterBall;
    [SerializeField] Transform counterBallField;
    [SerializeField] RouletteController rouletteController;

    [SerializeField] Material[] stageMaterials;
    const int LIGHT_DOWN = 0, LIGHT_UP = 1;
    
    private List<GameObject> counterBalls = new List<GameObject>();
    [ShowInInspector] Queue<Action> actions = new Queue<Action>();
    int counterChecker = 0;
    bool startFlag = true;

    public bool isAction = false;

    private void Start()
    {
        int posX = 4;
        for(int i = 0; i < 9; i++)
        {
            var c_ball = Instantiate(counterBall, new Vector3(posX, 10.5f, -0.4f), Quaternion.identity);
            c_ball.GetComponent<MeshRenderer>().material = stageMaterials[LIGHT_DOWN];
            c_ball.transform.SetParent(counterBallField);
            counterBalls.Add(c_ball);
            posX--;
        }
    }

    public void ChangeCounterMaterial()
    {
        if(counterChecker >= counterBalls.Count) return;

        counterBalls[counterChecker].GetComponent<MeshRenderer>().material = stageMaterials[LIGHT_UP];
        counterChecker++;
        actions.Enqueue(Action);
        
        if(startFlag)
        {
            StartCoroutine(StartAction());
            startFlag = false;
        }
    }

    public IEnumerator StartAction()
    {
        yield return new WaitForSeconds(1);

        while(checkQueue())
        {
            yield return new WaitForSeconds(1);
        }

        startFlag = true;
    }

    public bool checkQueue()
    {
        if(actions.Count == 0) return false;
        if(!rouletteController.isActiveRoulette && !isAction) actions.Dequeue().Invoke();
        return true;
    }

    public void Action()
    {
        isAction = true;
        rouletteController.OnStartRoulette();
        counterBalls[counterChecker-1].GetComponent<MeshRenderer>().material = stageMaterials[LIGHT_DOWN];
        counterChecker--;
    }
}
