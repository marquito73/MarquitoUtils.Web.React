using MarquitoUtils.Web.React.Class.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Select
{
    public class CheckListBox : ContentBox
    {
        public CheckListBox(string id, string selectedValue) : base(id, selectedValue, enumInputType.Check)
        {
        }
    }
}
