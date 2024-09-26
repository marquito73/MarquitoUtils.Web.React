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
        public enumInputType InputType { get; private set; } = enumInputType.Text;
        public enumTextBoxType InputTypeValue { get; private set; }

        private EnumTextBoxType(string placeHolder, enumInputType inputType, enumTextBoxType inputTypeValue)
        {
            this.PlaceHolder = placeHolder;
            this.InputType = inputType;
            this.InputTypeValue = inputTypeValue;
        }

        public static EnumTextBoxType TEXT
        {
            get
            {
                return new EnumTextBoxType("", enumInputType.Text, enumTextBoxType.TEXT);
            }
        }

        public static EnumTextBoxType INCREMENT
        {
            get
            {
                return new EnumTextBoxType("0", enumInputType.Number, enumTextBoxType.INCREMENT);
            }
        }

        public static EnumTextBoxType CURRENCY
        {
            get
            {
                return new EnumTextBoxType("0.00", enumInputType.Currency, enumTextBoxType.CURRENCY);
            }
        }

        public static EnumTextBoxType QUANTITY
        {
            get
            {
                return new EnumTextBoxType("0", enumInputType.Number, enumTextBoxType.QUANTITY);
            }
        }

        public static EnumTextBoxType PASSWORD
        {
            get
            {
                return new EnumTextBoxType("********", enumInputType.Password, enumTextBoxType.PASSWORD);
            }
        }

        public static EnumTextBoxType EMAIL
        {
            get
            {
                return new EnumTextBoxType("example@domain.com", enumInputType.Email, enumTextBoxType.EMAIL);
            }
        }

        public static EnumTextBoxType PHONE_NUMBER
        {
            get
            {
                return new EnumTextBoxType("0102030405", enumInputType.Text, enumTextBoxType.PHONE_NUMBER);
            }
        }

        public static EnumTextBoxType URL
        {
            get
            {
                return new EnumTextBoxType("http://www.exemple.com", enumInputType.Text, enumTextBoxType.TEXT);
            }
        }
    }
}