using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor;
using Aga.Controls.Tree;

namespace CK2EditorGUI.Utility
{
    static class Extensions
    {
        public static TreePath Up(this TreePath path, int count = 1)
        {
            return new TreePath(path.FullPath.Take(path.FullPath.Length - count).ToArray());
        }
    }
}
