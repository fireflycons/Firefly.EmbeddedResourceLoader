
namespace Firefly.EmbeddedResourceLoader.Tests
{
    #pragma warning disable 0649

    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Xml.Linq;

    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1306", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage(
        "StyleCop.CSharp.MaintainabilityRules",
        "SA1401:FieldsMustBePrivate",
        Justification = "Reviewed. Suppression is OK here.")]
    public static class StaticResourceClass
    {
        [EmbeddedResource("Resources.TestResource1.txt")]
        public static string PublicStaticStringField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        internal static string InternalStaticStringField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        private static string privateStaticStringField;

        [EmbeddedResource("Resources.TestResource1.txt")]
        public static string PublicStaticStringProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        internal static string InternalStaticStringProperty { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        private static string PrivateStaticStringProperty { get; set; }

        [EmbeddedResource("Resources.XmlTestData.xml")]
        private static XDocument privateXDocumentField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        internal static XDocument InternalXDocumentField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        public static XDocument PublicXDocumentField;

        [EmbeddedResource("Resources.C_Sharp1.png")]
        public static Bitmap PublicBitmapField;

        [EmbeddedResource("Resources.C_Sharp1.png")]
        internal static Bitmap InternalBitmapField;

        [EmbeddedResource("Resources.C_Sharp1.png")]
        private static Bitmap privateBitmapField;

        [EmbeddedResource("Resources.XmlTestData.xml")]
        private static XDocument PrivateXDocumentProperty { get; set; }

        [EmbeddedResource("Resources.XmlTestData.xml")]
        internal static XDocument InternalXDocumentProperty { get; set; }

        [EmbeddedResource("Resources.XmlTestData.xml")]
        public static XDocument PublicXDocumentProperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        public static Bitmap PublicBitmapProperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        internal static Bitmap InternalBitmapProperty { get; set; }

        [EmbeddedResource("Resources.C_Sharp1.png")]
        private static Bitmap PrivateBitmapProperty { get; set; }


        public static string GetInternalStaticStringField()
        {
            return InternalStaticStringField;
        }

        public static string GetInternalStaticStringProperty()
        {
            return InternalStaticStringProperty;
        }

        public static string GetPrivateStaticStringField()
        {
            return privateStaticStringField;
        }

        public static string GetPrivateStaticStringProperty()
        {
            return PrivateStaticStringProperty;
        }

        public static string GetPublicStaticStringField()
        {
            return PublicStaticStringField;
        }

        public static string GetPublicStaticStringProperty()
        {
            return PublicStaticStringProperty;
        }

        public static XDocument GetPrivateXDocumentField()
        {
            return privateXDocumentField;
        }

        public static XDocument GetInternalXDocumentField()
        {
            return InternalXDocumentField;
        }

        public static XDocument GetPublicXDocumentField()
        {
            return PublicXDocumentField;
        }

        public static XDocument GetPrivateXDocumentProperty()
        {
            return PrivateXDocumentProperty;
        }

        public static XDocument GetInternalXDocumentProperty()
        {
            return InternalXDocumentProperty;
        }

        public static XDocument GetPublicXDocumentProperty()
        {
            return PublicXDocumentProperty;
        }

        public static Bitmap GetPrivateBitmapField()
        {
            return privateBitmapField;
        }

        public static Bitmap GetPrivateBitmapProperty()
        {
            return PrivateBitmapProperty;
        }

        public static Bitmap GetInternalBitmapField()
        {
            return InternalBitmapField;
        }
        
        public static Bitmap GetInternalBitmapProperty()
        {
            return InternalBitmapProperty;
        }

        public static Bitmap GetPublicBitmapField()
        {
            return PublicBitmapField;
        }

        public static Bitmap GetPublicBitmapProperty()
        {
            return PublicBitmapProperty;
        }


    }
}