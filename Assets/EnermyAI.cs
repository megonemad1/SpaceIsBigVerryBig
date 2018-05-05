using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnermyAI : MonoBehaviour
{
    Rigidbody2D rbody;
    MovemonetHarness mover;
    [SerializeField]
    public float xDirection;
    [SerializeField]
    float decisionIntervel;
    [SerializeField]
    List<ScriptableDecisionOption> _decisionOptions = new List<ScriptableDecisionOption>();
    Stack<ScriptableDecisionOption> decisionOptions = new Stack<ScriptableDecisionOption>();
    public List<ScriptableDecisionOption> DecisionOptions
    {
        set
        {
            decisionOptions.Clear();
            _decisionOptions.Clear();
            value.ForEach(i =>
            {
                decisionOptions.Push(i); _decisionOptions.Add(i);
            });
        }
        get { return _decisionOptions.ToArray().ToList(); }
    }

    [HideInInspector]
    public bool killDecisionLoop;
    IEnumerator decisionLoop;

    private void OnValidate()
    {
        decisionOptions = new Stack<ScriptableDecisionOption>(_decisionOptions);
        _decisionOptions = decisionOptions.ToList();
    }

    void Awake()
    {
        mover = GetComponent<MovemonetHarness>();

        decisionLoop = DecisionLoop();
    }

    public SpawnEnemy controler;
    public RandomInitable R;

    IEnumerator DecisionLoop()
    {
        yield return new WaitForSeconds(1);
        while (!killDecisionLoop)
        {
            if (decisionOptions != null && decisionOptions.Count > 0)
            {
                Debug.Log("decision Made");
                decisionOptions.Pop().go(this, controler);
            }
            else
                StartCoroutine(killAfter(5));
            yield return new WaitForSeconds(decisionIntervel);
        }
    }
    private IEnumerator killAfter(float after)
    {
        yield return new WaitForSeconds(after);
        killDecisionLoop = true;
        Destroy(this.gameObject);

    }
    private void OnEnable()
    {
        StartCoroutine(decisionLoop);
    }
    private void OnDisable()
    {
        StopCoroutine(decisionLoop);
    }
    private void FixedUpdate()
    {
        if (!mover)
            Debug.LogError("missing mover", this.gameObject);
        mover.Move(xDirection * Vector2.right + Vector2.down);
    }
}
