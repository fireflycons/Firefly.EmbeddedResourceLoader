namespace Firefly.EmbeddedResourceLoader.Tests
{
    #pragma warning disable 649

    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Xml.Linq;

    /// <summary>
    /// Instance class containing a variety of fields and properties with embedded resource initialization
    /// </summary>
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1306", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage(
        "StyleCop.CSharp.MaintainabilityRules",
        "SA1401:FieldsMustBePrivate",
        Justification = "Reviewed. Suppression is OK here.")]
    public class InstanceResourceClass
    {
        [EmbeddedResource("Resources.TestResource1.txt")]
        private static string privateStaticStringField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        protected static string ProtectedStaticStringField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        internal static string InternalStaticStringField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        public static string PublicStaticStringField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        private static string PrivateStaticStringProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        protected static string ProtectedStaticStringProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        internal static string InternalStaticStringProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        public static string PublicStaticStringProperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        public Bitmap PublicBitmapField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        public string PublicStringField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        private XDocument privateXDocumentField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        protected XDocument ProtectedXDocumentField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        internal XDocument InternalXDocumentField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        public XDocument PublicXDocumentField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        private XDocument PrivateXDocumentProperty { get; set; }

        [EmbeddedResource("Resources.XmlTestData.xml")]
        protected XDocument ProtectedXDocumentProperty { get; set; }

        [EmbeddedResource("Resources.XmlTestData.xml")]
        internal XDocument InternalXDocumentProperty { get; set; }

        [EmbeddedResource("Resources.XmlTestData.xml")]
        public XDocument PublicXDocumentProperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        internal Bitmap InternalBitmapField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        internal string InternalStringField;

        [EmbeddedResource("Resources.C_Sharp1.png")]
        protected Bitmap ProtectedBitmapField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        protected string ProtectedStringField;

        [EmbeddedResource("Resources.C_Sharp1.png")]
        private Bitmap privateBitmapField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        private string privateStringField;

        [EmbeddedResource("Resources.C_Sharp1.png")]
        public Bitmap PublicBitmapProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        public string PublicStringProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        public virtual string PublicVirtualStringPproperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        internal Bitmap InternalBitmapProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        internal string InternalStringProperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        protected Bitmap ProtectedBitmapProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        protected string ProtectedStringProperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        private Bitmap PrivateBitmapProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        private string PrivateStringProperty { get; set; }

        public static string GetPrivateStaticStringField()
        {
            return privateStaticStringField;
        }

        public static string GetProtectedStaticStringField()
        {
            return ProtectedStaticStringField;
        }

        public static string GetInternalStaticStringField()
        {
            return InternalStaticStringField;
        }

        public static string GetPublicStaticStringField()
        {
            return PublicStaticStringField;
        }

        public static string GetPrivateStaticStringProperty()
        {
            return PrivateStaticStringProperty;
        }

        public static string GetProtectedStaticStringProperty()
        {
            return ProtectedStaticStringProperty;
        }

        public static string GetInternalStaticStringProperty()
        {
            return InternalStaticStringProperty;
        }

        public static string GetPublicStaticStringProperty()
        {
            return PublicStaticStringProperty;
        }

        public Bitmap GetPublicBitmapField()
        {
            return this.PublicBitmapField;
        }

        public Bitmap GetPublicBitmapProperty()
        {
            return this.PublicBitmapProperty;
        }

        public Bitmap GetInternalBitmapField()
        {
            return this.InternalBitmapField;
        }

        public Bitmap GetInternalBitmapProperty()
        {
            return this.InternalBitmapProperty;
        }

        public string GetInternalStringField()
        {
            return this.InternalStringField;
        }

        public string GetInternalStringProperty()
        {
            return this.InternalStringProperty;
        }

        public Bitmap GetPrivateBitmapField()
        {
            return this.privateBitmapField;
        }

        public Bitmap GetPrivateBitmapProperty()
        {
            return this.PrivateBitmapProperty;
        }

        public string GetPrivateStringField()
        {
            return this.privateStringField;
        }

        public string GetPrivateStringProperty()
        {
            return this.PrivateStringProperty;
        }

        public Bitmap GetProtectedBitmapField()
        {
            return this.ProtectedBitmapField;
        }

        public Bitmap GetProtectedBitmapProperty()
        {
            return this.ProtectedBitmapProperty;
        }

        public string GetProtectedStringField()
        {
            return this.ProtectedStringField;
        }

        public string GetProtectedStringProperty()
        {
            return this.ProtectedStringProperty;
        }

        public string GetPublicStringField()
        {
            return this.PublicStringField;
        }

        public string GetPublicStringProperty()
        {
            return this.PublicStringProperty;
        }

        public string GetPublicVirtualStringProperty()
        {
            return this.PublicVirtualStringPproperty;
        }

        public XDocument GetPrivateXDocumentField()
        {
            return this.privateXDocumentField;
        }

        public XDocument GetProtectedXDocumentField()
        {
            return this.ProtectedXDocumentField;
        }

        public XDocument GetInternalXDocumentField()
        {
            return this.InternalXDocumentField;
        }

        public XDocument GetPublicXDocumentField()
        {
            return this.PublicXDocumentField;
        }

        public XDocument GetPrivateXDocumentProperty()
        {
            return this.PrivateXDocumentProperty;
        }

        public XDocument GetInternalXDocumentProperty()
        {
            return this.InternalXDocumentProperty;
        }

        public XDocument GetProtectedXDocumentProperty()
        {
            return this.ProtectedXDocumentProperty;
        }

        public XDocument GetPublicXDocumentProperty()
        {
            return this.PublicXDocumentProperty;
        }
    }
}