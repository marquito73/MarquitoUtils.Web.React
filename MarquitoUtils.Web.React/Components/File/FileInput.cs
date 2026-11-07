using MarquitoUtils.Web.React.Communication;
using MarquitoUtils.Web.React.Components;
using MarquitoUtils.Web.React.Enums.Action;
using MarquitoUtils.Web.React.Url;

namespace MarquitoUtils.Web.React.Components.File
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
