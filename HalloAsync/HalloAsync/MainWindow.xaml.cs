using Microsoft.Data.SqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(200);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => { pb1.Value = i; Thread.Sleep(100); /*worker wartet auf UI*/ });
                    Thread.Sleep(20);
                }
                this.Dispatcher.Invoke(() => ((Button)sender).IsEnabled = !!!!!false);
            });
        }

        private void StartTastMitTS(object sender, RoutedEventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            ((Button)sender).IsEnabled = false;

            cts = new CancellationTokenSource();

            Task.Run(() => //<-- Worker
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Factory.StartNew(() => pb1.Value = i, CancellationToken.None, TaskCreationOptions.None, ts);
                    Thread.Sleep(200);

                    if (cts.IsCancellationRequested)
                        break; //+clean
                }

                Task.Factory.StartNew(() => ((Button)sender).IsEnabled = !!!!!false, CancellationToken.None, TaskCreationOptions.None, ts);
            });
        }

        CancellationTokenSource cts = null;

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAA(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                await Task.Delay(200);
            }
        }

        private async void GoDb(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();

            using var con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=Northwnd;Trusted_Connection=true");
            await con.OpenAsync();

            using var cmd = con.CreateCommand();
            cmd.CommandText = "WAITFOR DELAY '00:00:10';SELECT COUNT(*) FROM Employees";

            try
            {
                var count = await cmd.ExecuteScalarAsync(cts.Token);

                MessageBox.Show($"{count} Employees in DB");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erfolgreich abgrebrochen"); //hmmmmm
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        public long AlteLangsameFunktion(string text)
        {
            Thread.Sleep(5000);
            return text.Length * 26434567;
        }

        public Task<long> AlteLangsameFunktionAsync(string text)
        {
            return Task.Run<long>(() => AlteLangsameFunktion(text));
        }

        private async void GoAlt(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{AlteLangsameFunktion("Hallo")}");
            MessageBox.Show($"{await AlteLangsameFunktionAsync("Hallo")}");
        }
    }
}
