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

        private static List<Type> m_editorTypes;
        public static IEnumerable<Type> EditorTypes
        {
            get
            {
                return EditorTypes.AsEnumerable();
            }
        }
        public static int EditorTypesCount { get { return m_editorTypes.Count; } }

        /// <summary>
        /// Register an EditorGUI that can be used for visually editing entries
        /// </summary>
        /// <param name="type">The class of the editor</param>
        public static void RegisterEditor(Type type)
        {
            if (IsEditorGUI(type))
                RegisterEditorUnsafe(type);
            else
                throw new ArgumentException("EditorGUI's must implement IEditorGUI, and include a static CanEdit(Entry) method", "type");
        }

        private static void RegisterEditorUnsafe(Type type)
        {
            m_editorTypes.Add(type);
        }

        /// <summary>
        /// Checks if the given Type is a valid EditorGUI.
        /// Type must be a class, implement IEditorGUI, and have a static method CanEdit(Entry)
        /// </summary>
        /// <param name="type"></param>
        public static bool IsEditorGUI(Type type)
        {
            if (!type.IsClass || type.GetInterface(nameof(IEditorGUI)) == null)
                return false;
            MethodInfo canEdit = type.GetMethod("CanEdit", new Type[] { typeof(Entry) });
            return canEdit != null && canEdit.IsStatic;
        }
        
        static EditorsInfo()
        {
            //code based on http://stackoverflow.com/a/18362459
            //looks for available GUI's and adds them to the list
            m_editorTypes = new List<Type>();
            string[] guiProviders = Directory.GetFiles(EditorsPath, "*.dll");
            foreach (string dllPath in guiProviders)
            {
                try
                {
                    Assembly dll = Assembly.LoadFile(dllPath);
                    foreach (Type type in dll.GetExportedTypes())
                    {
                        if (IsEditorGUI(type))
                            RegisterEditorUnsafe(type);
                    }
                }
                catch (Exception e) when (e is FileLoadException || e is BadImageFormatException)
                {
                    System.Diagnostics.Debug.WriteLine("A GUIProvider dll could not be loaded: " + e.Message);
                }
            }
        }

        public static List<IEditorGUI> FindEditors(Entry entry)
        {
            List<IEditorGUI> ret = new List<IEditorGUI>();
            foreach (Type type in m_editorTypes)
            {
                if ((bool)type.GetMethod("CanEdit", new Type[] { typeof(Entry) }).Invoke(null, new object[] { entry }))//this calls type.CanEdit(entry)
                    ret.Add((IEditorGUI)Activator.CreateInstance(type));//instantiate the type and add the instance to the list
            }
            return ret;
        }
    }
}

