using MarquitoUtils.Main.Class.Enums;

namespace MarquitoUtils.Web.React.Class.Enums
{
    public class EnumTextBoxType : EnumClass
    {
        public enum enumTextBoxType
        {
            TEXT,
            INCREMENT,
            CURRENCY,
            QUANTITY,
            PASSWORD,
            EMAIL,
            PHONE_NUMBER
        }

        public string PlaceHolder { get; private set; } = "";
        public EnumInputType InputType { get; private set; } = EnumInputType.Text;
        public enumTextBoxType InputTypeValue { get; private set; }

        private EnumTextBoxType(string placeHolder, EnumInputType inputType, enumTextBoxType inputTypeValue)
        {
            this.PlaceHolder = placeHolder;
            this.InputType = inputType;
            this.InputTypeValue = inputTypeValue;
        }

        public static EnumTextBoxType TEXT
        {
            get
            {
                return new EnumTextBoxType("", EnumInputType.Text, enumTextBoxType.TEXT);
            }
        }

        public static EnumTextBoxType INCREMENT
        {
            get
            {
                return new EnumTextBoxType("0", EnumInputType.Number, enumTextBoxType.INCREMENT);
            }
        }

        public static EnumTextBoxType CURRENCY
        {
            get
            {
                return new EnumTextBoxType("0.00", EnumInputType.Currency, enumTextBoxType.CURRENCY);
            }
        }

        public static EnumTextBoxType QUANTITY
        {
            get
            {
                return new EnumTextBoxType("0", EnumInputType.Number, enumTextBoxType.QUANTITY);
            }
        }

        public static EnumTextBoxType PASSWORD
        {
            get
            {
                return new EnumTextBoxType("********", EnumInputType.Password, enumTextBoxType.PASSWORD);
            }
        }

        public static EnumTextBoxType EMAIL
        {
            get
            {
                return new EnumTextBoxType("example@domain.com", EnumInputType.Email, enumTextBoxType.EMAIL);
            }
        }

        public static EnumTextBoxType PHONE_NUMBER
        {
            get
            {
                return new EnumTextBoxType("0102030405", EnumInputType.Text, enumTextBoxType.PHONE_NUMBER);
            }
        }

        public static EnumTextBoxType URL
        {
            get
            {
                return new EnumTextBoxType("http://www.exemple.com", EnumInputType.Text, enumTextBoxType.TEXT);
            }
        }
    }
}