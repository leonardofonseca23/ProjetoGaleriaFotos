using ProjetoGaleriaFotos;

namespace ProjetoGaleriaFotos;

public partial class AddFotos : ContentPage
{
    FotosDatabase fotosDatabase;
    string _fotoAtual= "";

	public AddFotos()
	{
		InitializeComponent();
        fotosDatabase = new FotosDatabase();
	}

    private async void tirarFoto_Clicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult foto = await MediaPicker.Default.CapturePhotoAsync();
            await GravarFoto(foto);
        }
        else
        {
            DisplayAlert("Erro", "Câmera não disponível", "OK");
        }

    }

    private async Task GravarFoto(FileResult foto)
    {
        if (foto != null)
        {
            string arquivoLocal = Path.Combine(FileSystem.CacheDirectory, foto.FileName);

            using Stream sourceStream = await foto.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(arquivoLocal);

            await sourceStream.CopyToAsync(localFileStream);

            LdFoto.Source = arquivoLocal;
            _fotoAtual = arquivoLocal;
        }
        else
        {
            _fotoAtual = "";
            LdFoto.Source = null;
        }
    }

    private async void importarFoto_Clicked(object sender, EventArgs e)
    {
        FileResult foto = await MediaPicker.Default.PickPhotoAsync();

        await GravarFoto(foto);
    }

    private async void salvarFoto_Clicked(object sender, EventArgs e)
    {
        string comentario = Descricao.Text;

        if (comentario == "")
        {
            await DisplayAlert("Erro", "Descrição não informada", "OK");
            return;
        }

        if (_fotoAtual == "")
        {
            await DisplayAlert("Erro", "Foto não selecionada", "OK");
            return;
        }

        await SaveFoto(_fotoAtual, comentario);

        Descricao.Text = "";
        LdFoto.Source = null;
        _fotoAtual = "";
    }

    private async Task SaveFoto(string localFilePath, string comentario)
    {

        Postagem saveFoto = new Postagem();
        saveFoto.Foto = localFilePath;
        saveFoto.Comentario = comentario;
        saveFoto.DtPostagem = DateTime.Now;

        await fotosDatabase.SaveItemAsync(saveFoto);
    }

    private async void Voltar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}