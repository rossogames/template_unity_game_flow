using System;

namespace Rossogames.Inputs.Enums
{
    [Flags]
    public enum CursorButton : byte
    {
        None = 0,
        Left = 1,
        Right = 2,
        Middle = 4
    }
}