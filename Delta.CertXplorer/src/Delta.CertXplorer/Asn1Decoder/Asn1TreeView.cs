using Delta.CapiNet.Asn1;
using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.Asn1Decoder
{
    public partial class Asn1TreeView : TreeViewEx
    {
        private const int emptyImageIndex = 0;
        private const int documentImageIndex = 1;
        private const int sequenceImageIndex = 2;
        private const int setImageIndex = 3;
        private const int oidImageIndex = 4;
        private const int integerImageIndex = 5;
        private const int stringImageIndex = 6;
        private const int timestampImageIndex = 7;
        private const int unknownImageIndex = 8;
        private const int specificImageIndex = 9;
        private const int booleanImageIndex = 10;
        private const int invalidImageIndex = 11;

        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1TreeView"/> class.
        /// </summary>
        public Asn1TreeView()
        {
            InitializeComponent();

            // We shouldn't sort nodes as they must appear in the order they are defined in data
            base.Sorted = false; 

            //base.AllowDrag = false;
            base.AllowDrop = false;
        }

        public TreeNodeEx CreateDocumentNodes(Asn1Document document, string rootNodeText)
        {
            var rootNode = new TreeNodeEx(rootNodeText);
            rootNode.SelectedImageIndex = documentImageIndex;
            rootNode.ImageIndex = documentImageIndex;

            rootNode.Tag = document;

            foreach (var node in document.Nodes)
                AddAsnNode(node, rootNode);

            Nodes.Add(rootNode);

            return rootNode;
        }

        private void AddAsnNode(Asn1Object asn, TreeNodeEx parent)
        {
            var treeNode = new TreeNodeEx(asn.ToString());
            var imageIndex = GetImageIndex(asn);
            treeNode.SelectedImageIndex = imageIndex;
            treeNode.ImageIndex = imageIndex;

            var asnt = asn as Asn1StructuredObject;
            if (asnt != null)
            {
                foreach (var asn2 in asnt.Nodes)
                    AddAsnNode(asn2, treeNode);
            }

            treeNode.Tag = asn;
            parent.Nodes.Add(treeNode);
        }

        private int GetImageIndex(Asn1Object asn)
        {
            if (asn == null) return emptyImageIndex;
            if (asn is Asn1InvalidObject) return invalidImageIndex;
            if (asn is Asn1Sequence) return sequenceImageIndex;
            if (asn is Asn1Set) return setImageIndex;
            if (asn is Asn1Oid) return oidImageIndex;
            if (asn is Asn1Integer) return integerImageIndex;
            if (asn is Asn1BitString) return stringImageIndex;
            if (asn is Asn1Utf8String) return stringImageIndex;
            if (asn is Asn1NumericString) return stringImageIndex;
            if (asn is Asn1OctetString) return stringImageIndex;
            if (asn is Asn1UtcTime) return stringImageIndex;
            if (asn is Asn1Unsupported) return unknownImageIndex;
            if (asn is Asn1ContextSpecific) return specificImageIndex;
            if (asn is Asn1Boolean) return booleanImageIndex;

            return emptyImageIndex;
        }
    }
}
