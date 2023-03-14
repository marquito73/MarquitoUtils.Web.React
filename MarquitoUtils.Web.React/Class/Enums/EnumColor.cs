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
    public class EnumColorAttr : EnumClass
    {
        public string Color { get; private set; } = "";
        public string CssColor { get; private set; } = "";
        public ushort Red { get; private set; } = 0;
        public ushort Green { get; private set; } = 0;
        public ushort Blue { get; private set; } = 0;
        public double Opacity { get; private set; } = 1;

        public EnumColorAttr(string cssColor)
        {
            this.CssColor = cssColor;
        }

        public EnumColorAttr(string cssColor, string color)
        {
            this.CssColor = cssColor;
            this.Color = color;

            Color cColor = this.GetColor();
            this.Red = cColor.R;
            this.Green = cColor.G;
            this.Blue = cColor.B;
        }

        public EnumColorAttr(string cssColor, string color, double opacity) : this(cssColor, color)
        {
            this.Opacity = opacity;
        }

        public EnumColorAttr(string cssColor, ushort red, ushort green, ushort blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public EnumColorAttr(string cssColor, ushort red, ushort green, ushort blue, double opacity)
            : this(cssColor, red, green, blue)
        {
            this.Opacity = opacity;
        }

        public string GetHexColor()
        {
            return "#" + this.Color;
        }

        public string GetCssColor()
        {
            return "color_" + this.CssColor;
        }

        public string GetCssHoverColor()
        {
            return "hover_" + this.GetCssColor();
        }

        public string GetCssBackgroundColor()
        {
            return "backcolor_" + this.CssColor;
        }

        public string GetCssHoverBackgroundColor()
        {
            return "hover_" + this.GetCssBackgroundColor();
        }

        public string GetCssBorderColor()
        {
            return "bordercolor_" + this.CssColor;
        }

        public string GetCssHoverBorderColor()
        {
            return "hover_" + this.GetCssBorderColor();
        }

        public string GetCssRgbaColor()
        {
            StringBuilder sbRgba = new StringBuilder();

            Color color = this.GetColor();

            sbRgba.Append("rgba(").Append(Utils.GetAsString(color.R)).Append(",")
                .Append(Utils.GetAsString(color.G)).Append(",")
                .Append(Utils.GetAsString(color.B)).Append(",")
                .Append(Utils.GetAsString(this.Opacity)).Append(")");

            return sbRgba.ToString();
        }

        public Color GetColor()
        {
            return ColorTranslator.FromHtml(this.GetHexColor());
        }
    }

    public static class EnumColors
    {
        public static EnumColorAttr Attr(this EnumColor colorType)
        {
            return EnumUtils.GetAttr<EnumColorAttr, EnumColor>(colorType);
        }
        public static string GetHexColor(this EnumColor colorType)
        {
            return Attr(colorType).GetHexColor();
        }

        public static string GetCssColor(this EnumColor colorType)
        {
            return Attr(colorType).GetCssColor();
        }

        public static string GetCssHoverColor(this EnumColor colorType)
        {
            return Attr(colorType).GetCssHoverColor();
        }

        public static string GetCssBackgroundColor(this EnumColor colorType)
        {
            return Attr(colorType).GetCssBackgroundColor();
        }

        public static string GetCssHoverBackgroundColor(this EnumColor colorType)
        {
            return Attr(colorType).GetCssHoverBackgroundColor();
        }

        public static string GetCssBorderColor(this EnumColor colorType)
        {
            return Attr(colorType).GetCssBorderColor();
        }

        public static string GetCssHoverBorderColor(this EnumColor colorType)
        {
            return Attr(colorType).GetCssHoverBorderColor();
        }

        public static string GetCssRgbaColor(this EnumColor colorType)
        {
            return Attr(colorType).GetCssRgbaColor();
        }

        public static Color GetColor(this EnumColor colorType)
        {
            return Attr(colorType).GetColor();
        }
    }

    [DataContract]
    public enum EnumColor
    {
        [EnumMember]
        [EnumColorAttr("None", "FFFFFF00", 0)] None,
        [EnumMember]
        [EnumColorAttr("Black", "000000")] Black,
        [EnumMember]
        [EnumColorAttr("White", "FFFFFF")] White,
        [EnumMember]
        [EnumColorAttr("Grey", "C9C9C9")] Grey,

        [EnumMember]
        [EnumColorAttr("Yellow", "ffd000")] Yellow,
        [EnumMember]
        [EnumColorAttr("Yellow-Green", "d9ff00")] YellowGreen,
        [EnumMember]
        [EnumColorAttr("Green", "00ff00")] Green,
        [EnumMember]
        [EnumColorAttr("Blue-Green", "00ff80")] BlueGreen,
        [EnumMember]
        [EnumColorAttr("Cyan", "00ffd0")] Cyan,
        [EnumMember]
        [EnumColorAttr("Blue", "0000ff")] Blue,
        [EnumMember]
        [EnumColorAttr("Blue-Violet", "5d00ff")] BlueViolet,
        [EnumMember]
        [EnumColorAttr("Violet", "8c00ff")] Violet,
        [EnumMember]
        [EnumColorAttr("Pink", "ff00ea")] Pink,
        [EnumMember]
        [EnumColorAttr("Red-Violet", "ff0059")] RedViolet,
        [EnumMember]
        [EnumColorAttr("Red", "ff0000")] Red,
        [EnumMember]
        [EnumColorAttr("Red-Orange", "ff4000")] RedOrange,
        [EnumMember]
        [EnumColorAttr("Orange", "ff7b00")] Orange,
        [EnumMember]
        [EnumColorAttr("Yellow-Orange", "ffa200")] YellowOrange,

        [EnumMember]
        [EnumColorAttr("Canary", "FFFD82")] Canary,
        [EnumMember]
        [EnumColorAttr("LightSalmon", "FF9B71")] LightSalmon,
        [EnumMember]
        [EnumColorAttr("SizzlingRed", "E84855")] SizzlingRed,
        [EnumMember]
        [EnumColorAttr("BrowSugar", "B56B45")] BrowSugar,
        [EnumMember]
        [EnumColorAttr("SpaceCadet", "2B3A67")] SpaceCadet,
        [EnumMember]
        [EnumColorAttr("GreenApple", "ABDD93")] GreenApple,


        [EnumMember]
        [EnumColorAttr("Pastel-Light-Blue", "A6C7EA")] PastelLightBlue,
        [EnumMember]
        [EnumColorAttr("Pastel-Blue", "89BADE")] PastelBlue,
        [EnumMember]
        [EnumColorAttr("Pastel-Dark-Blue", "699AAC")] PastelDarkBlue,
        [EnumMember]
        [EnumColorAttr("Pastel-Light-Green", "A9DAD1")] PastelLightGreen,
        [EnumMember]
        [EnumColorAttr("Pastel-Green", "90CFB5")] PastelGreen,
        [EnumMember]
        [EnumColorAttr("Pastel-Dark-Green", "88BBAA")] PastelDarkGreen,
        [EnumMember]
        [EnumColorAttr("Pastel-Apple-Green", "BFDAB5")] PastelAppleGreen
    }
}
