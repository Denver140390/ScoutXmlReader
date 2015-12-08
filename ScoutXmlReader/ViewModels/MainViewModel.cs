using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using ScoutXmlReader.Models;
using ScoutXmlReader.Services;

namespace ScoutXmlReader.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly IDialogService _xmlFileDialogService = new OpenXmlFileDialogDialogService();

        public DataViewModel DataViewModel { get; } = new DataViewModel();
        public DataOverviewViewModel DataOverviewViewModel { get; } = new DataOverviewViewModel();

        public MainViewModel()
        {
            LoadXmlCommand = new ActionCommand(LoadXml);
            ClearCommand = new ActionCommand(Clear);
        }

        public ActionCommand LoadXmlCommand { get; private set; }
        private void LoadXml()
        {
            List<ScoutData> data = _xmlFileDialogService.Show().Select(ScoutData.FromXml).ToList();

            DataViewModel.LoadData(data);
            DataOverviewViewModel.LoadData(data);
        }

        public ActionCommand ClearCommand { get; private set; }
        private void Clear()
        {
            DataViewModel.Clear();
        }
    }
}
