using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BehaviourTreeNode
{
    public override bool process()
    {
        foreach(BehaviourTreeNode node in children){
            if(!node.process())
            return false;
        }
        return true;
    }
}
