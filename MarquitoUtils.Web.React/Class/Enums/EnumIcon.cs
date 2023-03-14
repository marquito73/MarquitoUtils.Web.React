using MarquitoUtils.Main.Class.Enums;
using MarquitoUtils.Main.Class.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumIconAttr : EnumClass
    {
        public string IconCss { get; private set; }

        public EnumIconAttr(string iconCss)
        {
            this.IconCss = iconCss;
        }
    }

    public static class EnumIcons
    {
        public static EnumIconAttr Attr(this EnumIcon colorType)
        {
            return EnumUtils.GetAttr<EnumIconAttr, EnumIcon>(colorType);
        }
    }

    [DataContract]
    public enum EnumIcon
    {
        [EnumMember]
        [EnumIconAttr("")] None,
        [EnumMember]
        [EnumIconAttr("icon-eye")] ShowContent,
        [EnumMember]
        [EnumIconAttr("icon-eye-blocked")] HideContent,
        [EnumMember]
        [EnumIconAttr("icon-magic-wand")] MagicWand,
        [EnumMember]
        [EnumIconAttr("icon-arrow-up-left")] ArrowUpLeft,
        [EnumMember]
        [EnumIconAttr("icon-arrow-up")] ArrowUp,
        [EnumMember]
        [EnumIconAttr("icon-arrow-up-right")] ArrowUpRight,
        [EnumMember]
        [EnumIconAttr("icon-arrow-right")] ArrowRight,
        [EnumMember]
        [EnumIconAttr("icon-arrow-down-right")] ArrowDownRight,
        [EnumMember]
        [EnumIconAttr("icon-arrow-down")] ArrowDown,
        [EnumMember]
        [EnumIconAttr("icon-arrow-down-left")] ArrowDownLeft,
        [EnumMember]
        [EnumIconAttr("icon-arrow-left")] ArrowLeft,
        [EnumMember]
        [EnumIconAttr("icon-plus")] Plus,
        [EnumMember]
        [EnumIconAttr("icon-minus")] Minus,
        [EnumMember]
        [EnumIconAttr("icon-backward")] PreviousOne,
        [EnumMember]
        [EnumIconAttr("icon-backward2")] PreviousTwo,
        [EnumMember]
        [EnumIconAttr("icon-forward")] ForWardOne,
        [EnumMember]
        [EnumIconAttr("icon-forward2")] ForWardTwo,
        [EnumMember]
        [EnumIconAttr("icon-forward3")] NextTwo,
        [EnumMember]
        [EnumIconAttr("icon-radio-checked")] RadioChecked,
        [EnumMember]
        [EnumIconAttr("icon-radio-unchecked")] RadioUnchecked,
        [EnumMember]
        [EnumIconAttr("icon-bold")] Bold,
        [EnumMember]
        [EnumIconAttr("icon-underline")] Underline,
        [EnumMember]
        [EnumIconAttr("icon-italic")] Italic,
        [EnumMember]
        [EnumIconAttr("icon-strikethrough")] Strikethrough,
        [EnumMember]
        [EnumIconAttr("icon-menu")] Menu,
        [EnumMember]
        [EnumIconAttr("icon-filter")] Filter,
        [EnumMember]
        [EnumIconAttr("icon-spinner")] Loading1,
        [EnumMember]
        [EnumIconAttr("icon-spinner2")] Loading2,
        [EnumMember]
        [EnumIconAttr("icon-spinner3")] Loading3,
        [EnumMember]
        [EnumIconAttr("icon-spinner4")] Loading4,
        [EnumMember]
        [EnumIconAttr("icon-spinner5")] Loading5,
        [EnumMember]
        [EnumIconAttr("icon-spinner6")] Loading6,
        [EnumMember]
        [EnumIconAttr("icon-spinner7")] Loading7,
        [EnumMember]
        [EnumIconAttr("icon-spinner8")] Loading8,
        [EnumMember]
        [EnumIconAttr("icon-spinner9")] Loading9,
        [EnumMember]
        [EnumIconAttr("icon-spinner10")] Loading10,
        [EnumMember]
        [EnumIconAttr("icon-spinner11")] Refresh,
        [EnumMember]
        [EnumIconAttr("icon-search")] Search,
        [EnumMember]
        [EnumIconAttr("icon-download2")] Download,
        [EnumMember]
        [EnumIconAttr("icon-upload3")] Upload,
        [EnumMember]
        [EnumIconAttr("icon-home")] Home,
        [EnumMember]
        [EnumIconAttr("icon-mail1")] Mail1,
        [EnumMember]
        [EnumIconAttr("icon-mail2")] Mail2,
        [EnumMember]
        [EnumIconAttr("icon-mail3")] Mail3,
        [EnumMember]
        [EnumIconAttr("icon-mail4")] Mail4
    }
}
