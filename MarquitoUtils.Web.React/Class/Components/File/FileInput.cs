using MarquitoUtils.Web.React.Class.Communication;
using MarquitoUtils.Web.React.Class.Enums.Action;
using MarquitoUtils.Web.React.Class.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.File
{
    public class FileInput<TActionUrl> : Component
        where TActionUrl : WebAction
    {
        public string TextToDisplay { get; set; } = "Drag and drop a file here";
        public string Color { get; set; } = "deepskyblue";
        public short TextToDisplaySize { get; set; } = 20;
        public List<string> AuthorizedFileExtensions { get; private set; } = new List<string>();
        public WebActionUrl<TActionUrl> UploadURL { get; private set; } = new WebActionUrl<TActionUrl>(EnumAction.UploadFile);
        public FileInput(string id) : base(id)
        {
            this.ReactComponentName = "FileInput";
        }
    }
}
