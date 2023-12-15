using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BehaviourTreeNode
{
    public override bool process()
    {
        foreach(BehaviourTreeNode node in children){
            if(node.process()) return true;
        }
        return false;
    }
}
