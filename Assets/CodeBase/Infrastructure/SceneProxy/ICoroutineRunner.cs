using System.Collections;
using UnityEngine;

namespace Infrastructure.SceneProxy
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}