using UnityEngine;
using System.Collections;
namespace Assets.Scripts.Infrastructure
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);
	}
}
