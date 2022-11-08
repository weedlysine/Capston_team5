using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree
{
    List<BehaviourTree> child = new List<BehaviourTree>();
}

public class ActionTask : BehaviourTree
{

}

public class CompositeTask : BehaviourTree
{

}

public class ConditionalAbort : BehaviourTree
{

}

public class DecorationTask : BehaviourTree
{

}