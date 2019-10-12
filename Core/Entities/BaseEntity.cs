using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Entities
{
    /// <summary>
    /// Signifies which class is an entity
    /// </summary>
    /// <remarks>
    /// An entity varies from a value object because is has an Id  property
    /// This makes it unique even though the combination of its properties might not be
    /// </remarks>
    public class BaseEntity
    {
        public int Id { get; private set; }
    }
}
