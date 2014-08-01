using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using ServiceDashboard;

namespace Dashboard
{
    public static class DashboardFunctions
    {
        public static void OverlayAlert(Window window, bool overlayAlertOn, bool paused)
        {
            if (paused)
            {
                window.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Paused;
                window.TaskbarItemInfo.Overlay =  ResourceFile.bt_pause.ToImageSource();
                window.TaskbarItemInfo.ProgressValue = 1;
            }
            else
            {
                if (overlayAlertOn)
                {
                    window.TaskbarItemInfo.Overlay = ResourceFile.bt_attention.ToImageSource();
                    window.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;
                    window.TaskbarItemInfo.ProgressValue = 1;
                }
                else
                {
                    window.TaskbarItemInfo.Overlay = ResourceFile.bt_play.ToImageSource();
                    window.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
                    window.TaskbarItemInfo.ProgressValue = 1;
                }
            }
        }

        public static string GetFolder(string folder)
        {
            string retValue = folder;
            if (string.IsNullOrEmpty(retValue))
            {
                retValue = System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            return retValue;
        }

        public static ImageSource ToImageSource(this Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        public static T DeserializeFromFile<T>(string fileName)
        {
            T rValue = default(T);
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentException("The filename is missing");
                if (File.Exists(fileName))
                {
                    using (FileStream fsr = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        var dsObj = serializer.Deserialize(fsr);
                        var value = Convert.ChangeType(dsObj, typeof(T));
                        rValue = (T)value;                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem deserializing the file: " + fileName, ex);
            }
            return rValue;
        }

        public static void SerializeToFile<T>(T obj, string fileName)
        {
            try
            {
                string newFileName = string.Empty;
                using (FileStream fsw = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(fsw, obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem serializing the object to a file", ex);
            }
        }
    }
}
