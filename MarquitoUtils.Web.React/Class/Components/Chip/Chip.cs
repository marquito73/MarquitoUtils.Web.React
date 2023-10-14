using MarquitoUtils.Web.React.Class.Components.TextArea;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Components.Chip
{
    /// <summary>
    /// Chip component, useful for display short data
    /// </summary>
    public class Chip : Label
    {
        /// <summary>
        /// Background color
        /// </summary>
        [JsonRequired]
        public string ChipColor { get; set; } = "white";
        /// <summary>
        /// Text showed when then user hover the chip
        /// </summary>
        [JsonRequired]
        public string TooltipText { get; set; } = "";
        /// <summary>
        /// Display a border for this chip
        /// </summary>
        [JsonRequired]
        public bool HasBorder { get; set; } = false;

        /// <summary>
        /// Chip component, useful for display short data
        /// </summary>
        /// <param name="id">Id of the chip</param>
        /// <param name="text">Data to display on this chip</param>
        /// <param name="chipColor">The background chip color</param>
        public Chip(string id, string text, string chipColor) : base(id, text)
        {
            this.ChipColor = chipColor;
        }
    }
}
