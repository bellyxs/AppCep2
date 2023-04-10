using AppCep.Model;
using AppCep.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCep.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BuscaLogradouroPorBairroECidade : ContentPage
    {
        Cidade city;
        ObservableCollection<Cidade> lst_cidades = new ObservableCollection<Cidade>();
        ObservableCollection<Bairro> lst_bairro = new ObservableCollection<Bairro>();

        public BuscaLogradouroPorBairroECidade()
        {
            InitializeComponent();
            pck_cidade.ItemsSource = lst_cidades;
            pck_bairro.ItemsSource = lst_bairro;
        }

        private async void pck_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                carregando.IsRunning = true;
                lst_bairro.Clear();
                lst_cidades.Clear();

                Picker disparador = sender as Picker;

                string estado = disparador.SelectedItem as string;

                List<Cidade> arr_cidades = await DataService.GetCidadeByEstado(estado);

                lst_cidades.Clear();

                arr_cidades.ForEach(i => lst_cidades.Add(i));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                carregando.IsRunning = false;
            }
        }

        private async void pck_bairro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                carregando.IsRunning = true;

                lst_end.ItemsSource = null;

                Picker disparador = sender as Picker;

                Bairro bairro = disparador.SelectedItem as Bairro;

                if (bairro != null)
                {
                    List<Logradouro> arr_end = await DataService.GetLogradouroByBairroAndIdCidade(bairro.descricao_bairro, city.id_cidade);

                    lst_end.ItemsSource = arr_end;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                carregando.IsRunning = false;
            }
        }

        private async void pck_cidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                carregando.IsRunning = true;
                lst_bairro.Clear();


                Picker disparador = sender as Picker;

                Cidade cidade = disparador.SelectedItem as Cidade;

                lst_bairro.Clear();

                List<Bairro> arr_bairros = await DataService.GetBairrosByIdCidade(cidade.id_cidade);


                arr_bairros.ForEach(item => lst_bairro.Add(item));
                Console.WriteLine(arr_bairros);

                this.city = cidade;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                carregando.IsRunning = false;
            }
        }


    }
}