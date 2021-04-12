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
using AnemoAPI;
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
                

                Console.WriteLine("Checking For First Run");
                if (Source.Properties.Settings.Default.FirstRun == false)
                {
                    await Task.Delay(1000);
                    Console.WriteLine("First Run == True Joining Discord");
                    System.Diagnostics.Process.Start("https://discord.gg/XzC6yDQ5St");
                    Source.Properties.Settings.Default.FirstRun = true;
                    Source.Properties.Settings.Default.Save();
                }





                // Update detector
                WebClient web = new WebClient();
                if (web.DownloadString("https://www.ch3rry.red/storage/heartbeat/check") != "CH3RRY-DO5AU7VSZ9HKO0297CNE0RTJWNOAMG51NQJTS")
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Please run the installer to update!", "Update Available", MessageBoxButton.OK, MessageBoxImage.Information); // Add custom Box
                    Environment.Exit(0);


                }
                else
                {

                    Console.WriteLine("Checking For Updates");
                    await Task.Delay(1000);
                    Console.WriteLine("No Updates Found!");
                }








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

        Anemo apiAenmo = new Anemo();

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



            apiAenmo.ExecuteScript(avalon.Text);
        }





        


        public DiscordRpcClient client;

        //Called when your application first starts.
        //For example, just before your main loop, on OnEnable for unity.
        void Initialize()
        {
            /*
            Create a Discord client
            NOTE: 	If you are using Unity3D, you must use the full constructor and define
                     the pipe connection.
            */
            client = new DiscordRpcClient("824695565274710046");

            //Set the logger
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            //Subscribe to events
            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Starting RPC!", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Started!");
            };

            //Connect to the RPC
            client.Initialize();

            //Set the rich presence
            //Call this as many times as you want and anywhere in your code.
            client.SetPresence(new RichPresence()
            {
                Details = "Using Source",
                State = "from ch3rry.red",
                Assets = new Assets()
                {
                    LargeImageKey = "perfect_icon_512x512",
                    LargeImageText = "www.ch3rry.red",
                    SmallImageKey = "Discord-Logo-White_512x512"
                }
            });
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
            apiAenmo.ExecuteScript(dexscript.Text);
        }



       private void openwindow()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("If you want to support Source go to\nwww.ch3rry.red/donate", "Support Us?", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void rpcbox_Checked(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void rpcbox_Unchecked(object sender, RoutedEventArgs e)
        {
            client.Dispose();
        }

        private void button4_Copy2_Click(object sender, RoutedEventArgs e)
        {
            apiAenmo.InjectAnemo();
            /*if (apiAenmo.IsInjected())
            {
                apiAenmo.ExecuteScript(File.ReadAllText("scripts\\"));
            }
            else
            {
                return;
            }*/
            
        }

        
    
    }
}


