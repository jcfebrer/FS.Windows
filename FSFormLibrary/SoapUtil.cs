using FSException;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#if NET35_OR_GREATER
    using System.Xml.Linq;
#endif

#if NET40_OR_GREATER
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Soap;
#endif

namespace FSFormLibrary
{
    /// <summary>
    /// Utilidades para trabajar con Soap.
    /// </summary>
    public class SoapUtil
    {

#region SOAP Serialization
#if NET40_OR_GREATER
        /// <summary>
        ///     DeSerializes a string into a  object
        /// </summary>
        /// <param name="soapString">String to be deserialized</param>
        /// <returns>Deserialized field object</returns>
        public static object SoapTo(string soapString)
        {
            IFormatter formatter;
            object objectFromSoap = null;
            try
            {
                using (var memStream = new MemoryStream(Encoding.UTF8.GetBytes(soapString)))
                {
                    formatter = new SoapFormatter();
                    objectFromSoap = formatter.Deserialize(memStream);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return objectFromSoap;
        }

        /// <summary>
        ///     DeSerializes a string into a  object
        /// </summary>
        /// <param name="filePath">String to be deserialized</param>
        /// <returns>Deserialized field object</returns>
        public static object SoapToFromFile(string filePath)
        {
            IFormatter formatter;
            object objectFromSoap = null;
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    formatter = new SoapFormatter();
                    objectFromSoap = formatter.Deserialize(fileStream);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return objectFromSoap;
        }

        /// <summary>
        ///     Serializes the field object into a string
        /// </summary>
        /// <param name="objToSoap">Field Object to be serialized</param>
        /// <returns>Serialized field object</returns>
        public static string ToSoap(object objToSoap)
        {
            IFormatter formatter;
            var strObject = "";
            try
            {
                using (var memStream = new MemoryStream())
                {
                    formatter = new SoapFormatter();
                    formatter.Serialize(memStream, objToSoap);
                    memStream.Flush();
                    strObject = Encoding.UTF8.GetString(memStream.GetBuffer(), 0, (int)memStream.Position);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return strObject;
        }

        /// <summary>
        ///     Serializes the field object into a string
        /// </summary>
        /// <param name="objToSoap">Field Object to be serialized</param>
        /// <param name="filePath">File to write result</param>
        /// <returns>Serialized field object</returns>
        public static void ToSoap(object objToSoap, string filePath)
        {
            IFormatter formatter;
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    formatter = new SoapFormatter();
                    formatter.Serialize(fileStream, objToSoap);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        ///     DeSerializes a string into a  object
        /// </summary>
        /// <param name="filePath">String to be deserialized</param>
        /// <param name="binder">Serialization binder</param>
        /// <returns>Deserialized field object</returns>
        public static object SoapToFromFile(string filePath, SerializationBinder binder)
        {
            IFormatter formatter;
            object objectFromSoap = null;
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    formatter = new SoapFormatter();
                    formatter.Binder = binder;
                    objectFromSoap = formatter.Deserialize(fileStream);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return objectFromSoap;
        }
#endif
#endregion
    }
}