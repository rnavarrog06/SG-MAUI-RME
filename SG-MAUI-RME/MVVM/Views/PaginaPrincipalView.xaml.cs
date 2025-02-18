using SG_MAUI_RME.MVVM.ViewModels;

namespace SG_MAUI_RME.MVVM.Views;

public partial class PaginaPrincipal : ContentPage
{
	public PaginaPrincipal()
	{
		InitializeComponent();
        BindingContext = new PaginaPrincipalViewModel();
    }
}