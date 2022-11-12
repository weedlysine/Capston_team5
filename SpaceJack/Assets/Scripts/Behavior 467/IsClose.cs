using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsClose : Conditional
{
	public GameObject Jack;
	public GameObject Robot;
	float distance;
	public override TaskStatus OnUpdate()
	{
		distance = Vector3.Distance(Jack.transform.position, Robot.transform.position);
		if (distance <= 5.0f)
		{
			return TaskStatus.Success;
		}
		else
		{
			return TaskStatus.Failure;
		}
	}
}