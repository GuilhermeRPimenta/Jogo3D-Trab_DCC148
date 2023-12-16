using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAttacking : BehaviourTreeNode
{
    public bool attacking;

    public override bool process(){
        attacking = true;
        return true;
    }
}
