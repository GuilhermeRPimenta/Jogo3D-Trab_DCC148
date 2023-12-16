using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : BehaviourTreeNode
{
    public bool attacking;
    public override bool process()
    {
        return attacking;
    }
}
