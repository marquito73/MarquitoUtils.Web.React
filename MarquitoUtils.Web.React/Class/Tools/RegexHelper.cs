using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarquitoUtils.Web.React.Class.Tools
{
    public class RegexHelper
    {
        private static string SpecialCharacters = "@$!%*?&";

        public static string GetDefaultRegex()
        {
            return "^.*$";
        }

        private static string GetQuantifierForRegex(bool isRequired, int maxLength)
        {
            int minLength;
            if (isRequired)
            {
                minLength = 1;
            }
            else
            {
                minLength = 0;
            }
            return GetQuantifierForRegex(minLength, maxLength);
        }

        private static string GetQuantifierForRegex(int minLength, int maxLength)
        {
            StringBuilder sbQuantifier = new StringBuilder();
            sbQuantifier.Append("{")
                    .Append(minLength)
                    .Append(",");
            if (maxLength >= 0)
            {
                sbQuantifier.Append(maxLength);
            }
            sbQuantifier.Append("}");
            return sbQuantifier.ToString();
        }

        private static string GetExactQuantifierForRegex(int length)
        {
            StringBuilder sbQuantifier = new StringBuilder();
            sbQuantifier.Append("{").Append(length).Append("}");
            return sbQuantifier.ToString();
        }

        public static string GetMailRegex(bool isRequired)
        {
            StringBuilder mailRegex = new StringBuilder();
            if (!isRequired)
            {
                mailRegex.Append("^$|"); // N'est pas obligatoire
            }
            mailRegex
                    .Append("^(")
                    // Une chaine de caractère acceptant uniquement les lettres (minuscules et majuscules), les chiffres, et les caractères
                    // "-", "_" et "." de maximum 123 caractères
                    .Append($@"[a-zA-Z0-9\-_\.]{GetQuantifierForRegex(true, 123)}")
                    // Une @
                    .Append($"@{GetExactQuantifierForRegex(1)}")
                    // Une chaine de caractère acceptant uniquement les lettres (minuscules et majuscules), les chiffres, et les caractères
                    // "-" et "." de maximum 123 caractères
                    .Append($@"[a-zA-Z0-9\-\.]{GetQuantifierForRegex(true, 123)}")
                    // Un .
                    .Append($@"\.{GetExactQuantifierForRegex(1)}")
                    // Une chaine de caractère acceptant uniquement les lettres (minuscules et majuscules) entre 2 et 6 caractères
                    .Append("[a-zA-Z]")
                    .Append($"[a-zA-Z]{GetQuantifierForRegex(2, 6)}")

                    .Append(")$");

            return mailRegex.ToString();
        }

        public static string GetPhoneRegex(bool isRequired)
        {
            // Un numéro de téléphone pouvant commencer par un code international (ex:+33)
            // Exemple de resultat du regex : "^(\+{1}[0-9]{1,11})$|^([0-9]{0,12})$"
            return GetPhoneRegex(isRequired, 128);
        }

        public static string GetPhoneRegex(bool isRequired, int maxLength)
        {
            // Un numéro de téléphone pouvant commencer par un code international (ex:+33)
            // Exemple de resultat du regex : "^(\+{1}[0-9]{1,11})$|^([0-9]{0,12})$"
            StringBuilder phoneRegex = new StringBuilder();
            phoneRegex.Append("^(").Append(@"\\+")
                .Append(GetExactQuantifierForRegex(1))
                .Append("[0-9]")
                .Append(GetQuantifierForRegex(true, maxLength))
                .Append(")$|^([0-9]").Append(GetQuantifierForRegex(isRequired, maxLength)).Append(")$");
            return phoneRegex.ToString();
        }

        public static string GetIntegerRegex(bool isRequired, int maxLength)
        {
            StringBuilder integerRegex = new StringBuilder();
            integerRegex.Append("^\\d")
                .Append(GetQuantifierForRegex(isRequired, maxLength))
                .Append("$");
            return integerRegex.ToString();
        }

        public static string GetTextLimitRegex(bool isRequired)
        {
            return GetTextLimitRegex(isRequired, -1);
        }

        public static string GetTextLimitRegex(bool isRequired, int maxLength)
        {
            StringBuilder codeRegex = new StringBuilder();
            codeRegex.Append("\\A[\\w\\W]")
                    .Append(GetQuantifierForRegex(isRequired, maxLength))
                    .Append("\\z");
            return codeRegex.ToString();
        }


        // TODO Revoir le regex et le faire marcher
        public static string GetAmountRegex(bool isRequired, int mainLength, int decimalsLength)
        {
            //^[0-9]{1,4}(\.[0-9]{1,2})?$|^$

            /*StringBuilder amountRegex = new StringBuilder();
            amountRegex.Append("^[0-9]")
                .Append(GetQuantifierForRegex(true, mainLength))
                .Append("+").Append(@"\\.").Append(GetQuantifierForRegex(false, 1))
                .Append("(?:")
                .Append("[0-9]").Append(GetQuantifierForRegex(true, decimalsLength)).Append(")?$");
            if (!isRequired)
            {
                amountRegex.Append("|^$");
            }*/

            StringBuilder amountRegex = new StringBuilder();

            amountRegex.Append("^[0-9]")
                .Append(GetQuantifierForRegex(true, mainLength))
                .Append("(").Append(@"\\.").Append("[0-9]")
                .Append(GetQuantifierForRegex(true, decimalsLength))
                .Append(")?$|^$");

            return amountRegex.ToString();
        }

        public static string GetPasswordRegex(int minLength, bool requireOneUpperCase,
            bool requireOneLowerCase, bool requireOneNumber, bool requireOneSpecialCharacter)
        {
            return GetPasswordRegex(minLength, -1, requireOneUpperCase, requireOneLowerCase, requireOneNumber,
                requireOneSpecialCharacter);
        }

        public static string GetPasswordRegex(int minLength, int maxLength, bool requireOneUpperCase, 
            bool requireOneLowerCase, bool requireOneNumber, bool requireOneSpecialCharacter)
        {
            StringBuilder passwordRegex = new StringBuilder();

            //^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$
            passwordRegex.Append("^");
            if (!requireOneUpperCase && !requireOneLowerCase && !requireOneNumber && !requireOneSpecialCharacter)
            {
                passwordRegex.Append("(?=.*[A-Za-z])");
            }
            else
            {
                if (requireOneUpperCase)
                {
                    passwordRegex.Append("(?=.*[A-Z])");
                }
                if (requireOneLowerCase)
                {
                    passwordRegex.Append("(?=.*[a-z])");
                }
                if (requireOneNumber)
                {
                    passwordRegex.Append("(?=.*").Append(@"\\d)");
                }
                if (requireOneSpecialCharacter)
                {
                    passwordRegex.Append("(?=.*[").Append(SpecialCharacters).Append("])");
                }
            }
            passwordRegex.Append("[")
                .Append("A-Z")
                .Append("a-z")
                .Append(@"\\d")
                .Append(SpecialCharacters)
                .Append("]");

            passwordRegex.Append(GetQuantifierForRegex(minLength, maxLength));
            passwordRegex.Append("$|^$");

            return passwordRegex.ToString();
        }
    }
}
