namespace AsyncDelegatesWForms
{
    public delegate void CopyingThread(string fromPath, string toPath);

    public partial class Form1 : Form
    {
        private SynchronizationContext sC = null;
        private CancellationTokenSource cts;

        public Form1()
        {
            InitializeComponent();
            sC = SynchronizationContext.Current;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fromPath.Text) || string.IsNullOrEmpty(toPath.Text))
            {
                MessageBox.Show("Some of the fields are empty",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(fromPath.Text) || !File.Exists(toPath.Text))
            {
                MessageBox.Show("One or both of the paths do not exist",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cts = new CancellationTokenSource();

            try
            {
                await CopyMethod(fromPath.Text, toPath.Text, cts.Token);
                ShowSuccess();
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Task is cancelled");
                sC.Send(_ => progressBar1.Value = 0,null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }

        private Task CopyMethod(string sourceFilePath, string destinationFilePath, CancellationToken token)
        {
            const int bufferSize = 4096;

            return Task.Factory.StartNew(() =>
            {
                token.ThrowIfCancellationRequested();

                using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                {
                    long totalBytes = sourceStream.Length;
                    long copiedBytes = 0;
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;

                    sC.Send(_ =>
                    {
                        progressBar1.Maximum = 100;
                        progressBar1.Value = 0;
                    }, null);

                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        token.ThrowIfCancellationRequested();

                        destinationStream.Write(buffer, 0, bytesRead);
                        copiedBytes += bytesRead;

                        int progress = (int)((copiedBytes * 100) / totalBytes);
                        sC.Send(_ => progressBar1.Value = progress, null);

                        Task.Delay(1000, token).Wait(token);
                    }
                }
            },
            token,                          
            TaskCreationOptions.LongRunning,  
            TaskScheduler.Default);
        }

        private void ShowSuccess()
        {
            MessageBox.Show("Copying is finished", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }
    }
}
