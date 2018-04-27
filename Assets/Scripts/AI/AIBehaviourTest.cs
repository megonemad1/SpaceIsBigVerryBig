using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipStates
{
    idle

};

public class AIBehaviourTest : MonoBehaviour{
    public String state = "";
    Behaviour[] b;

    void idle()
    {

    }

    void LookArround()
    {

    }

    // Use this for initialization
    void Start () {
        b = new Behaviour[]{ };
        Array.Sort(b);
    }
	
	// Update is called once per frame
	void Update () {
       
		
	}
    class Behaviour : IComparable<Behaviour>
    {
        Action a;
        int priority = 0;
        public Behaviour(Action a, int priority)
        {
            this.a = a;
            this.priority = priority;
        }
        public int CompareTo(Behaviour other)
        {
            return priority.CompareTo(other.priority);
        }
    }
}
