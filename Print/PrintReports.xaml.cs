using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Print
{
    /// <summary>
    /// Interaction logic for PrintReports.xaml
    /// </summary>
    public partial class PrintReports : Window
    {
        #region Constructor

        public PrintReports()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Methods

        private bool Button_Okay(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure?";
            string caption = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?",
                "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
            else if (result == MessageBoxResult.No)
            {
                // No code here
            }
            else
            {
                // Cancel code here
            }

        }

        private void OnClickDefault(object sender, RoutedEventArgs e)
        {
            bool Proceed = false;
            Proceed = Button_Okay(sender, e);
            ComboBox optionsBox = PrintOptions;
            string SelectedValue = optionsBox.SelectionBoxItem.ToString();
            

            if (Proceed)
            {
                switch (SelectedValue)
                {
                    case ("Batchsheets"):
                        {
                            ComboBoxItem_Batchsheets(sender, e);
                            break;
                        }
                    case ("MonthlyReports"):
                        {
                            ComboBoxItem_MonthlyReports(sender, e);
                            break;
                        }
                    case ("Statements"):
                        {
                            ComboBoxItem_Statements(sender, e);
                            break;
                        }
                    case ("PerkinsReassignments"):
                        {
                            ComboBoxItem_PerkinsReassignments(sender, e);
                            break;
                        }
                    case ("InterestPaid"):
                        {
                            Console.WriteLine(@"Selection was invalid, terminating.");
                            break;
                        }
                    case ("All"):
                        {
                            ComboBoxItem_All(sender, e);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(@"Selection was invalid, terminating.");
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("Something went wrong!");
            }

            Button_Cancel(sender, e);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region ComboBox Item Methods

        private void ComboBoxItem_Please_Select_an_Option(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please Make a selection before pressing \"Okay\".");
        }

        private void ComboBoxItem_Batchsheets(object sender, RoutedEventArgs e)
        {
            RunBatch Script = new RunBatch("Batchsheets");
            Script.ExecuteCommand();
        }

        private void ComboBoxItem_MonthlyReports(object sender, RoutedEventArgs e)
        {
            RunBatch Script = new RunBatch("MonthlyReports");
            Script.ExecuteCommand();
        }

        private void ComboBoxItem_Statements(object sender, RoutedEventArgs e)
        {
            RunBatch Script = new RunBatch("Statements");
            Script.ExecuteCommand();
        }

        private void ComboBoxItem_PerkinsReassignments(object sender, RoutedEventArgs e)
        {
            RunBatch Script = new RunBatch("PerkinsReassignments");
            Script.ExecuteCommand();
        }

        private void ComboBoxItem_All(object sender, RoutedEventArgs e)
        {
            //ComboBoxItem_Batchsheets(sender, e);
            //ComboBoxItem_MonthlyReports(sender, e);
            //ComboBoxItem_Statements(sender, e);
            //ComboBoxItem_Statements(sender, e);
            RunBatch Script = new RunBatch("All");
            Script.ExecuteCommand();
        }

        #endregion
        
    }

    public class RunBatch
    {
        #region Properties

        private string BatchPath { get; set; }
        private const string Path = @"C:\AS400Report\Scripts";
        private string BatchName { get; set; }

        #endregion

        #region Constructor

        public RunBatch(string ReportType)
        {
            switch (ReportType)
            {
                case ("Batchsheets"):
                    {
                        BatchName = "prtbatch.bat";
                        break;
                    }
                case ("MonthlyReports"):
                    {
                        BatchName = "prtq.bat";
                        break;
                    }
                case ("Statements"):
                    {
                        BatchName = "getstmt.bat";
                        break;
                    }
                case ("PerkinsReassignments"):
                    {
                        BatchName = "get_perkins_reassignments.bat";
                        break;
                    }
                case ("InterestPaid"):
                    {
                        BatchName = "Error";
                        Console.WriteLine(@"Selection was invalid, terminating.");
                        break;
                    }
                case ("All"):
                    {
                        BatchName = "PrintAll.bat";
                        break;
                    }
                default:
                    {
                        BatchName = "Error";
                        Console.WriteLine(@"Selection was invalid, terminating.");
                        break;
                    }
            }

            BatchPath = Path + "\\" + BatchName;
        }

        #endregion

        #region Methods

        public void ExecuteCommand()
        {
            var processInfo = new ProcessStartInfo(/*"cmd.exe", "/c " + BatchPath*/)
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = BatchPath,
                WorkingDirectory = Path
            };

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }

        #endregion

    }
}
