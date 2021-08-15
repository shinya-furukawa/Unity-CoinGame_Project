using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] GameObject rouletteObj;
    Rigidbody rigid;

    public bool isActiveRoulette = false, isStartRoulette = false;

    private void Start()
    {
        rigid = rouletteObj.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isActiveRoulette)
        {
            if(isStartRoulette) StartRoulette();
            else
            {
                if(rigid.angularVelocity.z < 0.15f)
                {
                    Debug.Log("Stop Roulette");
                    rigid.angularVelocity = Vector3.zero;
                    Debug.Log(rouletteObj.transform.localEulerAngles.z);
                    StartCoroutine(DropBall());
                    isActiveRoulette = false;
                }
            }
        }
    }

    public void OnStartRoulette()
    {
        isActiveRoulette = true;
        isStartRoulette = true;
    }

    private void StartRoulette()
    {
        Debug.Log("Start Roulette");
        isStartRoulette = false;
        rigid.AddTorque(0, 0, Random.Range(9,12), ForceMode.Impulse);
    }

    public IEnumerator DropBall()
    {
        yield return StartCoroutine(Action());
        Debug.Log("Action End");
        yield return new WaitForSeconds(2);
        stageManager.isAction = false;
    }

    public IEnumerator Action()
    {
        Debug.Log("Action");
        yield return new WaitForSeconds(2);
    }

}
