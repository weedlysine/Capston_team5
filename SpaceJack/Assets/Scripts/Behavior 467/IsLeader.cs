using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsLeader : Conditional
{
	public GameObject drone1;
	public override TaskStatus OnUpdate()
	{
		if (drone1.CompareTag("leader"))
		{
			return TaskStatus.Success;
		}
		else return TaskStatus.Failure;
	}
}