
namespace Marsman.UmbracoCodeFirst
{
    public enum InitialisationMode
    {
        /// <summary>
        /// The types in the database will be updated to reflect the
        /// type definitions specified
        /// </summary>
        Sync = 0,

        /// <summary>
        /// If the types in the database do not match the type definitions specified
        /// an exception will be thrown, and the database will not be modified.
        /// </summary>
        Ensure = 1,

        /// <summary>
        /// The types definitions are used as specified without checking if the database
        /// matches the definitions. This is useful in load balanced scenarios where one
        /// master instance will initialise the database whilst n slave instances will
        /// simply assume that the database will be synchronised to the type definitions
        /// before traffic is routed to them. It is the developer's responsibility to ensure
        /// that this happens. If passive instances are used before the database types are
        /// synchronised spurious behaviour may occur.
        /// </summary>
        Passive = 2
    }
}