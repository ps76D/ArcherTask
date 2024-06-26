using System.Collections;
using UnityEngine;

namespace TaskArcher.Infrastructure.Services
{
    public sealed class Coroutines : MonoBehaviour
    {
        public static Coroutines Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("[COROUTINE MANAGER]");
                    _instance = go.AddComponent<Coroutines>();
                    DontDestroyOnLoad(go);
                }
            
                return _instance;
            }
        }

        private static Coroutines _instance;

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return Instance.StartCoroutine(enumerator);
        }

        public static void StopRoutine(Coroutine routine)
        {
            if (routine != null)
                Instance.StopCoroutine(routine);
        }
    }
}
