using System;
using System.Collections;
using UnityEngine;

public class SlideState : State
{
    public override event Action OnStateClosed;

    private MonoBehaviour _monoBehaviour;

    private float _slideTime;

    public SlideState(float slideTime, MonoBehaviour monoBehaviour)
    {
        _slideTime = slideTime;
        _monoBehaviour = monoBehaviour;
    }

    public override void Enter()
    {
        _monoBehaviour.StartCoroutine(Sliding());
    }

    public override void Update()
    {

    }

    public override void BreakState()
    {
        _monoBehaviour.StopCoroutine(Sliding());
    }

    public override void Exit()
    {
        OnStateClosed = null;
    }

    private IEnumerator Sliding()
    {
        yield return new WaitForSeconds(_slideTime);

        OnStateClosed?.Invoke();
    }
}
