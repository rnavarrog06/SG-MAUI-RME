using SG_MAUI_RME.MVVM.ViewModels;

namespace SG_MAUI_RME.MVVM.Views;

public partial class GestionUsuariosView : ContentPage
{
	public GestionUsuariosView()
	{
		InitializeComponent();
        BindingContext = new GestionUsuariosViewModel();
    }
}