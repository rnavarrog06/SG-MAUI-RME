using SG_MAUI_RME.MVVM.ViewModels;

namespace SG_MAUI_RME.MVVM.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();

		BindingContext = new UsuarioViewModel();
	}
}