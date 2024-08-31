using System.Collections;
using UnityEngine;

namespace ArkheroClone.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}