using System;

namespace SimplePersistence.Model.Security
{
    /// <summary>
    ///     EntityType that represents one specific user claim
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public class UserClaim<TUserKey> : Entity<int>, IUserClaim<TUserKey> 
        where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        ///     User Id for the user who owns this claim
        /// </summary>
        public virtual TUserKey UserId { get; set; }

        /// <summary>
        ///     Claim type
        /// </summary>
        public virtual string ClaimTypeId { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        public virtual string ClaimValue { get; set; }
    }

    public class UserClaim : UserClaim<string> { }
}