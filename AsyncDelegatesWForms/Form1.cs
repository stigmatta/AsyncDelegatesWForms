namespace AsyncDelegatesWForms
{
    public delegate void CopyingThread(string fromPath, string toPath);
    public partial class Form1 : Form
    {
        private SynchronizationContext sC = null;
        CopyingThread copyThread = null;
        AsyncCallback showMessage = null;
        public Form1()
        {
            InitializeComponent();
            sC = SynchronizationContext.Current;
            copyThread = CopyMethod;
            showMessage = ShowSuccess;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fromPath.Text) || String.IsNullOrEmpty(toPath.Text))
            {
                MessageBox.Show("Some of the fields are empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(fromPath.Text) || !File.Exists(toPath.Text))
            {
                MessageBox.Show("One or both of the paths do not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IAsyncResult ar = copyThread.BeginInvoke(fromPath.Text, toPath.Text, showMessage,null);
        }

        private void CopyMethod(string sourceFilePath, string destinationFilePath)
        {
            const int bufferSize = 4096;
            try
            {
                using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                    {
                        long totalBytes = sourceStream.Length;
                        long copiedBytes = 0; 
                        byte[] buffer = new byte[bufferSize];
                        int bytesRead;

                        sC.Send(_ => progressBar1.Maximum = 100, null);
                        sC.Send(_ => progressBar1.Value = 0, null);

                        while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            destinationStream.Write(buffer, 0, bytesRead);
                            copiedBytes += bytesRead;

                            int progress = (int)((copiedBytes * 100) / totalBytes);

                            sC.Send(_ => progressBar1.Value = progress, null);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при копировании файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ShowSuccess(IAsyncResult ar)
        {
            MessageBox.Show("Copying was successful","Finish",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
