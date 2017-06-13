using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ini
{
    /// <summary>
    /// The IniFile class allows you to create new inifiles or edit and use existing ones
    /// </summary>
    public class File
    {
        private readonly List<Section> _sections;
        private string _path;

        /// <summary>
        /// Gets or sets the section at name "name"
        /// The section will be added if it is not found
        /// </summary>
        /// <param name="name">The name of the section to get or set</param>
        /// <returns>The found section</returns>
        public Section this[string name] => GetOrCreateSection(name);

        private File()
        {
            _sections = new List<Section>();
        }

        /// <summary>
        /// Initialises a new IniFile
        /// An IniFile at "path" will be parsed if it is present
        /// </summary>
        /// <param name="path">path to (existing or destination) IniFile</param>
        public File(string path) : this()
        {
            _path = path;
            if (System.IO.File.Exists(_path))
            {
                Parse();
            }
        }

        /// <summary>
        /// Adds a new Section to the IniFile
        /// </summary>
        /// <param name="section">Section to add</param>
        public void AddSection(Section section)
        {
            if (_sections.Any(x => x.Name == section.Name))
            {
                throw new InvalidOperationException("Ini File already contains a section with the same name");
            }

            _sections.Add(section);
        }

        /// <summary>
        /// Gets the section at the given name
        /// The section will be created if the section doesn't exist
        /// </summary>
        /// <param name="name">name of the section</param>
        /// <returns>the section</returns>
        public Section GetOrCreateSection(string name)
        {
            Section section = _sections.FirstOrDefault(x => x.Name == name);
            if (section == null)
            {
                section = new Section(name);
                AddSection(section);
            }

            return section;
        }

        private void Parse()
        {
            string[] lines = System.IO.File.ReadAllLines(_path);
            Section currentSection = null;
            Comments currentComments = null;

            var paramRegex   = new Regex(@"(.*?)=(.*)");
            var sectionRegex = new Regex(@"^\[([a-zA-Z:_]+)\]$");
            var commentRegex = new Regex($@"^[{Comments.CommentChars}][\s\S]*");

            foreach (string line in lines)
            {
                if (paramRegex.IsMatch(line))
                {
                    if (currentSection == null)
                    {
                        throw new FormatException("Inifile has parameters outside of a section");
                    }

                    var match = paramRegex.Match(line);
                    var param = new Param(match.Groups[1].Value, match.Groups[2].Value, currentComments);

                    currentSection.AddParam(param);
                    currentComments = null;
                }
                else if (commentRegex.IsMatch(line))
                {
                    if (currentComments == null)
                    {
                        currentComments = new Comments();
                    }

                    currentComments.Add(line);
                }
                else if (sectionRegex.IsMatch(line))
                {
                    var match = sectionRegex.Match(line);
                    currentSection = new Section(match.Groups[1].Value, currentComments);

                    _sections.Add(currentSection);
                    currentComments = null;
                }
            }
        }

        /// <summary>
        /// Saves the IniFile to the path given at initialisation
        /// </summary>
        public void Save()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Section section in _sections)
            {
                builder.Append(section);
            }

            System.IO.File.WriteAllText(_path, builder.ToString());
        }

        /// <summary>
        /// Saves the current IniFile to the given location
        /// </summary>
        /// <param name="path">The location to store the IniFile</param>
        public void Save(string path)
        {
            _path = path;
            Save();
        }
    }
}
