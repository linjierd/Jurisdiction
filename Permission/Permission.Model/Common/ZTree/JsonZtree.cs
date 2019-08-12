using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permission.Model.Common.ZTree
{
    public class JsonZtree
    {
        public string id
        {
            get;
            set;
        }
        public string pId
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }

        public string open { get; set; }

        public string Checked { get; set; }

        public bool isParent { get; set; }
    }
}
