using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.WebApp.CustomControls
{
    /// <summary>
    /// Specifies the type of the TextBox control (Alphabet, Numeric, Decimal).
    /// </summary>
    /// <createdby>Amit Kumar Chandra</createdby>
    /// <createddate>05/12/2012</createddate>
    public enum TextBoxType
    {
        /// <summary>
        /// TextBox allows only alphabets.
        /// </summary>
        Alphabet = 1,
        /// <summary>
        /// TextBox allows only number.
        /// </summary>
        Numeric = 2,
        /// <summary>
        /// TextBox allows decimal number.
        /// </summary>
        Decimal = 3
    };

    /// <summary>
    /// Represents a custom TextBox control.
    /// </summary>
    /// <createdby>Amit Kumar Chandra</createdby>
    /// <createddate>05/12/2012</createddate>
    public class CustomTextBox : TextBox
    {
        /// <summary>
        /// Gets or sets the type of the TextBox control.
        /// </summary>
        /// <value>The type.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>05/12/2012</createddate>
        [DefaultValue(typeof(TextBoxType), "Numeric")]
        [Browsable(true)]
        public TextBoxType Type { get; set; }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>05/12/2012</createddate>
        [DefaultValue(typeof(int), "0")]
        [Browsable(true)]
        public int Precision { get; set; }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>05/12/2012</createddate>
        [DefaultValue(typeof(int), "0")]
        [Browsable(true)]
        public int Scale { get; set; }

        /// <summary>
        /// Renders the <see cref="OCTMPWeb.CustomControls.CustomTextBox"/> control to the specified <see cref="System.Web.UI.HtmlTextWriter"/> object.
        /// </summary>
        /// <param name="writer">The <see cref="System.Web.UI.HtmlTextWriter"/> that receives the rendered output.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>05/12/2012</createddate>
        protected override void Render(HtmlTextWriter writer)
        {
            string type = string.Empty;

            switch (Type)
            {
                case TextBoxType.Alphabet:
                    type = "alphabet";
                    break;
                case TextBoxType.Decimal:
                    type = "decimal";
                    break;
                case TextBoxType.Numeric:
                    type = "numeric";
                    break;
                default:
                    type = "numeric";
                    break;
            }

            this.Attributes.Add("onpaste", "return false");
            this.Attributes.Add("ondragover", "_False(event)");
            //this.Attributes.Add("ondragenter", "_False(event)");
            this.Attributes.Add("ondrop", "return _StopDrop(event,this);");
            this.Attributes.Add("oncontextmenu", "return false;");
            this.Attributes.Add("onkeypress", "return _Validate(event,'" + type + "'," + Scale.ToString() + "," + Precision.ToString() + ",this)");

            if (type == "decimal")
            {
                this.Attributes.Add("onkeydown", "_SetText(this)");
                this.Attributes.Add("onkeyup", "_CheckText(this," + Precision.ToString() + ")");
                this.Attributes.Add("onblur", "return _FormatDecimal(this,'" + type + "'," + Scale.ToString() + "," + Precision.ToString() + ")");
            }

            base.Render(writer);
        }
    }
}
