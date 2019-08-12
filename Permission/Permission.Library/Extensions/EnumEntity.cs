using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permission.Library.Extensions
{
    public class EnumEntity
    {
        public string Name
        {
            get;
            set;
        }
        List<EnumItem> items;

        public List<EnumItem> Items
        {
            get 
            {
                if (this.items == null)
                {
                    this.items = new List<EnumItem>();
                }
                return items; 
            }
            set { items = value; }
        }

    }
    public class EnumItem
    {
        public string EnumField { get; set; }
        public int EnumValue { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
    }
}
