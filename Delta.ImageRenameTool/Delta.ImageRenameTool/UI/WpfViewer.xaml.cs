using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Goheer.EXIF;
using System.Windows.Media;
using System.Globalization;

namespace Delta.ImageRenameTool.UI
{
    /// <summary>
    /// Interaction logic for WpfViewer.xaml
    /// </summary>
    public partial class WpfViewer : UserControl //, INotifyPropertyChanged
    {
        public WpfViewer()
        {
            InitializeComponent();
        }

        internal void ResetImage()
        {
            Dispatcher.Invoke((Action)(() => SetValue(ImageProperty, null)));
        }

        internal void LoadImageAsync(FileRenameInfo data, bool applyExifRotation = true)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                var filename = data.GetFileName();
                var rotation = applyExifRotation ? GetRotation(data.Exif) : 0.0;

                // load image
                var image = new BitmapImage();
                try
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        image.BeginInit();
                        image.StreamSource = stream;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.EndInit(); // load the image from the stream
                    }

                    SetValue(ImageProperty, image);
                    SetValue(RotationProperty, rotation);
                }
                catch (Exception ex)
                {
                    // An error occurred while loading the image file (maybe it does not exist any more...)
                    // Let's make an image indicating the problem.
                    SetValue(ImageProperty, CreateErrorImage(ex));
                }
            }));
        }

        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(
            "Rotation", typeof(double), typeof(WpfViewer), new PropertyMetadata(0.0));

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            "Image", typeof(ImageSource), typeof(WpfViewer), new PropertyMetadata(null));

        private double GetRotation(EXIFextractor exif)
        {
            int orientation = 1;
            try
            {
                var porientation = exif.Cast<Pair>().FirstOrDefault(p => (string)p.First == "Orientation");
                orientation = int.Parse((string)porientation.Second);
            }
            catch (Exception ex)
            {
                var debugException = ex; // Intended                
            }

            if (orientation == 1) return 0.0;
            if (orientation == 6) return 90.0;

            // Other cases not handled for now; see http://sylvana.net/jpegcrop/exif_orientation.html
            // Here is the algorithm:
            /*
             * 1) transform="";;
             * 2) transform="-flip horizontal";;
             * 3) transform="-rotate 180";;
             * 4) transform="-flip vertical";;
             * 5) transform="-transpose";;
             * 6) transform="-rotate 90";;
             * 7) transform="-transverse";;
             * 8) transform="-rotate 270";;
             * *) transform="";;
             */

            return 0.0;
        }

        private ImageSource CreateErrorImage(Exception ex)
        {
            var message = ex == null ? "Unknown Error" : ex.Message;            
            var visual = new DrawingVisual();
            using (var dc = visual.RenderOpen())
                dc.DrawText(new FormattedText(
                    message, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                    new Typeface("Segoe UI"), 32.0, Brushes.Red), new Point(0.0, 0.0));

            return new DrawingImage(visual.Drawing);
        }
    }
}
