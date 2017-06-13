using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ini
{
    /// <summary>
    /// A single parameter fo the IniFile class
    /// </summary>
    public class Param
    {
        private Comments _comments;
        /// <summary>
        /// The name of the current parameter
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The value of the current parameter
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// The comments for the current parameter
        /// </summary>
        public Comments Comments
        {
            get { return _comments ?? (_comments = new Comments()); }
        }

        private Param()
        {
        }

        /// <summary>
        /// The IniParam constructor.
        /// This constuctor only sets the name, 
        /// the value remains null
        /// </summary>
        /// <param name="name">name of this parameter</param>
        public Param(string name) : this()
        {
            Name = name;
            Value = string.Empty;
        }

        /// <summary>
        /// The Iniparam constructor.
        /// This constructor sets both the name and value of the current parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Param(string name, string value) : this(name)
        {
            Value = value;
        }

        /// <summary>
        /// The Iniparam constructor
        /// This constructor sets both the name and value
        /// comments can also be passed to this constructor
        /// </summary>
        /// <param name="name">Name of parameter</param>
        /// <param name="value">Value of parameter</param>
        /// <param name="comments">Comments of parameter</param>
        public Param(string name, string value, Comments comments) : this(name, value)
        {
            _comments = comments;
        }

        /// <summary>
        /// Casts the value of the current param to type T, as long as T implements IConvertible
        /// example types are: int, byte, DateTime, TimeSpan, bool
        /// </summary>
        /// <typeparam name="T">type of cast</typeparam>
        /// <param name="defaultValue">the value to use if casting fails</param>
        /// <param name="autoAssign">Wether to assign the default value in case of error</param>
        /// <returns>the casted value</returns>
        public T Cast<T>(T defaultValue = default(T), bool autoAssign = false) where T : IConvertible
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                if (autoAssign)
                {
                    Value = Convert.ToString(defaultValue, CultureInfo.InvariantCulture);
                }
                return defaultValue;
            }

            try
            {
                return (T) Convert.ChangeType(Value, typeof(T));
            }
            catch (Exception e) when (e is InvalidCastException || e is FormatException || e is OverflowException)
            {
                if (autoAssign)
                {
                    Value = Convert.ToString(defaultValue, CultureInfo.InvariantCulture);
                }
                return defaultValue;
            }
        }

        public IPAddress Cast(IPAddress defaultValue = default(IPAddress), bool autoAssign = false) 
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                if (autoAssign)
                {
                    Value = Convert.ToString(defaultValue, CultureInfo.InvariantCulture);
                }
                return defaultValue;
            }
            IPAddress outip = defaultValue;
            if (IPAddress.TryParse(Value, out outip))
            {
                return outip;
            }

            if (autoAssign)
            {
                Value = defaultValue.ToString();
            }

            return defaultValue;
        }

        /// <summary>
        /// converts the current parameter to a string
        /// </summary>
        /// <returns>name=value</returns>
        public override string ToString()
        {
            if (_comments == null)
            {
                return $"{Name}={Value}";
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine();
            builder.Append(Comments);
            builder.Append($"{Name}={Value}");

            return builder.ToString();
        }
    }
}
