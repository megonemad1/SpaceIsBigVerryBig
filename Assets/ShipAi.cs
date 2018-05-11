using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipAi: MonoBehaviour
{
    [SerializeField]
    protected List<ScriptableDecisionOption> _decisionOptions = new List<ScriptableDecisionOption>();
    protected Stack<ScriptableDecisionOption> decisionOptions = new Stack<ScriptableDecisionOption>();
    public RandomInitable R;
    public ShipSpawner controler;
    [SerializeField]
    public CoroutineLoop decisionLoop;
    protected MovemonetHarness mover;
    [SerializeField]
    public float xDirection;
    public Action onFixedUpdate = () => { };
    [SerializeField]
    public int ScoreValue;

    protected void Awake()
    {
        decisionLoop.onTick += Tick;
        mover = GetComponent<MovemonetHarness>();
    }

    public void AddToScore(PlayerScriptableObject player)
    {
        player.score += ScoreValue;
    }
   
    public void Tick()
    {
        if (decisionOptions != null && decisionOptions.Count > 0)
        {
            var decision = decisionOptions.Pop();
            this.name = name.Split('(')[0] + "(" + decision.name + ")";
            decision.go(this, controler);
        }
        else
        {

            this.name = name.Split('(')[0] + "(Self_Destruct)";
            StartCoroutine(killAfter(5));
        }
    }



    private IEnumerator killAfter(float after)
    {
        yield return new WaitForSeconds(after);
        SelfDestruct();

    }

    public void SelfDestruct()
    {
        decisionLoop.killDecisionLoop = true;
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (!mover)
            Debug.LogError("missing mover", this.gameObject);
        mover.Move(xDirection * Vector2.right + Vector2.down);
        onFixedUpdate.Invoke();
    }

    public List<ScriptableDecisionOption> DecisionOptions
    {
        set
        {
            decisionOptions.Clear();
            _decisionOptions.Clear();
            value.ForEach(i =>
            {
                decisionOptions.Push(i);
                _decisionOptions.Add(i);
            });
        }
    }

    internal void addDecision(ScriptableDecisionOption scriptableDecisionOption)
    {
        decisionOptions.Push(scriptableDecisionOption);
        _decisionOptions.Add(scriptableDecisionOption);
    }

    private void OnValidate()
    {
        decisionOptions = new Stack<ScriptableDecisionOption>(_decisionOptions);
        _decisionOptions = decisionOptions.ToList();
    }

}