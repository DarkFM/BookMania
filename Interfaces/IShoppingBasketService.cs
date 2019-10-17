using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Interfaces
{
    public interface IShoppingBasketService
    {
        /// <summary>
        /// Gets all basket items for the user
        /// </summary>
        /// <param name="userId">The logged in user or the "guest" user if the userId is null</param>
        /// <returns>The Viewmodel used for displaying the result</returns>
        Task<BasketViewModel> GetBasketItems(int? userId = null);
        /// <summary>
        /// Creates a new basket for the logged in user OR Creates a new basket for the guest user with an Id of "guest".
        /// This is a no-op if a basket already exists for the user.
        /// </summary>
        /// <param name="userId">The user id if a user is logged in.</param>
        Task CreateBasketForUser(int? userId = null);
        /// <summary>
        /// Removes an item from the basket for the current user
        /// </summary>
        /// <param name="basketItem">The basket item to remove</param>
        /// <param name="userId">The logged in user or the "guest" user if the userId is null</param>
        Task RemoveBasketItem(BasketItem basketItem, int? userId = null);

        /// <summary>
        /// Updates the basket items for the current user
        /// </summary>
        /// <param name="userId">The logged in user or the "guest" user if the userId is null</param>
        /// <param name="basketItems">The updated basket items</param>
        /// <returns>The Viewmodel used for displaying the result</returns>
        Task<BasketViewModel> UpdateBasketItems(int? userId = null, params BasketItem[] basketItems);

        /// <summary>
        /// Adds a new items to the basket for the current user
        /// </summary>
        /// <param name="basketItem"></param>
        /// <param name="userId">The logged in user or the "guest" user if the userId is null</param>
        /// <returns>The Viewmodel used for displaying the result</returns>
        Task<BasketViewModel> AddBasketItem(BasketItem basketItem, int? userId = null);
    }
}
