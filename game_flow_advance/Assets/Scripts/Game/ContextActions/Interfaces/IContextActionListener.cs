using UnityEngine;

namespace Rossogames.ContextActions.DataEntities
{
    public interface IContextActionListener<T> where T : ContextActionDataEntity
    {
        public bool IsContextActionEnabled(T contextActionDataEntity)
        {
            return true;
        }
        Awaitable OnContextActionInvoke(T contextActionDataEntity);
    }
}
