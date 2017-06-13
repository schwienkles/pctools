using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ini
{
    /// <summary>
    /// A single Section to be used with the IniFile class
    /// </summary>
    public class Section
    {
        private List<Param> _params;
        private Comments _comments;
        /// <summary>
        /// The name of the current Section
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The comments for the current Section
        /// </summary>
        public Comments Comments
        {
            get { return _comments ?? (_comments = new Comments()); }
        }

        /// <summary>
        /// Gets or sets the parameter at the given name
        /// The parameter is created if it doesn't exist
        /// </summary>
        /// <param name="paramName">name of the parameter</param>
        /// <returns>The parameter at name "paramName"</returns>
        public Param this[string paramName]
        {
            get { return GetOrCreateParameter(paramName); }
            set
            {
                AddParam(value);
            }
        }

        private Section()
        {
            _params = new List<Param>();
        }

        /// <summary>
        /// Initializes a new Section
        /// </summary>
        /// <param name="name">Name of the section</param>
        public Section(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new Section
        /// </summary>
        /// <param name="name">name of the section</param>
        /// <param name="comments">comments of the section</param>
        public Section(string name, Comments comments) : this(name)
        {
            _comments = comments;
        }

        /// <summary>
        /// Gets (or creates a new) parameter at "name"
        /// </summary>
        /// <param name="name">Name of the parameter to find</param>
        /// <returns>The found parameter</returns>
        public Param GetOrCreateParameter(string name)
        {
            Param param = _params.FirstOrDefault(x => x.Name == name);
            if (param == null)
            {
                param = new Param(name);
                AddParam(param);
            }

            return param;
        }

        /// <summary>
        /// Adds a new parameter to the current Section
        /// </summary>
        /// <param name="param"></param>
        public void AddParam(Param param)
        {
            if (_params.Any(x => x.Name == param.Name))
            {
                throw new InvalidOperationException(nameof(param.Name));
            }

            _params.Add(param);
        }

        /// <summary>
        /// Converts the current Section to a string output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{_comments}[{Name}]");

            foreach (Param param in _params)
            {
                builder.AppendLine(param.ToString());
            }

            builder.AppendLine();
            builder.AppendLine();

            return builder.ToString();
        }
    }
}
