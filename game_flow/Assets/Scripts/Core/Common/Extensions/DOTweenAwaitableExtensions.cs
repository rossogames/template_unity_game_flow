using DG.Tweening;
using UnityEngine;

namespace Game.Common.Extensions
{
    public static class DOTweenAwaitableExtensions
    {
        public static async Awaitable Await(this Tween tween)
        {
            var acs = new AwaitableCompletionSource();

            if (tween == null || !tween.IsActive() || tween.IsComplete())
            {
                acs.SetResult();
                await acs.Awaitable;
            }

            tween.OnComplete(() => acs.TrySetResult());
            tween.OnKill(() => acs.TrySetResult());

            await acs.Awaitable;
        }

    }
}
