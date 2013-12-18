using System;
using System.Web;
using System.Web.UI;
using NursingLibrary.Presenters;

/// <summary>
/// Summary description for StudentHeaderBaseControl
/// </summary>
public class StudentHeaderBaseControl<TView, TPresenter> : UserControl
    where TPresenter : StudentPresenter<TView>
    where TView : class
{
    public TPresenter Presenter { get; set; }

    //// ReSharper disable InconsistentNaming
    protected void Page_Init(object sender, EventArgs e)
    //// ReSharper restore InconsistentNaming
    {
        InjectDependencies();
    }

    protected virtual void InjectDependencies()
    {
        var context = HttpContext.Current;
        if (context == null)
        {
            return;
        }

        var accessor = context.ApplicationInstance as IContainerAccessor;
        if (accessor == null)
        {
            return;
        }

        var container = accessor.Container;
        if (container == null)
        {
            throw new InvalidOperationException("No Unity container found");
        }

        Presenter = container.Resolve(typeof(TPresenter), string.Empty) as TPresenter;
        if (Presenter == null)
        {
            throw new InvalidOperationException("Presenter could not be resolved");
        }

        Presenter.View = this as TView;
    }
}
