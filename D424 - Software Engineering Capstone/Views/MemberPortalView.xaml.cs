using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class MemberPortalView : ContentPage
{
	public UserModel CurrentUser { get; set; }

	public MemberPortalView(UserModel currentUser)
	{
		InitializeComponent();

		CurrentUser = currentUser;
	}
}