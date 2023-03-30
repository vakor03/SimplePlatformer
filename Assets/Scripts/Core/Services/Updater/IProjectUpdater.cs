using System;

namespace Core.Services.Updater
{
    public interface IProjectUpdater
    {
        event Action UpdateCaller;
        event Action FixedUpdateCaller;
        event Action LateUpdateCaller;
    }
}