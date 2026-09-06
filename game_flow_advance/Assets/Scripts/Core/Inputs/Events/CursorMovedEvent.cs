using UnityEngine;

namespace RossoGames.Inputs.Events
{
    /// <summary>
    /// Evento de movimiento del cursor. La instancia de esta clase puede ser reutilizada
    /// por InputsService para evitar asignaciones en cada frame.
    /// </summary>
    /// <remarks>
    /// ADVERTENCIA: esta es una referencia única compartida. No mantengas referencias a
    /// esta instancia ni la uses de forma asíncrona. Si necesitas persistir los datos,
    /// copia los valores (por ejemplo, new Vector2(...)).
    /// </remarks>
    public sealed class CursorMovedEvent : IEvent
    {
        public Vector2 ScreenPosition;
    }
}
