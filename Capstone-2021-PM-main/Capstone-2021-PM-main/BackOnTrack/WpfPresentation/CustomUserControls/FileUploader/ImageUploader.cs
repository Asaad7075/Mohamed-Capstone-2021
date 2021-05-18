using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfPresentation.CustomUserControls
{
    public class ImageUploader
    {
        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/20
        /// 
        /// Allows the image to be pulled if 
        /// there is a need to display it after
        /// uploading it.
        /// </summary>
        private BitmapImage _chosenImage;
        public BitmapImage Image
        {
            get => _chosenImage;
            private set
            {
                _chosenImage = value;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/20
        /// 
        /// Allows the byte array of the
        /// image to be pulled for data
        /// storage purposes.
        /// </summary>
        private byte[] _byteImage;
        public byte[] ByteImage
        {
            get => _byteImage;
            private set
            {
                _byteImage = value;
            }
        }

        private FileUploader _fileUploader;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/23
        /// 
        /// Constructor to initialize the image 
        /// uploader connected to the user tool
        /// </summary>
        /// <param name="fileUploader"></param>
        public ImageUploader(FileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }


        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/20
        /// 
        /// Private helper method that constrains 
        /// the users choices to jpg and png.
        /// </summary>
        /// <param name="openFileDialog"></param>
        public void UploadImage(OpenFileDialog openFileDialog)
        {
            try
            {
                _chosenImage = new BitmapImage();
                openFileDialog.Filter = "Image files files (*.jpg;*.png)|*.jpeg;*.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                if (openFileDialog.ShowDialog() == true)
                {
                    ByteImage = File.ReadAllBytes(openFileDialog.FileName);
                    using (var ms = new MemoryStream(ByteImage))
                    {
                        _chosenImage.BeginInit();
                        _chosenImage.UriSource = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);
                        _chosenImage.CacheOption = BitmapCacheOption.OnLoad;
                        _chosenImage.StreamSource = ms;
                        _chosenImage.EndInit();
                    }

                    if (_chosenImage != null)
                    {
                        _fileUploader.txtblkFileName.Text = GetFileDisplayName(openFileDialog.SafeFileName);
                    }
                    if (_chosenImage == null)
                    {
                        _fileUploader.txtblkFileName.Text = "No File Uploaded...";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image was unable to be uploaded." + "\n\n" +
                    ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message,
                    "Upload Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/23
        /// 
        /// Private helper method to allow
        /// long names to be trimmed.
        /// </summary>
        /// <param name="safeFileName"></param>
        /// <returns></returns>
        private string GetFileDisplayName(string safeFileName)
        {
            if (safeFileName.Length > 20)
            {
                return safeFileName.Substring(0, 20) + "...";
            }
            else
            {
                return safeFileName;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/20
        /// 
        /// Notifies the user if the image
        /// attached to the image uploader
        /// has been pointed to null and reset. A return
        /// of true means it was pointed to null,
        /// and a return of false means it did 
        /// not need to be garbage collected.
        /// </summary>
        /// <returns></returns>
        public bool GarbageCollectImage()
        {
            if (Image != null || ByteImage != null) // Garbage-collect by pointing it to null
            {
                Image = null;
                ByteImage = null;
                _fileUploader.txtblkFileName.Text = "No File Uploaded...";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
