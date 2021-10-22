using System;

namespace Project.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ProjectEntities Init();
    }
}