using FSException;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FSFormLibrary
{
    /// <summary>
    /// Biblioteca de funciones para la conversión de aplicaciones VB.
    /// </summary>
    public class Vb
    {
        /// <summary>
        /// Salto de línea
        /// </summary>
        public static string vbCrLf = Environment.NewLine;

        /// <summary>
        /// Tipo para utilizar los mensajes de MessegeBox.Show.
        /// </summary>
        public enum TypeMsg
        {
            /// <summary>
            /// Informativo
            /// </summary>
            Information,
            /// <summary>
            /// Critico
            /// </summary>
            Critical,
            /// <summary>
            /// Pregunta
            /// </summary>
            Question,
            /// <summary>
            /// Exclamación
            /// </summary>
            Exclamation,
            /// <summary>
            /// Error
            /// </summary>
            Error
        }

        /// <summary>
        /// Tipo para los botones a mostrar en los mensajes de MessegeBox.Show.
        /// </summary>
        public enum TypeButton
        {
            /// <summary>
            /// Si y No
            /// </summary>
            YesNo,
            /// <summary>
            /// Si,no y cancel
            /// </summary>
            YesNoCancel,
            /// <summary>
            /// Ok
            /// </summary>
            Ok,
            /// <summary>
            /// Ok y cancel
            /// </summary>
            OkCancel
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="text"></param>
        public static DialogResult MsgBox(string text)
        {
            return MsgBox(text, TypeMsg.Information);
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="typeMsg"></param>
        public static DialogResult MsgBox(string text, TypeMsg typeMsg)
        {
            return MsgBox(text, typeMsg, "");
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="typeMsg"></param>
        /// <param name="caption"></param>
        public static DialogResult MsgBox(string text, TypeMsg typeMsg, string caption)
        {
            return MsgBox(text, typeMsg, caption, TypeButton.Ok);
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="typeMsg"></param>
        /// <param name="caption"></param>
        /// <param name="typeButton"></param>
        public static DialogResult MsgBox(string text, TypeMsg typeMsg, string caption, TypeButton typeButton)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBoxButtons button = MessageBoxButtons.OK;

            switch (typeMsg)
            {
                case TypeMsg.Information:
                    icon = MessageBoxIcon.Information;
                    break;
                case TypeMsg.Error:
                case TypeMsg.Critical:
                    icon = MessageBoxIcon.Error;
                    break;
                case TypeMsg.Exclamation:
                    icon = MessageBoxIcon.Exclamation;
                    break;
                case TypeMsg.Question:
                    icon = MessageBoxIcon.Question;
                    break;
            }

            switch(typeButton)
            {
                case TypeButton.Ok:
                    button = MessageBoxButtons.OK;
                    break;
                case TypeButton.YesNo:
                    button = MessageBoxButtons.YesNo;
                    break;
                case TypeButton.YesNoCancel: 
                    button = MessageBoxButtons.YesNoCancel;
                    break;
                case TypeButton.OkCancel:
                    button = MessageBoxButtons.OKCancel;
                    break;
            }

            return MessageBox.Show(text, caption, button , icon);
        }

        /// <summary>
        /// Realiza un pregunta y devuelve el resultado escrito.
        /// </summary>
        /// <param name="question"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InputBox(string question, string title = "")
        {
            throw new ExceptionUtil("Función no implementada. Utiliza FSFormControls.InputBox().");
        }

        /// <summary>
        /// Devuelve true si obj1 != obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool IsDifferent(object obj1, object obj2)
        {
            if (obj1 != obj2)
                return true;
            return false;
        }

        /// <summary>
        /// Devuelve true si obj1 == obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool IsEqual(object obj1, object obj2)
        {
            if (Convert.ToDecimal(obj1) == Convert.ToDecimal(obj2))
                return true;
            return false;
        }

        /// <summary>
        /// Concatena obj1 + obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static string Concatenate(object obj1, object obj2)
        {
            return obj1.ToString() + obj2.ToString();
        }

        /// <summary>
        /// Multiplica obj1 * obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static decimal Multiply(object obj1, object obj2)
        {
            return Convert.ToDecimal(obj1) * Convert.ToDecimal(obj2);
        }

        /// <summary>
        /// Divide obj1 / obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static decimal Divide(object obj1, object obj2)
        {
            return Convert.ToDecimal(obj1) / Convert.ToDecimal(obj2);
        }

        /// <summary>
        /// Devuelve true si la cadena es numerica.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(String s)
        {
            Boolean value = true;
            if (s == String.Empty || s == null)
                value = false;
            else
            {
                foreach (Char c in s.ToCharArray())
                    if (c != ',' && c != '.')
                        value = value && Char.IsDigit(c);
            }
            return value;
        }

        /// <summary>
        /// Devuelve el objeto como cadena.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string Value(object valor)
        {
            string value = "";
            if (valor != null)
                value = valor.ToString();

            return value;
        }

        /// <summary>
        /// Devuelve el objeto como entero.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static int ValueInt(object valor)
        {
            int value = 0;
            if (valor != null)
            {
                if (valor.ToString() != "")
                    value = Convert.ToInt32(valor);
            }
            return value;
        }

        /// <summary>
        /// Logitud de una cadena
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Len(string str)
        {
            return str.Length;
        }

        /// <summary>
        /// Devuelve len carácteres por la izquierda
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(string str, int len)
        {
            return str.Substring(0, len);
        }

        /// <summary>
        /// Devuelve len carácteres por la derecha
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(string str, int len)
        {
            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// Devuelve los len carácteres comenzando desde start.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Mid(string str, int start, int len)
        {
            return str.Substring(start, len);
        }

        /// <summary>
        /// Evalua la expresión y devuel el objeto depediendo del resultado.
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        public static object IIf(bool expr, object valueIfTrue, object valueIfFalse)
        {
            return expr ? valueIfTrue : valueIfFalse;
        }

        /// <summary>
        /// Devuelve los len carácteres comenzando desde la posición 0.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Mid(string str, int len)
        {
            return str.Substring(0, len);
        }

        /// <summary>
        /// Devuelve -1 si la cadena data no existe en str.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int InStr(string str, string data)
        {
            return str.IndexOf(data);
        }

        /// <summary>
        /// Devuelve -1 si la cadena data no existe en str, comenzando desde start.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="str"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int InStr(int start, string str, string data)
        {
            return str.IndexOf(data, start);
        }

        /// <summary>
        /// Devuelve el character de un valor. Ejemplo: Chr(65) = 'A'.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char Chr(int value)
        {
            return (char)value;
        }

        /// <summary>
        /// Devuelvel el código ASCII de un carácter. Ejemplo Asc('A') = 65
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int Asc(char c)
        {
            return (int)c;
        }

        /// <summary>
        /// Devuelvel el código ASCII del primer carácter de una cadena. Ejemplo Asc("AITA") = 65
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int Asc(string s)
        {
            return (int)s.ToCharArray()[0];
        }

        /// <summary>
        /// Convierte a mayúsculas una cadena.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UCase(string str)
        {
            return str.ToUpper();
        }

        /// <summary>
        /// Convierte a minúsculas una cadena.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LCase(string str)
        {
            return str.ToLower();
        }

        /// <summary>
        /// Devuelve len caráteres en blanco.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Space(int len)
        {
            string str = "";
            return str.PadLeft(len, ' ');
        }

        /// <summary>
        /// Elimina los espacion por delante y detrás de una cadena.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Trim(string str)
        {
            return str.Trim();
        }

        /// <summary>
        /// ??????
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string StrConv(string str, int type)
        {
            return str;
        }

        /// <summary>
        /// Devuelve la cadena con el primer caracter en mayúsculas.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Capital(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        /// <summary>
        /// Modifica la cadena con el carácter dado en la posición index.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <param name="newChar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string MidStmtStr(string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            char[] chars = input.ToCharArray();
            chars[index - 1] = newChar;
            return new string(chars);
        }

        /// <summary>
        /// Convierte en cadena el objeto.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object  obj)
        {
            return obj.ToString();
        }

        /// <summary>
        /// Convierte en booleano el objeto.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// Convierte en objeto en fecha.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDate(object obj)
        {
            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// Convierte en entero el objeto.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInteger(object obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// Convierte en long el objeto.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLong(object obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// Devuelve dos objetos convertidos a string y concatenados.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static string ConcatenateObject(object obj1, object obj2)
        {
            return obj1.ToString() + obj2.ToString();
        }

        /// <summary>
        /// Reemplaza una cadena por un valor.
        /// </summary>
        /// <param name="cad"></param>
        /// <param name="search"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Replace(string cad, string search, string value)
        {
            return cad.Replace(search, value);
        }
    }
}
