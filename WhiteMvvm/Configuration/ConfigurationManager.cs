using System;
using Acr.UserDialogs;

namespace WhiteMvvm.Configuration
{
    public sealed class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> Lazy = new Lazy<ConfigurationManager>(() => new ConfigurationManager());
        private string _viewsFolderName;
        private string _viewModelFolderName;
        private string _viewsFileName;
        private string _loadingDisplay;
        private string _viewModelFileName;
        private string _okDisplay;
        private string _cancelDisplay;
        private string _errorTitleDisplay;
        private string _connectionErrorDisplay;

        public static ConfigurationManager Current => Lazy.Value;
        private ConfigurationManager()
        {
        }
        public bool UseBaseIndicator { get; set; } = true;
        public MaskType IndicatorMaskType { get; set; } = MaskType.Gradient;
        public string ViewsFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewsFolderName))
                    return "Views";
                return _viewsFolderName;
            }
            set => _viewsFolderName = value;
        }
        public string ViewModelFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewModelFolderName))
                    return "ViewModels";
                return _viewModelFolderName;
            }
            set => _viewModelFolderName = value;
        }
        public string ViewsFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewsFileName))
                    return "Page";
                return _viewsFileName;
            }
            set => _viewsFileName = value;
        }
        public string LoadingDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(_loadingDisplay))
                    return "Loading...";
                return _loadingDisplay;
            }
            set => _loadingDisplay = value;
        }

        public string OkDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(_okDisplay))
                    return "Ok";
                return _okDisplay;
            }
            set => _okDisplay = value;
        }

        public string CancelDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(_cancelDisplay))
                    return "Cancel";
                return _cancelDisplay;
            }
            set => _cancelDisplay = value;
        }

        public string ErrorTitleDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(_errorTitleDisplay))
                    return "Error";
                return _errorTitleDisplay;
            }
            set => _errorTitleDisplay = value;
        }

        public string ConnectionErrorDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionErrorDisplay))
                    return "Internet connection lost !";
                return _connectionErrorDisplay;
            }
            set => _connectionErrorDisplay = value;
        }

        public string ViewModelFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewModelFileName))
                    return "ViewModel";
                return _viewModelFileName;
            }
            set => _viewModelFileName = value;
        }
    }
}
