using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ini
{
    /// <summary>
    /// Comments for use in the IniFile
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// The allowed characters to be used as comment identifiers
        /// </summary>
        public const string CommentChars = "#;";
        /// <summary>
        /// The initial amount of memory spaces to reserve for comments
        /// There is no space reserved of there are no comments
        /// </summary>
        public const int InitialCapacity = 1;
        private List<string> _comments;

        /// <summary>
        /// Initializes a new comments group
        /// </summary>
        public Comments()
        {    
        }

        private bool HasComments()
        {
            return _comments == null || _comments.Any();
        }

        /// <summary>
        /// Adds one or more comments to the comments list
        /// </summary>
        /// <param name="comment">comment(s) to add</param>
        public void Add(params string[] comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            if (_comments == null)
            {
                _comments = new List<string>(InitialCapacity);
            }

            foreach (string comm in comment)
            {
                if(comm.Length < 2 || CommentChars.IndexOf(comm[0]) < 0)
                {
                    throw new FormatException($"Comment '{comm}' is too short or doesn't start with any of the comment characters");
                }

                if (!_comments.Contains(comm))
                {
                    _comments.Add(comm);
                }
            }
        }

        /// <summary>
        /// Converst the current comment section to a string
        /// </summary>
        /// <returns>(string)comments</returns>
        public override string ToString()
        {
            if (!HasComments())
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            
            foreach (string comment in _comments)
            {
                builder.AppendLine(comment);
            }

            return builder.ToString();
        }
    }
}
