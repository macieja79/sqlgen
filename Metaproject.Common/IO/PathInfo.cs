using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Metaproject.IO
{
    [DebuggerDisplay("{FullPath}")]
    public class PathInfo
    {
        private readonly List<string> _itemsList;

        public PathInfo(string path)
        {
            _itemsList = GetSplited(path);
        }

        public string FullPath => string.Join(Path.DirectorySeparatorChar.ToString(), _itemsList);

        public bool IsExist => File.Exists(FullPath);

        public PathInfo Down(int count)
        {
            for (var i = 0; i < count; i++)
                Down();

            return this;
        }

        public PathInfo Down()
        {
            _itemsList.RemoveAt(_itemsList.Count - 1);
            return this;
        }

        public PathInfo Up(string pathPart)
        {
            var items = GetSplited(pathPart);
            _itemsList.AddRange(items);
            return this;
        }

        private List<string> GetSplited(string path)
        {
            char[] separators = { Path.DirectorySeparatorChar, '/', '\\' };
            var items = path.Split(separators).ToList();
            return items;
        }
    }
}
