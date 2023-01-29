// Inspector Gadgets // https://kybernetik.com.au/inspector-gadgets // Copyright 2020 Kybernetik //

using UnityEngine;

namespace InspectorGadgets
{
    /// <summary>
    /// Holds a text comment for a <see cref="GameObject"/> which can be viewed and edited in the inspector.
    /// <para></para>
    /// By default, this script sets itself to be excluded from the build.
    /// </summary>
    [HelpURL(Strings.APIDocumentationURL + "/" + nameof(CommentComponent))]
    public sealed class CommentComponent : MonoBehaviour, IComment
    {
        /************************************************************************************************************************/

        [SerializeField, TextArea]
        private string _Text;

#if UNITY_EDITOR
        string IComment.TextFieldName => nameof(_Text);
#endif

        /// <summary>[<see cref="SerializeField"/>] [<see cref="IComment"/>] The text of this comment.</summary>
        public string Text
        {
            get => _Text;
            set => _Text = value;
        }

        /************************************************************************************************************************/

        /// <summary>False if this script is set to <see cref="HideFlags.DontSaveInBuild"/>.</summary>
        public bool IncludeInBuild
        {
            get => (hideFlags &= HideFlags.DontSaveInBuild) == 0;
            set
            {
                if (value)
                    hideFlags &= ~HideFlags.DontSaveInBuild;
                else
                    hideFlags |= HideFlags.DontSaveInBuild;
            }
        }

        /************************************************************************************************************************/

        private void Reset()
        {
            IncludeInBuild = false;
        }

        /************************************************************************************************************************/
    }
}

/************************************************************************************************************************/

#if UNITY_EDITOR
namespace InspectorGadgets.Editor
{
    [UnityEditor.CustomEditor(typeof(CommentComponent))]
    internal sealed class CommentComponentEditor : CommentEditor { }
}
#endif

