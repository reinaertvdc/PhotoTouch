using System;
using System.Collections.Generic;
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
        public PhotoFrame()
        {
            InitializeComponent();

            SwitchMode(this, null);
            ((SurfaceButton)FindName("ButtonClose")).Content = FindResource("CloseIcon");
        }










        void SwitchMode(object sender, RoutedEventArgs e)
        {
            // get the actual photo frame
            ScatterViewItem frame = (ScatterViewItem)this.FindName("Frame");
            
            // get the drawing canvas of the photo frame
            SurfaceInkCanvas canvas = (SurfaceInkCanvas)this.FindName("Canvas");

            // get the mode butten of the photo drame
            SurfaceButton buttonMode = (SurfaceButton)this.FindName("ButtonMode");

            // if the canvas is drawable, we are in draw mode, so switch to move mode
            if (canvas.IsHitTestVisible)
            {
                canvas.IsHitTestVisible = false;
                buttonMode.Content = FindResource("DrawIcon");
            }
            // otherwise, we are in move mode, so switch to draw mode
            else
            {
                canvas.IsHitTestVisible = true;
                buttonMode.Content = FindResource("MoveIcon");
            }
        }

        void Close(object sender, RoutedEventArgs e)
        {
            // get the actual photo frame
            ScatterViewItem frame = (ScatterViewItem)this.FindName("Frame");

            ((ScatterView)frame.Parent).Items.Remove(frame);
        }
    }
}
