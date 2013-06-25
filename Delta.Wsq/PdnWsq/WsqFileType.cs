using System.IO;
using System.Drawing;
using System.Collections.Generic;

using PaintDotNet;
using PaintDotNet.IO;
using PaintDotNet.IndirectUI;
using PaintDotNet.PropertySystem;

using Delta.Wsq;

namespace PdnWsq
{
    public class WsqFileType : PropertyBasedFileType, IFileTypeFactory
    {
        private enum PropertyNames
        {
            Quality,
            Comment
        }

        public const string CodecName = "WSQ";

        private static readonly string[] extensions = new string[] { ".wsq" };
        private const FileTypeFlags flags = FileTypeFlags.SupportsLoading | FileTypeFlags.SupportsSaving;

        public WsqFileType()
            : base(CodecName, flags, extensions)
        {
        }

        public override ControlInfo OnCreateSaveConfigUI(PropertyCollection props)
        {
            var info = PropertyBasedFileType.CreateDefaultSaveConfigUI(props);
            info.SetPropertyControlValue(PropertyNames.Quality, ControlInfoPropertyNames.DisplayName, "Quality");
            info.SetPropertyControlValue(PropertyNames.Comment, ControlInfoPropertyNames.DisplayName, "Comment");
            return info;
        }

        public override PropertyCollection OnCreateSavePropertyCollection()
        {
            return new PropertyCollection(new List<Property> 
            {
                new Int32Property(PropertyNames.Quality, WsqCodec.Constants.DefaultQuality, WsqCodec.Constants.MinQuality, WsqCodec.Constants.MaxQuality),
                new StringProperty(PropertyNames.Comment, string.Empty)
            });
        }

        protected override void OnSaveT(Document input, Stream output, PropertyBasedSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
        {
            var quality = (int)token.GetProperty(PropertyNames.Quality).Value;
            var comment = (string)token.GetProperty(PropertyNames.Comment).Value;

            using (var args = new RenderArgs(scratchSurface))
                input.Render(args, false);

            var enc = new WsqEncoder();
            byte[] bytes = null;
            using (var bitmap = scratchSurface.CreateAliasedBitmap())
            {
                SetResolution(bitmap, input);
                enc.Comment = comment;
                bytes = enc.EncodeQualityGdi(bitmap, quality);
            }

            if (bytes != null)
                output.Write(bytes, 0, (int)bytes.Length);
        }

        protected override Document OnLoad(Stream input)
        {
            if (input == null || input.Length == 0) return null;
            var length = input.Length;
            var bytes = new byte[length];
            input.ProperRead(bytes, 0, (int)length);

            var dec = new WsqDecoder();
            using (var bitmap = dec.DecodeGdi(bytes))
            {
                var surface = new Surface(bitmap.Width, bitmap.Height);
                surface.CopyFromGdipBitmap(bitmap);

                var layer = new BitmapLayer(surface);
                var document = new Document(layer.Width, layer.Height);

                document.Layers.Add(layer);

                document.DpuUnit = MeasurementUnit.Inch;
                document.DpuX = (double)bitmap.HorizontalResolution;
                document.DpuY = (double)bitmap.VerticalResolution;

                return document;
            }
        }

        #region IFileTypeFactory Members

        public FileType[] GetFileTypeInstances()
        {
            return new FileType[] { new WsqFileType() };
        }

        #endregion

        private void SetResolution(Bitmap bitmap, Document document)
        {
            if (document.DpuUnit == MeasurementUnit.Pixel)
                return; // Nothing we can do here...

            var dpix = document.DpuUnit == MeasurementUnit.Inch ?
                document.DpuX : Document.DotsPerCmToDotsPerInch(document.DpuX);
            var dpiy = document.DpuUnit == MeasurementUnit.Inch ?
                document.DpuY : Document.DotsPerCmToDotsPerInch(document.DpuY);

            bitmap.SetResolution((float)dpix, (float)dpiy);
        }
    }
}
