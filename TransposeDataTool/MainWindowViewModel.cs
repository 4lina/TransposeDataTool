using Microsoft.Win32;
using Reactive.Bindings;

namespace TransposeDataTool
{
    internal class MainWindowViewModel
    {
        private readonly MainWindowModel mainWindowModel;

        public MainWindowViewModel()
        {
            this.mainWindowModel = new MainWindowModel();
            this.OpenFileButton = new ReactiveCommand();
            this.OpenFileButton.Subscribe(this.OpenFile_Click);
        }

        private void OpenFile_Click()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // receive file name
                var fileName = openFileDialog.FileName;

                // read data
                var data = this.mainWindowModel.ReadFile(fileName);

                // transpose data content 
                var transposedData = this.mainWindowModel.TransposeData(data);

                // save transposed table into new file
                this.mainWindowModel.SaveData(fileName, transposedData);
            }
        }

        public ReactiveCommand OpenFileButton { get; }
    }
}
