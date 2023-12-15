using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTreeNode
{
    protected List<BehaviourTreeNode> children;
    public abstract bool process();
    public BehaviourTreeNode()
    {
        children = new List<BehaviourTreeNode>();
    }
    public void addChild(BehaviourTreeNode node)
    {
        children.Add(node);
    }
}
