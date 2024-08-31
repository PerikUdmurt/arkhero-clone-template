using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace ArkheroClone.Infrastructure.Timer
{
    public class CustomTimer
    {
        public event Action<int> CountdownChanged;

        public async UniTask<bool> StartCountdown(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                CountdownChanged?.Invoke(seconds - i);
                await UniTask.WaitForSeconds(1);
            }
            CountdownChanged?.Invoke(0);
            return true;
        }
    }
}