using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

using CK2EditorGUI.EditorGUIs;
using CK2Editor;

namespace CK2EditorGUI.Utility
{
    public static class EditorsInfo
    {
        public static readonly string EditorsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "GUIProviders");

        private static List<IEditorGUIProvider> m_editorTypes;
        public static IEnumerable<IEditorGUIProvider> EditorTypes
        {
            get
            {
                return m_editorTypes.AsEnumerable();
            }
        }
        public static int EditorTypesCount { get { return m_editorTypes.Count; } }

        /// <summary>
        /// Register an EditorGUI that can be used for visually editing entries
        /// </summary>
        /// <param name="type">The class of the editor</param>
        public static void RegisterEditor(Type type)
        {
            if (IsEditorGUIProvider(type))
                RegisterEditorUnsafe(type);
            else
                throw new ArgumentException("EditorGUI's must implement IEditorGUI, and include a static CanEdit(Entry) method", "type");
        }

        private static void RegisterEditorUnsafe(Type type)
        {
            m_editorTypes.Add((IEditorGUIProvider)Activator.CreateInstance(type));
        }

        /// <summary>
        /// Checks if the given Type is a valid EditorGUIProvider
        /// Type must be a class and implement IEditorGUIProvider
        /// </summary>
        /// <param name="type"></param>
        public static bool IsEditorGUIProvider(Type type)
        {
            if (!type.IsClass || type.GetInterface(nameof(IEditorGUIProvider)) == null)
                return false;
            return true;
        }

        static EditorsInfo()
        {
            //code based on http://stackoverflow.com/a/18362459
            //looks for available GUI's and adds them to the list
            m_editorTypes = new List<IEditorGUIProvider>();
            string[] guiProviders = Directory.GetFiles(EditorsPath, "*.dll");
            foreach (string dllPath in guiProviders)
            {
                try
                {
                    Assembly dll = Assembly.LoadFile(dllPath);
                    foreach (Type type in dll.GetExportedTypes())
                    {
                        if (IsEditorGUIProvider(type))
                            RegisterEditorUnsafe(type);
                    }
                }
                catch (Exception e) when (e is FileLoadException || e is BadImageFormatException)
                {
                    System.Diagnostics.Debug.WriteLine("A GUIProvider dll could not be loaded: " + e.Message);
                }
            }
        }

        public static List<IEditorGUIProvider> FindEditors(Entry entry)
        {
            List<IEditorGUIProvider> ret = new List<IEditorGUIProvider>();
            foreach (IEditorGUIProvider type in m_editorTypes)
            {
                if (type.CanEdit(entry))
                    ret.Add(type);
            }
            return ret;
        }

        public static IEditorGUIProvider FindEditorByName(string name)
        {
            return m_editorTypes.Find(provider => provider.StructureName == name);
        }
    }
}

