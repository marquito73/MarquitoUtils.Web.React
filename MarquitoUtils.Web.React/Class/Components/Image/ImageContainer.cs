using MarquitoUtils.Main.Class.Entities.Image;
using MarquitoUtils.Main.Class.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Image
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
