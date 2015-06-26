using System;

namespace SimplePersistence.Model.Security
{
    public interface IUserClaim<TKey> : IEntity<int> where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     User Id for the user who owns this claim
        /// </summary>
        TKey UserId { get; set; }

        /// <summary>
        ///     Claim type id
        /// </summary>
        string ClaimTypeId { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        string ClaimValue { get; set; }
    }
}