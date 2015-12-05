using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace PhotoTouch
{
    /// <summary>
    /// Interaction logic for PhotoFrame.xaml
    /// </summary>
    public partial class PhotoFrame : UserControl
    {
        // current mode (true = draw mode, false = move mode)
        bool mode = true;

        // current drawing mode (true = pencil, false = erase)
        bool drawingMode = true;

        // current drawing color
        Color drawingColor;

        // colored circle in the draw button
        Ellipse buttonDrawContent = new Ellipse();

        // actual frame of this photo frame control
        ScatterViewItem frame;

        // drawing canvas in this photo frame
        SurfaceInkCanvas canvas;

        // buttons at the bottom of this photo frame
        SurfaceButton buttonMode;
        SurfaceButton buttonDraw;
        SurfaceButton buttonClose;

        // location of photos
        string photosDir = Environment.GetEnvironmentVariable("public") + @"\Pictures\Sample Pictures";



        public PhotoFrame()
        {
            InitializeComponent();

            // get the different controls of this photo frame
            frame = (ScatterViewItem)this.FindName("Frame");
            canvas = (SurfaceInkCanvas)this.FindName("Canvas");
            buttonMode = (SurfaceButton)this.FindName("ButtonMode");
            buttonDraw = (SurfaceButton)this.FindName("ButtonDraw");
            buttonClose = (SurfaceButton)this.FindName("ButtonDraw");

            // initialize the mode of the photo frame
            SetMode();

            // initialize the colored circle for the draw button
            buttonDrawContent.Width = 38;
            buttonDrawContent.Height = 38;

            // create the icon in the close button
            ((SurfaceButton)FindName("ButtonClose")).Content = FindResource("CloseIcon");

            // set the default drawing color
            SetDrawingColor(Color.FromRgb(255, 0, 0));

            // set the content of the photo frame
            if (Directory.Exists(photosDir))
            {
                string[] files = Directory.GetFiles(photosDir, "*.jpg");
                if (files.Length > 0)
                {
                    Photo.Source = new BitmapImage(new Uri(files[new Random().Next(0, files.Length)]));
                }
            }
        }

        void SetMode()
        {
            SetDrawingMode();
            if (mode)
            {
                canvas.IsHitTestVisible = false;
                buttonMode.Content = FindResource("MoveIcon");
                buttonDraw.Content = null;
            }
            else
            {
                canvas.IsHitTestVisible = true;
                buttonMode.Content = FindResource("DrawIcon");
            }
        }
        
        void SwitchMode(object sender, RoutedEventArgs e)
        {
            mode = !mode;
            SetMode();
        }

        void SetDrawingMode()
        {
            if (drawingMode)
            {
                buttonDraw.Content = buttonDrawContent;
                canvas.EditingMode = SurfaceInkEditingMode.Ink;
            }
            else
            {
                buttonDraw.Content = FindResource("EraseIcon"); ;
                canvas.EditingMode = SurfaceInkEditingMode.EraseByPoint;
            }
        }

        void SwitchDrawingMode(object sender, RoutedEventArgs e)
        {
            if (mode)
            {
                return;
            }
            drawingMode = !drawingMode;
            SetDrawingMode();
        }

        void SetDrawingColor(Color color)
        {
            drawingColor = color;
            buttonDrawContent.Fill = new SolidColorBrush(drawingColor);
            ((SurfaceInkCanvas)FindName("Canvas")).DefaultDrawingAttributes.Color = drawingColor;
        }

        void Close(object sender, RoutedEventArgs e)
        {
            // remove the photo frame from the table
            ((ScatterView)frame.Parent).Items.Remove(frame);
        }
    }
}
