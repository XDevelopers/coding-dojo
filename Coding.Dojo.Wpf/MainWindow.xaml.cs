using Coding.Dojo.Abtraction;
using Coding.Dojo.Wpf.ViewModels;
using Microsoft.Win32;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Coding.Dojo.Wpf
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

        #region [ Events ]

        private void ImportarClientesClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Json files (*.json)|*.json",
                InitialDirectory = @"C:\Temp\"
            };

            // Faz a Procura por um arquivo que atenda ao Fitro configurado acima!
            if (openFileDialog.ShowDialog() == true)
            {
                var json = File.ReadAllText(openFileDialog.FileName);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    // Chama os Métodos para:
                    // Converter o Json em um (Lista de Objetos)
                    var clientes = ConvertJson(json);

                    // Preencher a DataGrid com a (Lista dos Objetos)
                    if (clientes.Any())
                    {
                        LoadDataGrid(clientes);
                    }
                }
            }
        }

        private void ValidarClientesClick(object sender, RoutedEventArgs e)
        {
            // Pegar os dados que estão dentro da DataGrid e Repopula-los somente com os Adultos
            var clientes = new List<ClienteViewModel>((IEnumerable<ClienteViewModel>)DadosImportados.ItemsSource);
            if (!clientes.Any())
                return;

            var idadeAdulta = DateTime.Now.AddYears(-18).Date;
            var clientesAdultos = clientes
                .Where(c => c.DataNascimento >= idadeAdulta)
                .Select(c =>
                {
                    c.ConsideradoAdulto = true;
                    return c;
                });

            DadosImportados.ItemsSource = clientesAdultos;

            // Modifica o Estilo da DataGrid para sabermos que foi atualizada
            DadosImportados.Background = Brushes.LightGray;
            DadosImportados.RowBackground = Brushes.LightYellow;
            DadosImportados.AlternatingRowBackground = Brushes.LightBlue;
        } 

        #endregion [ Events ]

        #region [ Helpers ]

        private IEnumerable<Cliente> ConvertJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            // Instancia o Manager
            var manager = new Managers.ClienteManager();

            // Deserializa o Json
            var result = manager.Deserialize<List<Cliente>>(json);

            if (result == null)
                return null;
            
            return result;
        }

        private void LoadDataGrid(IEnumerable<Cliente> clientes)
        {
            DadosImportados.ItemsSource = clientes.Select(c => 
            {
                return new ClienteViewModel(c);
            }).ToList();

            // Limpa o Estilo da Grid
            DadosImportados.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF0F0F0")); //DadosImportados.Background = Brushes.White;
            DadosImportados.RowBackground = null;
            DadosImportados.AlternatingRowBackground = null;
        }

        private void PegarClientesClick(object sender, RoutedEventArgs e)
        {
            var baseUrl = $"https://my.api.mockaroo.com/codingdojo.json?key=beda91c0";

            var restClient = new RestClient(baseUrl);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");

            var response = restClient.Execute(request);
            if (response != null && response.Content != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = response.Content;

                // Chama os Métodos para:
                // Converter o Json em um (Lista de Objetos)
                var clientes = ConvertJson(json);

                // Preencher a DataGrid com a (Lista dos Objetos)
                if (clientes.Any())
                {
                    LoadDataGrid(clientes);
                }
            }
            else
            {
                MessageBox.Show($"Não foi possível Recuperar os dados da Url informada:\n {baseUrl}"," Ops...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion [ Helpers ]
    }
}