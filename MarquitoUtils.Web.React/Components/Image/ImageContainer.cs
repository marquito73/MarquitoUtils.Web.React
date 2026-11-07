using MarquitoUtils.Main.Common.Tools;
using MarquitoUtils.Main.Files.Image;
using MarquitoUtils.Web.React.Components;

namespace MarquitoUtils.Web.React.Components.Image
{
    public class ImageContainer : Component
    {
        public string ImageContent { get; set; }

        public ImageContainer(string id, string imageContent) : base(id)
        {
            this.ImageContent = imageContent;
        }

        public ImageContainer(string id, ImageData imageContent) : base(id)
        {
            this.ImageContent = $"data:image;base64,{Utils.GetAsString(imageContent)}";
        }
    }
}
