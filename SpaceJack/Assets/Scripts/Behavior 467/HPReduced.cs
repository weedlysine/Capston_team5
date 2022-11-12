using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HPReduced : Conditional
{
	public GameObject Robot = GameObject.FindWithTag("Robot");
	int HP;
	public override TaskStatus OnUpdate()
	{
		HP = Robot.GetComponent<Robot>().HP;
		if (HP<=50)
			return TaskStatus.Success;
		else
			return TaskStatus.Failure;
	}
}
