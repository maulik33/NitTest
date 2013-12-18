using System;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class ADMIN_ValidateKaplan : PageBase<IValidateKaplanView, ValidateKaplanPresenter>, IValidateKaplanView
{
    public override void PreInitialize()
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //// Gokul - as per mail from Mike / Srikar on 6/28/2011, 2:04 PM.
        //// This functionliaty is no longer used. So making it void.
        ////Presenter.GetCommandvalues();
    }
}
