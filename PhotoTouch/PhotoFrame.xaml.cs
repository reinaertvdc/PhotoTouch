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
        }










        void SwitchMode(object sender, RoutedEventArgs e)
        {
            // get the actual photo frame
            ScatterViewItem frame = (ScatterViewItem)this.FindName("Frame");
            
            SurfaceInkCanvas canvas = (SurfaceInkCanvas)this.FindName("Canvas");

            SurfaceButton buttonMode = (SurfaceButton)this.FindName("ButtonMode");

            if (canvas.IsHitTestVisible)
            {
                canvas.IsHitTestVisible = false;
                buttonMode.Content = FindResource("DrawIcon");
            }
            else
            {
                canvas.IsHitTestVisible = true;
                buttonMode.Content = FindResource("MoveIcon");
            }
        }
    }
}
