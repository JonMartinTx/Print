using System;
using System.Diagnostics;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace Print
{

    #region PrintReports

    /// A Simple GUI Interface for printing EDSI Documents

    public partial class PrintReports
    {
        #region Constructor

        public PrintReports()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Methods

        #region Okay

        private bool Button_Okay()
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

        #endregion

        #region Cancel

        public void Button_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?",
                "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                Close();
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

        #endregion

        #region Error

        private void Button_Error(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(@"Your Scripts Folder is out of date. Please locate the README file located at 'C:\Users\Owner\Google Drive\EDSI\Updated Scripts' for more information. Click 'OK' to locate the README.",
                "Update Required", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Close();
            }
            else if (result == MessageBoxResult.Cancel)
            {
                Button_Cancel(sender, e);
            }
            else
            {
                // Cancel code here
            }

        }

        #endregion

        #region Test Updater

        public void Button_TEST(object sender, RoutedEventArgs e)
        {

            ReadmeDialog updaterTest = new ReadmeDialog();
            updaterTest.ShowDialog();

            //Restart Print.exe after updating the Scripts
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();

        }

        #endregion

        #region Default

        private void OnClickDefault(object sender, RoutedEventArgs e)
        {
            var proceed = Button_Okay();
            var optionsBox = PrintOptions;
            var selectedValue = optionsBox.SelectionBoxItem.ToString();


            if (proceed)
            {
                switch (selectedValue)
                {
                    case ("Batchsheets"):
                        {
                            ComboBoxItem_Batchsheets();
                            break;
                        }
                    case ("Monthly Reports"):
                        {
                            ComboBoxItem_MonthlyReports();
                            break;
                        }
                    case ("Invoices"):
                        {
                            ComboBoxItem_Invoices();
                            break;
                        }
                    case ("Collection Agency Reports"):
                        {
                            ComboBoxItem_CollectionAgencyReports();
                            break;
                        }
                    case ("Statements"):
                        {
                            ComboBoxItem_Statements();
                            break;
                        }
                    case ("Perkins Reassignments"):
                        {
                            ComboBoxItem_PerkinsReassignments();
                            break;
                        }
                    case ("Interest Paid"):
                        {
                            try
                            {
                                ComboBoxItem_InterestPaid();
                            }
                            catch
                            {
                                Console.WriteLine(@"Your Scripts Folder is out of date. Please locate the README file located at 'C:\Users\Owner\Google Drive\EDSI\Updated Scripts' for more information.");
                                ReadmeDialog Updater = new ReadmeDialog();
                                Updater.ShowDialog();
                                ComboBoxItem_MonthlyReports();
                            }

                            break;
                        }
                    case ("All"):
                        {
                            ComboBoxItem_All();
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

        #endregion

        #endregion

        #region ComboBox Item Methods

        private void ComboBoxItem_Please_Select_an_Option(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please Make a selection before pressing \"Okay\".");
        }

        private void ComboBoxItem_Batchsheets()
        {
            RunBatch script = new RunBatch("Batchsheets");
            script.ExecuteCommand();
        }

        private void ComboBoxItem_MonthlyReports()
        {
            RunBatch script = new RunBatch("Monthly Reports");
            script.ExecuteCommand();
        }

        private void ComboBoxItem_Invoices()
        {
            RunBatch script = new RunBatch("Invoices");
            script.ExecuteCommand();
        }

        private void ComboBoxItem_CollectionAgencyReports()
        {
            RunBatch script = new RunBatch("Collection Agency Reports");
            script.ExecuteCommand();
        }

        private void ComboBoxItem_InterestPaid()
        {
            RunBatch script = new RunBatch("Interest Paid");
            script.ExecuteCommand();
        }

        private void ComboBoxItem_Statements()
        {
            RunBatch script = new RunBatch("Statements");
            script.ExecuteCommand();
        }

        private void ComboBoxItem_PerkinsReassignments()
        {
            RunBatch script = new RunBatch("Perkins Reassignments");
            script.ExecuteCommand();
        }

        private void ComboBoxItem_All()
        {
            //ComboBoxItem_Batchsheets(sender, e);
            //ComboBoxItem_MonthlyReports(sender, e);
            //ComboBoxItem_Statements(sender, e);
            //ComboBoxItem_Statements(sender, e);
            RunBatch script = new RunBatch("All");
            script.ExecuteCommand();
        }

        #endregion

    }

    #endregion

    #region RunBatch

    public class RunBatch
    {

        #region Properties

        private string BatchPath { get; set; }
        private const string Path = @"C:\AS400Report\Scripts";
        private string BatchName { get; set; }

        #endregion

        #region Constructor

        public RunBatch(string reportType)
        {
            switch (reportType)
            {
                case ("Batchsheets"):
                    {
                        BatchName = "prtbatch.bat";
                        break;
                    }
                case ("Monthly Reports"):
                    {
                        BatchName = "prtq.bat";
                        break;
                    }
                case ("Collection Agency Reports"):
                    {
                        BatchName = "prtq.bat";
                        break;
                    }
                case ("Invoices"):
                    {
                        BatchName = "prtq.bat";
                        break;
                    }
                case ("Statements"):
                    {
                        BatchName = "getstmt.bat";
                        break;
                    }
                case ("Perkins Reassignments"):
                    {
                        BatchName = "get_perkins_reassignments.bat";
                        break;
                    }
                case ("Interest Paid"):
                    {
                        BatchName = "getintrpaid.bat";
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

            var process = new Process(); //Process.Start(processInfo);

            process.OutputDataReceived += ConsumeData;

            try
            {
                process.StartInfo = processInfo;
                process.Start();
                process.BeginOutputReadLine();
                //process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                //Console.WriteLine("output>>" + e.Data);
                //process.BeginOutputReadLine();

                //process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                //    Console.WriteLine("error>>" + e.Data);
                //process.BeginErrorReadLine();
                process.WaitForExit();
            }
            finally
            {
                process.OutputDataReceived -= ConsumeData;
            }

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }

        private void ConsumeData(object sendingProcess,
            DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrWhiteSpace(outLine.Data))
                Console.WriteLine(outLine.Data);
        }

        #endregion

    }

    #endregion

}
