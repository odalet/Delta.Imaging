using System;
using System.Windows.Forms;
using System.Globalization;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Stocke et restaure l'état (ouvert/fermé) des noeuds d'un arbre 
    /// </summary>
    /// <remarks>
    /// L'état de l'arbre est stocké dans une chaîne.
    /// </remarks>
    public class TreeViewExpandedStateSerializer
    {
        #region Resolvers
        private class TextTreeNodeResolver : ITreeNodeResolver
        {
            #region ITreeNodeResolver Members

            public TreeNode FindNode(TreeNodeCollection nodes, string key)
            {
                foreach (TreeNode tn in nodes) { if (tn.Text == key) return tn; }
                return null;
            }

            public string GetNodeKey(TreeNode node) { return node.Text; }

            #endregion
        }

        private class NameTreeNodeResolver : ITreeNodeResolver
        {
            #region ITreeNodeResolver Members

            public TreeNode FindNode(TreeNodeCollection nodes, string key)
            {
                if (nodes.ContainsKey(key)) return nodes[key];
                else return null;
            }

            public string GetNodeKey(TreeNode node) { return node.Name; }

            #endregion
        }

        #endregion

        private static readonly string TrueString = true.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();

        private TreeView treeview = null;
        private ITreeNodeResolver treenodeResolver = null;

        public TreeViewExpandedStateSerializer(TreeView tv) : this(tv, BuiltinTreeNodeResolver.Name) { }
        public TreeViewExpandedStateSerializer(TreeView tv, BuiltinTreeNodeResolver resolver) : this(tv, GetResolver(resolver)) { }

        public TreeViewExpandedStateSerializer(TreeView tv, ITreeNodeResolver resolver)
        {
            if (tv == null) throw new ArgumentNullException("tv");
            if (resolver == null) throw new ArgumentNullException("resolver");

            treeview = tv;
            treenodeResolver = resolver;
        }

        public ITreeNodeResolver TreeNodeResolver { get { return treenodeResolver; } }

        public string Serialize()
        {
            string innerState = string.Empty;
            if ((treeview.Nodes != null) && (treeview.Nodes.Count > 0))
                innerState = SerializeCollection(treeview.Nodes);
            return "{" + innerState + "}";
        }

        public void Deserialize(string state)
        {
            treeview.CollapseAll();
            DeserializeCollection(treeview.Nodes, state);
        }

        private static ITreeNodeResolver GetResolver(BuiltinTreeNodeResolver builtin)
        {
            switch (builtin)
            {
                case BuiltinTreeNodeResolver.Text: return new TextTreeNodeResolver();
                case BuiltinTreeNodeResolver.Name: return new NameTreeNodeResolver();
            }

            return null;
        }

        private string SerializeCollection(TreeNodeCollection collection)
        {
            string state = string.Empty;
            foreach (TreeNode tn in collection)
            {
                if ((tn.Nodes != null) && (tn.Nodes.Count > 0)) // inutile de parcourir les noeuds sans descendants
                {
                    string status = ":" + tn.IsExpanded.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
                    string innerState = string.Empty;
                    innerState = SerializeCollection(tn.Nodes);
                    state += "{" + SerializeText(treenodeResolver.GetNodeKey(tn)) + status + innerState + "}";
                }
            }

            return state;
        }

        private void DeserializeCollection(TreeNodeCollection collection, string state)
        {
            if (string.IsNullOrEmpty(state)) return;

            while ((state[0] == '{') && (state[state.Length - 1] == '}'))
            {
                state = state.Substring(1, state.Length - 2);
                state = state.Trim();
            }
            

            if (string.IsNullOrEmpty(state)) return;

            int index = 0;
            string currentNodeText = string.Empty;
            TreeNode lastNode = null;
            while (true)
            {
                if (index >= state.Length) break;
                else if (state[index] == '{')
                {
                    lastNode = TryExpand(collection, currentNodeText.Trim());
                    int next = FindClosingBrace(state.Substring(index)) + index;
                    if (next <= index) // on tente quand même...
                    {
                        if (lastNode != null)
                            DeserializeCollection(lastNode.Nodes, state.Substring(index));
                        break;
                    }
                    else
                    {
                        if (lastNode != null)
                            DeserializeCollection(lastNode.Nodes, state.Substring(index, next - index + 1));
                        index = next + 1;
                    }
                }
                else
                {
                    currentNodeText += state[index];
                    index++;
                }
            }

            TryExpand(collection, currentNodeText.Trim());
        }

        private TreeNode TryExpand(TreeNodeCollection nodes, string text)
        {
            bool expand = true;
            string nodetext = text;
            
            int index = text.IndexOf(':');
            if (index != -1)
            {
                nodetext = DeserializeText(text.Substring(0, index));
                expand = (string.Compare(text.Substring(index + 1), TrueString, true) == 0);
            }

            TreeNode tn = treenodeResolver.FindNode(nodes, nodetext);
            if ((tn != null) && expand) tn.Expand();
            return tn;
        }

        //private TreeNode FindNode(TreeNodeCollection nodes, string text)
        //{
        //    foreach (TreeNode tn in nodes)
        //    {
        //        if (tn.Text == text) return tn;
        //    }

        //    return null;
        //}

        private int FindClosingBrace(string text)
        {
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '{') count++;
                if (text[i] == '}')
                {
                    count--;
                    if (count == 0) return i;
                }
            }
            
            return -1;
        }

        private string SerializeText(string text)
        {
            text = text.Replace("\\", "\\5C");
            text = text.Replace(":", "\\3A");
            text = text.Replace("{", "\\7B");
            text = text.Replace("}", "\\7D");
            return text;
        }

        private string DeserializeText(string text)
        {
            text = text.Replace("\\7D", "}");
            text = text.Replace("\\7B", "{");
            text = text.Replace("\\3A", ":");
            text = text.Replace("\\5C", "\\");
            return text;
        }
    }
}
