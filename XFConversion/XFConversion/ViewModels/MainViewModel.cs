using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFConversion.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool _isConnected;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _apiResponse;

        private AuthenticationManager _authenticationManager;
        private bool _showErrorMessage;
        private bool _showResponseMessage;
        private string _errorMessage;

        #endregion

        #region Properties

        public bool ShowErrorMessage {
            get { return _showErrorMessage; }
            set { _showErrorMessage = value; OnPropertyChanged(); }
        }

        public bool ShowResponseMessage {
            get { return _showResponseMessage; }
            set { _showResponseMessage = value; OnPropertyChanged(); }
        }

        public string ErrorMessage {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool IsConnected {
            get { return _isConnected; }
            set {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName {
            get { return _firstName; }
            set {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName {
            get { return _lastName; }
            set {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email {
            get { return _email; }
            set {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ApiResponse {
            get { return _apiResponse; }
            set {
                if (_apiResponse != value)
                {
                    _apiResponse = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand QueryApiCommand { get; private set; }

        public MainViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
            LogoutCommand = new Command(LogoutAsync);
            QueryApiCommand = new Command(async () => await QueryApiAsync());
        }
        public void Load()
        {
            AuthenticationManager.SetConfiguration();
            _authenticationManager = new AuthenticationManager();
        }

        public async Task LoginAsync()
        {
            ResetErrorMessage();
            await _authenticationManager.LoginAsync();

            var user = _authenticationManager.UserInfo;
            IsConnected = true;
            FirstName = user.GivenName;
            LastName = user.FamilyName;
            Email = user.DisplayableId;
            ApiResponse = null;
        }

        public void LogoutAsync()
        {
            ResetErrorMessage();
            _authenticationManager.Logout();
            IsConnected = false;
            FirstName = null;
            LastName = null;
            Email = null;
            ApiResponse = null;
        }

        private void ResetErrorMessage()
        {
            ErrorMessage = "";
            ShowErrorMessage = false;
            ApiResponse = "";
            ShowResponseMessage = false;
        }
        public async Task QueryApiAsync()
        {
            ResetErrorMessage();
            var httpClient = _authenticationManager.CreateHttpClient();
            
            var response = await httpClient.GetAsync(new Uri(Configuration.ApiUri));


            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadAsStringAsync();
                ApiResponse = value;
                ShowResponseMessage = true;
            }
            else
            {
                ApiResponse = $"HTTP {(int)response.StatusCode} - {Enum.GetName(typeof(HttpStatusCode), response.StatusCode)}";
                ErrorMessage = ApiResponse;
                ShowErrorMessage = true;
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
