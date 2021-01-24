using UnityEngine;
using System.Collections;

public abstract class GameStateBase : MonoBehaviour
{
	public abstract void Activate();
	public abstract void Deactivate();
	public abstract void UpdateState();

	public override string ToString()
	{
		return this.GetType().ToString();
	}
}
