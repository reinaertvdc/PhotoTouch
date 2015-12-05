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
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            ((SurfaceButton)FindName("ButtonAddPhotoFrameTopLeft")).Content = FindResource("AddPhotoFrameIcon");
            ((SurfaceButton)FindName("ButtonAddPhotoFrameTopRight")).Content = FindResource("AddPhotoFrameIcon");
            ((SurfaceButton)FindName("ButtonAddPhotoFrameBottomRight")).Content = FindResource("AddPhotoFrameIcon");
            ((SurfaceButton)FindName("ButtonAddPhotoFrameBottomLeft")).Content = FindResource("AddPhotoFrameIcon");

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }










        void CreateNewPhotoFrame(object sender, RoutedEventArgs e)
        {
            // get the table on which the photo frames lie
            ScatterView table = (ScatterView)this.FindName("Table");

            if (table.Items.Count >= 12)
            {
                return;
            }

            // create a new photo frame
            PhotoFrame control = new PhotoFrame();
            ScatterViewItem frame = (ScatterViewItem)control.FindName("Frame");
            ((Grid)frame.Parent).Children.Remove(frame);

            // get a random rotation and position
            Random rn = new Random();
            int randomRotation = rn.Next(0, 90);
            int randomWidth = rn.Next(0, (int)Width / 2);
            int randomHeight = rn.Next(0, (int)Height / 2);

            // position the photo frame
            if (((SurfaceButton)sender).Name.Equals("ButtonAddPhotoFrameTopLeft"))
            {
                frame.Orientation = 90 + randomRotation;
                frame.Center = new Point(Width / 2 - randomWidth, Height / 2 - randomHeight);
            }
            else if (((SurfaceButton)sender).Name.Equals("ButtonAddPhotoFrameTopRight"))
            {
                frame.Orientation = 180 + randomRotation;
                frame.Center = new Point(Width / 2 + randomWidth, Height / 2 - randomHeight);
            }
            else if (((SurfaceButton)sender).Name.Equals("ButtonAddPhotoFrameBottomRight"))
            {
                frame.Orientation = 270 + randomRotation;
                frame.Center = new Point(Width / 2 + randomWidth, Height / 2 + randomHeight);
            }
            else if (((SurfaceButton)sender).Name.Equals("ButtonAddPhotoFrameBottomLeft"))
            {
                frame.Orientation = 0 + randomRotation;
                frame.Center = new Point(Width / 2 - randomWidth, Height / 2 + randomHeight);
            }

            // put the photo frame on the table
            table.Items.Add(frame);
        }
    }
}