using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Xml;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
using System.Net.NetworkInformation;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Collections.Specialized;
using Source;


namespace HeartbeatUINew
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            FileSystemWatcher watcher = new FileSystemWatcher();

            InitializeComponent();

            this.Loaded += async delegate (object s, RoutedEventArgs e)
            {
                

                





                /* Update detector
                WebClient web = new WebClient();
                if (web.DownloadString("https://www.ch3rry.red/storage/heartbeat/check") != "CH3RRY-DO5AU7VSZ9HKO0297CNE0RTJWNOAMG51NQJT")
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Please run the installer to update!", "Update Available", MessageBoxButton.OK, MessageBoxImage.Information); // Add custom Box
                    Environment.Exit(0);


                }
                else
                {

                    Console.WriteLine("Checking For Updates");
                    await Task.Delay(1000);
                    Console.WriteLine("No Updates Found!");
                } */








                // Startup
                UpdateSyntax();
                
                RefreshBox();
                openwindow();
            };

            // Refresh Box Trigger
            watcher.Path = @"Scripts\";
            watcher.Changed += delegate (object s, FileSystemEventArgs e)
            {
                RefreshBox();
            };
            watcher.Created += delegate (object s, FileSystemEventArgs e)
            {
                RefreshBox();
            };
            watcher.Deleted += delegate (object s, FileSystemEventArgs e)
            {
                RefreshBox();
            };
            watcher.Renamed += delegate (object s, RenamedEventArgs e)
            {
                RefreshBox();
            };
            watcher.EnableRaisingEvents = true;

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }



        private void UpdateSyntax()
        {
            Stream xshd_stream = File.OpenRead(Environment.CurrentDirectory + @"\bin\" + "Lua.xshd");
            XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
            avalon.SyntaxHighlighting = HighlightingLoader.Load(xshd_reader, HighlightingManager.Instance);

            xshd_reader.Close();
            xshd_stream.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

      

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            avalon.Clear();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != 0)
            {
                File.WriteAllText(dialog.FileName, avalon.Text);
            }
            else
            {
                return;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() != 0)
            {
                var s = dialog.OpenFile();
                using (StreamReader sr = new StreamReader(s))
                {
                    avalon.Text = sr.ReadToEnd();
                }
            }
            else
            {
                return;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }





        private void RefreshBox()
        {
            this.Dispatcher.Invoke(() =>
            {
                scriptbox.Items.Clear();
                DirectoryInfo dinfo = new DirectoryInfo("./Scripts");
                FileInfo[] Files = dinfo.GetFiles("*.txt");
                FileInfo[] Files2 = dinfo.GetFiles("*.lua");
                foreach (FileInfo file in Files)
                {
                    scriptbox.Items.Add(file.Name);
                }
                foreach (FileInfo file in Files2)
                {
                    scriptbox.Items.Add(file.Name);
                }
            });
        }



        private async void Button_Click_1Async(object sender, RoutedEventArgs e)
        {


            await Task.Delay(3000);


            this.Close();
        }


        private async void button2_Copy_Click(object sender, RoutedEventArgs e)
        {



           
        }

        private void scriptbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scriptbox.SelectedIndex != -1)
            {
                avalon.Text = File.ReadAllText("scripts\\" + scriptbox.SelectedItem.ToString());
            }
        }

        

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Topmost = false;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Killing RobloxPlayerBeta");
            foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                process.Kill();
            }
        }

        private void scriptclose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            
        }



       private void openwindow()
        {
           
        }

        private void rpcbox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void rpcbox_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void button4_Copy2_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}


