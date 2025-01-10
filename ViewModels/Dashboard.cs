using BaseApp.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using WpfHelpers;
using WpfHelpers.Controls;


namespace BaseApp.ViewModels
{
    public class Dashboard : ViewModelBase
    {
        public ConnectionService _connectionService { get; private set; }
        
        public ICommand ConnectCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand GetDataCommand { get; set; }
        public ICommand SearchCommand { get; set; }


        private string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        private ObservableCollection<SubRegions> _filteredApiData;
        public ObservableCollection<SubRegions> FilteredApiData
        {
            get { return _filteredApiData; }
            set
            {
                _filteredApiData = value;
                OnPropertyChanged(nameof(FilteredApiData));
            }
        }

        private string _apiUrl;
        public string ApiUrl
        {
            get => _apiUrl;
            set
            {
                _apiUrl = value;
                OnPropertyChanged(nameof(ApiUrl));
            }
        }

        private ObservableCollection<SubRegions> _apiData;
        public ObservableCollection<SubRegions> ApiData
        {
            get { return _apiData; }
            set
            {
                _apiData = value;
                OnPropertyChanged(nameof(ApiData));
            }
        }

        private bool _isStartButtonEnabled = true;
        public bool IsStartButtonEnabled
        {
            get { return _isStartButtonEnabled; }
            set
            {
                _isStartButtonEnabled = value;
                OnPropertyChanged(nameof(IsStartButtonEnabled));
            }
        }

        private bool _isStopButtonEnabled = false;
        public bool IsStopButtonEnabled
        {
            get { return _isStopButtonEnabled; }
            set
            {
                _isStopButtonEnabled = value;
                OnPropertyChanged(nameof(IsStopButtonEnabled));
            }
        }

        private ObservableCollection<string> _templates;
        public ObservableCollection<string> Templates
        {
            get { return _templates; }
            set
            {
                _templates = value;
                OnPropertyChanged(nameof(Templates));  
            }
        }



        private string _selectedTemplate;
        public string SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                _selectedTemplate = value;
                OnPropertyChanged(nameof(SelectedTemplate));  // Notify of changes to the property
            }
        }

        private bool _isStartCommandSent = false;
        public bool IsStartCommandSent
        {
            get { return _isStartCommandSent; }
            set
            {
                _isStartCommandSent = value;
                OnPropertyChanged(nameof(IsStartCommandSent));
            }
        }


        public Dashboard()
        {
            ApiData = new ObservableCollection<SubRegions>();
            FilteredApiData = new ObservableCollection<SubRegions>();

            _connectionService = new ConnectionService();
            Templates = new ObservableCollection<string>();

            StartCommand = new RelayCommand(SendStartCommand);
            StopCommand = new RelayCommand(SendStopCommand);

            SendCommand = new RelayCommand(SendDataToServer);
            GetDataCommand = new RelayCommand(FetchApiData);
            SearchCommand = new RelayCommand(SearchData);


            ConnectCommand = new DelegateCommand(async (param) =>
            {
                string settingsExcel = Bootstrap.settingPath;

                if (!File.Exists(settingsExcel))
                {
                    System.Windows.MessageBox.Show($"Settings file not found at {settingsExcel}. Please ensure the file exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var workbook = new XLWorkbook(settingsExcel);
                    var worksheet = workbook.Worksheet(1);

                    // Assuming the first row contains headers and data starts from the second row
                    string ipAddress = worksheet.Cell(2, 1).GetValue<string>();
                    int port = worksheet.Cell(2, 2).GetValue<int>();

                    bool isConnected = _connectionService.Connect(ipAddress, port);
                    if (isConnected)
                    {
                        System.Windows.MessageBox.Show("Connected to the server successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Failed to connect to the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (_connectionService != null && _connectionService.IsConnected)
                    {
                        string command = "GJL<CR>";
                        _connectionService.Send(command);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error while connecting to the server: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                string response = await _connectionService.Receive();
                ParseTemplatesFromResponse(response);
            });


        }
        

        private async void FetchApiData()
        {
            if (string.IsNullOrWhiteSpace(ApiUrl))
            {
                System.Windows.MessageBox.Show("Please enter a valid API URL.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var link = new Uri(ApiUrl); // API URL
                                                
                    var response = await httpClient.GetStringAsync(link);
                    var data = JsonConvert.DeserializeObject<List<SubRegions>>(response);

                    foreach (var item in data)
                    {
                        ApiData.Add(item);
                    }

                    System.Windows.MessageBox.Show("Data loaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Failed to fetch data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ParseTemplatesFromResponse(string response)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(response)) return;

                string[] parts = response.Split('|');

                // Validate that we have a correct response format
                if (parts.Length < 3) return;

                // The templates are located from index 2 onwards
                var templates = parts.Skip(2).Take(parts.Length - 3); // Skip "JBL" and the count, and exclude the <CR>

                // Clear any existing templates and add the new ones
                Templates.Clear();
                foreach (var template in templates)
                {
                    Templates.Add(template);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error parsing response: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        //Send Start Command.
        private async void SendStartCommand()
        {
            try
            {
                if (_connectionService != null && _connectionService.IsConnected)
                {
                    if (string.IsNullOrEmpty(SelectedTemplate))
                    {
                        System.Windows.MessageBox.Show("Please select a template before starting", "Template Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    string Startcommand = $"SST|1|<CR>";
                    string Selcommand = $"SEL|{SelectedTemplate}|<CR>";

                    _connectionService.Send(Selcommand);

                    // wait for ACK
                    string response = await _connectionService.Receive();
                    if (response == "ACK")
                    {
                        _connectionService.Send(Startcommand);
                        // Set the flag to true when the Start command is sent successfully
                        IsStartCommandSent = true;
                    }

                    // Disable Start button and enable Stop button
                    IsStartButtonEnabled = false;
                    IsStopButtonEnabled = true;
                    //System.Windows.MessageBox.Show("Printer Start.", "Send the Data to Printer to print.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Please connect to the server before starting the printer.", "Connection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Failed to send command: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendStopCommand()
        {
            try
            {
                string settingsExcel = Bootstrap.settingPath;
                if (_connectionService != null && _connectionService.IsConnected)
                {
                    string command = "SST|4|<CR>";

                    _connectionService.Send(command);

                    // Disable Stop button and enable Start button
                    IsStartButtonEnabled = true;
                    IsStopButtonEnabled = false;
                    System.Windows.MessageBox.Show("Printer Stop.", "Start the Printer to print the data.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                }
                else
                {
                    System.Windows.MessageBox.Show("Please connect to the server before starting the printer.", "Connection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Failed to send command: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchData()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                System.Windows.MessageBox.Show("Please enter a valid Country Name.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                // Filter the data based on country name
                var filtered = ApiData.Where(item => item.name.common.IndexOf(SearchQuery, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                // Update the FilteredApiData collection with the filtered results
                FilteredApiData = new ObservableCollection<SubRegions>(filtered);
            }
        }

        //Send Excel Data.

        private async void SendDataToServer()
        {
            var dataToSend = FilteredApiData; // The filtered data to be sent to the server

            if (dataToSend == null || dataToSend.Count == 0)
            {
                System.Windows.MessageBox.Show("No data to send.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!_connectionService.IsConnected)
            {
                System.Windows.MessageBox.Show("Please connect to the server before sending data.", "Connection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsStartCommandSent)
            {
                System.Windows.MessageBox.Show("Please start the printer by pressing the Start button before sending data.", "Start Command Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                foreach (var item in dataToSend)
                {
                    // Format the data to send to the server
                    string message = FormatRowForServer(item);

                    // Send data to the server
                    _connectionService.Send(message);

                    // Wait for acknowledgment from the server
                    string response = await _connectionService.Receive();

                    // Check the server response
                    if (response == "PRC")
                    {
                        System.Windows.MessageBox.Show($"Data for {item.name.common} Print successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show($"Failed to send data for {item.name.common}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error sending data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string FormatRowForServer(SubRegions item)
        {
            // Format the row to include only specific fields for sending
            return $"JDI|COUNTRY={item.name.common}|CAPITAL={item.capital}|REGION={item.region}|SUBREGION={item.subregion}|POPULATION={item.population}|<CR>";
        }
    }
}