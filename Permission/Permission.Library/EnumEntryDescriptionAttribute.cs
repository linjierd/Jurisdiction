
namespace Permission.Library
{
    using System;
    /// <summary>
    /// Represents the additional information for entries of the enum type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumEntryDescriptionAttribute : Attribute
    {
        public EnumEntryDescriptionAttribute(string displayName)
            : this(displayName, true, 0)
        {
        }

        public EnumEntryDescriptionAttribute(string displayName, bool visible)
            : this(displayName, visible, String.Empty, 0)
        {
        }

        public EnumEntryDescriptionAttribute(string displayName, int sortOrder)
            : this(displayName, true, String.Empty, sortOrder)
        {
        }

        public EnumEntryDescriptionAttribute(string displayName, bool visible, int sortOrder)
            : this(displayName, visible, String.Empty, sortOrder)
        {
        }

        public EnumEntryDescriptionAttribute(string displayName, bool visible, string tag, int sortOrder)
        {
            this.displayName = displayName;
            this.visible = visible;
            this.tag = null;
            this.sortOrder = sortOrder;
        }

        string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        bool visible;
        public bool Visible
        {
            get
            {
                return visible;
            }
        }

        string tag;
        public string Tag
        {
            get
            {
                return tag;
            }
        }

        int sortOrder;
        public int SortOrder
        {
            get
            {
                return sortOrder;
            }
        }

    } // class EnumEntryDescriptionAttribute
}
