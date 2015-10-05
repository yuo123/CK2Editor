using System;
namespace CK2Editor
{
    public abstract class Entry
    {
        /// <summary>
        /// A user friendly name, supplied by the format file
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// The name used by the save file
        /// </summary>
        public string InternalName { get; set; }
        /// <summary>
        /// A link to a related section, supplied by the format file
        /// </summary>
        public string Link { get; set; }
    }
}
