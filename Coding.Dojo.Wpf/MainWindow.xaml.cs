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
    /// Tela principal do Cadastro de Clientes - CodingDojô
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
                // Ler o Conteúdo de um arquivo (texto | json) em uma linha de código
                var filePath = openFileDialog.FileName;
                var json = "##Pegar o Conteúdo de um arquivo texto!"; //INSERIR aqui um função para ler o Conteudo de um arquivo (Texto | Json)

                if (true) // Validar se o Conteudo foi lido ou não 
                {
                    // Se conseguiu pegar o Json, então deverá [ 1- Converte em uma Lista fortemente tipada, 2- Preencher a Grid ]
                    // Converter o Json em um (Lista de Objetos)
                    var clientes = ConvertJson(json);

                    // Validar se o objeto existe antes de passar ao método que irá preencher a DataGrid
                    if (true)
                    {
                        // Preencher a DataGrid com a (Lista dos Objetos)
                        LoadDataGrid(clientes);
                    }
                }
            }
        }

        private void ValidarClientesClick(object sender, RoutedEventArgs e)
        {
            // Pegar os dados que estão dentro da DataGrid e Repopula-los somente com os Adultos
            var clientes = DadosImportados.ItemsSource;
            if (!clientes.Any())
                return;

            var idadeAdulta = DateTime.Now;// devem ser maiores que 18 anos;

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

        private object ConvertJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            // Instancia o Manager
            var manager = new Managers.ClienteManager();

            // Usando uma Classe de Helper Deserializar o JSON
            // Deserializa o Json
            var result = manager.Deserialize<object>(json);

            if (result == null)
                return null;
            
            return result;
        }

        private void LoadDataGrid(object clientes)
        {
            DadosImportados.ItemsSource = clientes;

            // Usando a lista acima melhorar a exibição para Exibir o Endereço corretamente. (Quem sabe usando uma ViewModel) !

            // Limpa o Estilo da Grid
            DadosImportados.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF0F0F0")); //DadosImportados.Background = Brushes.White;
            DadosImportados.RowBackground = null;
            DadosImportados.AlternatingRowBackground = null;
        }

        private void PegarClientesClick(object sender, RoutedEventArgs e)
        {
            var baseUrl = $"https://my.api.mockaroo.com/codingdojo.json?key=beda91c0";

            //var restClient = new Client Rest (baseUrl);
            //var request = new Request Rest(Method.GET);

            //request.AddHeader("Content-Type", "application/json");

            //var response = Client Rest.Execute(request);// manda o comando de Execução do Request solicitado

            // Valida o Resultado
            //if (response != null && response.Content != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            if (true)
            {
                //var json = response.Content;

                // Chama os Métodos para:
                // Converter o Json em um (Lista de Objetos)
                //var clientes = ConvertJson(json);

                // Preencher a DataGrid com a (Lista dos Objetos)
                //if (clientes.Any())
                //{
                //    LoadDataGrid(clientes);
                //}
            }
            else
            {
                MessageBox.Show($"Não foi possível Recuperar os dados da Url informada:\n {baseUrl}"," Ops...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion [ Helpers ]
    }
}