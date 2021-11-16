using System;
using System.Text;

namespace Sistema.Api.dll.Src
{
    /// <summary>
    /// Extensão do método StringBuilder
    /// </summary>
    public static class StringBuilderExtensionMethods
   {
        /// </code>
        /// </example>
        /// <param name="this"></param>
        /// <param name="condition"></param>
        /// <param name="strIf"></param>
        /// <returns></returns>
        public static StringBuilder AppendLineIf(this StringBuilder @this, bool condition, string strIf)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (condition)
            {
                @this.AppendLine(strIf);
            }

            return @this;
        }

        /// <param name="this"></param>
        /// <param name="condition"></param>
        /// <param name="strIf"></param>
        /// <param name="strElse"></param>
        /// <returns></returns>
        public static StringBuilder AppendLineIfElse(this StringBuilder @this, bool condition, string strIf, string strElse)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            @this.AppendLine(condition ? strIf : strElse);

            return @this;
        }

    }
}