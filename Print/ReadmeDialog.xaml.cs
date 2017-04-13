using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Print
{
    #region OpenFileExplorer

    /// Updates the scripts folder in the event a script is out of date.

    public partial class ReadmeDialog
    {
        #region Properties

        private readonly string _readMePath = @"C:\Users\Owner\Google Drive\EDSI\Updated Scripts\README.txt";
        public string PublicPath => _readMePath;

        #endregion

        #region Constructor

        public ReadmeDialog()
        {
            InitializeComponent();
            Readme.MinHeight = 100;
            Readme.MinWidth = 100;
            Readme.Text = File.ReadAllText(PublicPath);
            
            // Automatically resize window to fit README on screen.
            SizeToContent = SizeToContent.WidthAndHeight;
        }

        #endregion

        #region Methods

        #region Move Script

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            string Source = @"C:\Users\Owner\Google Drive\EDSI\Updated Scripts\Scripts";
            string Target = @"C:\AS400Report\Scripts";
            MoveDirectory(Source, Target);
        }

        #endregion

        #region Cancel

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var printer = new PrintReports();
            printer.Button_Cancel(sender, e);
        }

        #endregion

        #region Move Directory

        public static void MoveDirectory(string source, string target)
        {
            var stack = new Stack<Folders>();
            stack.Push(new Folders(source, target));

            while (stack.Count > 0)
            {
                var folders = stack.Pop();
                Directory.CreateDirectory(folders.Target);
                foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
                {
                    string targetFile = Path.Combine(folders.Target, Path.GetFileName(file));
                    if (File.Exists(targetFile)) File.Delete(targetFile);
                    File.Move(file, targetFile);
                }

                foreach (var folder in Directory.GetDirectories(folders.Source))
                {
                    stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
                }
            }
            Directory.Delete(source, true);
        }

        #endregion



        #endregion

    }

    #endregion

    #region Folders

    public class Folders
    {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public Folders(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }
    
    #endregion
}
