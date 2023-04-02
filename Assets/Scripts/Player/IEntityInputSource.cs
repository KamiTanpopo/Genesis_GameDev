﻿namespace Assets.Scripts.Player
{
    public interface IEntityInputSource
    {
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        bool Jump { get; }
        bool Attack { get; }

        void ResetOneTimeAction();
    }
}
