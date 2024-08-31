using ArkheroClone.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArkheroClone.Services.SceneLoader
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner runner)
        {
            _coroutineRunner = runner;
        }

        public void Load(SceneName sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName.ToString(), onLoaded));
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        private IEnumerator LoadScene(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}
