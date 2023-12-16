using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private BehaviourTreeNode behaviourTree;
    // Start is called before the first frame update
    void BuildBehaviourTree()
    {
        SequenceNode enteredFrontAttackSequence = new SequenceNode();
        enteredFrontAttackSequence.addChild(new Attacking());
        enteredFrontAttackSequence.addChild(new ContinueFrontAttack());

        SelectorNode enteredFrontAttackSelector = new SelectorNode();
        enteredFrontAttackSelector.addChild(enteredFrontAttackSequence);
        enteredFrontAttackSelector.addChild(new StartAttacking());

        SequenceNode checkIfEntersInFrontAttack = new SequenceNode();
        checkIfEntersInFrontAttack.addChild(new CheckPlayerInFront());
        checkIfEntersInFrontAttack.addChild(enteredFrontAttackSelector);

        SequenceNode enteredSpinAttackSequence = new SequenceNode();
        enteredSpinAttackSequence.addChild(new Attacking());
        enteredSpinAttackSequence.addChild(new ContinueSpinAttack());

        SelectorNode enteredSpinAttackSelector = new SelectorNode();
        enteredSpinAttackSelector.addChild(enteredSpinAttackSequence);
        enteredSpinAttackSelector.addChild(new StartAttacking());

    }

    // Update is called once per frame
    void UpdateBehaviourTreeProcess()
    {
        
    }
}
