using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfPresentation.CustomUserControls
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/20
    /// 
    /// Interaction logic for FileUploader.xaml
    /// </summary>
    public partial class FileUploader : UserControl
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/22
        /// 
        /// Customizable option to allow
        /// file uploader to be set to a particular
        /// upload type. Public setter to allow for
        /// initializing specified mode
        /// </summary>
        private FileModes _selectedMode;
        public FileModes SelectedMode
        {
            get => _selectedMode;
            set
            {
                if (value != FileModes.Disabled)
                {
                    btnFileDialogButton.IsEnabled = true;
                }
                _selectedMode = value;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/23
        /// 
        /// Separates the image loader logic
        /// from the user control.
        /// </summary>
        private ImageUploader _imageUploader;
        public ImageUploader ImageUploader
        {
            get => _imageUploader;
            private set
            {
                _imageUploader = value;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/20
        /// 
        /// Zero-argument constructor. Default
        /// file uploader is unusable unless it
        /// is instantiated with a specified mode.
        /// </summary>
        public FileUploader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/20
        /// 
        /// Private event helper to guide the users choice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileDialogButton_Click(object sender, RoutedEventArgs e)
        {
            switch (SelectedMode)
            {
                case FileModes.ImageMode:
                    _imageUploader = new ImageUploader(this);
                    _imageUploader.UploadImage(new OpenFileDialog());
                    break;
                default:
                    break;
            }
        }
    }
}
