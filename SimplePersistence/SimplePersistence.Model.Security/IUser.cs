using System;

namespace SimplePersistence.Model.Security
{
    public interface IUser<TUserKey> : IEntity<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        
    }
}