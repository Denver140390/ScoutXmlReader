using System;
using Microsoft.Win32;

namespace ScoutXmlReader.Services
{
    public class OpenXmlFileDialogDialogService : IDialogService
    {
        private readonly OpenFileDialog _fileDialog;

        public OpenXmlFileDialogDialogService()
        {
            _fileDialog = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                DefaultExt = "xml",
                Multiselect = true
            };
        }

        public String[] Show()
        {
            //TODO Async loading
            _fileDialog.ShowDialog();
            return _fileDialog.FileNames;
        }
    }
}
