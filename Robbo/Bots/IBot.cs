using System;

namespace Robbo.Bots
{
    public interface IBot : IDisposable
    {
        void Go();
    }
}
