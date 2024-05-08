using System.Collections;
using TaskArcher.Infrastructure.Services;
using UnityEngine;

namespace TaskArcher.Infrastructure
{
    public interface ICoroutineRunner: IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}