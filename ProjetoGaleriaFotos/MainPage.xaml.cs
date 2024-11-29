using System;

namespace ProjetoGaleriaFotos
{
    public partial class MainPage : ContentPage
    {

        FotosDatabase fotosDatabase;

        public MainPage()
        {
            InitializeComponent();
            fotosDatabase = new FotosDatabase();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LdFotos();
        }

        public async void LdFotos()
        {
            List<Postagem> fotos = await fotosDatabase.GetItemsAsync();

            FotoLista.ItemsSource = new List<Postagem>(fotos);
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

            var acao = await DisplayActionSheet("Deseja excluir?", "Não", "Sim");

            if (acao?.ToString() == "Sim")
            {
                var tappedItem = ((StackLayout)sender).BindingContext;

                if (tappedItem != null)
                {
                    Postagem? postagem = tappedItem as Postagem;
                    if (postagem != null)
                    {
                        await fotosDatabase.DeleteItemAsync(postagem);
                        LdFotos();
                    }
                }

            }

        }

        private async void NovaFoto_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddFotos");
        }
    }

}
