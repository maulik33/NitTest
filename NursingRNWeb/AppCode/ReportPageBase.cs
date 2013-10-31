using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.Presenters;
using WebControls;

/// <summary>
/// Summary description for ReportPageBase
/// </summary>
public abstract class ReportPageBase<TView, TPresenter> : PageBase<TView, TPresenter>
    where TView : class
    where TPresenter : ReportPresenterBase<TView>
{
    private readonly Dictionary<ReportParameter, ListControl> _paramControls;

    public ReportPageBase()
    {
        _paramControls = new Dictionary<ReportParameter, ListControl>();
    }

    public void MapControl(params ListControl[] control)
    {
        int lastIndex = _paramControls.Count - 1;
        foreach (ListControl item in control)
        {
            lastIndex++;
            _paramControls.Add(Presenter.Parameters.ElementAt(lastIndex).Value, item);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       //// call presenter.registerreportparams().. now this is called from preinitialize

        foreach (ListControl control in _paramControls.Values)
        {
            control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
        }
    }

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
        if (!IsPostBack)
        {
            Presenter.InitParamValues();
            Presenter.InitializeReport();
        }
        else
        {
            _paramControls.ToList().ForEach(p => AssignSelectedValue(p.Key, p.Value));
        }

        if (!IsPostBack)
        {
            Presenter.RefreshAll(MarkSelectedItems, ClearParamControl);
        }
    }

    private void control_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListControl control = (ListControl)sender;
        if (control != null)
        {
            // TODO: Gokul - Fix & Optimize Dependents Refresh Events
            ReportParameter param = _paramControls.Where(p => p.Value == control)
                .Select(p => p.Key).FirstOrDefault();
            Presenter.RefreshChildren(param, true);
            Presenter.RefreshAll(null, ClearParamControl);
        }
    }

    private void ClearParamControl(ReportParameter parameter)
    {
        KTPDropDownList dropDownList = _paramControls[parameter] as KTPDropDownList;
        if (dropDownList != null)
        {
            dropDownList.ClearData();
        }
        else
        {
            KTPListBox listBox = _paramControls[parameter] as KTPListBox;
            if (listBox != null)
            {
                listBox.ClearData();
            }
        }
    }

    private void AssignSelectedValue(ReportParameter parameter, ListControl control)
    {
        parameter.SelectedValues = string.Empty;

        KTPDropDownList dropDownList = control as KTPDropDownList;
        if (dropDownList != null)
        {
            parameter.SelectedValues = dropDownList.SelectedValuesText;
        }
        else
        {
            KTPListBox listBox = control as KTPListBox;
            if (listBox != null)
            {
                parameter.SelectedValues = listBox.SelectedValuesText;
            }
        }
    }

    private void MarkSelectedItems(ReportParameter parameter)
    {
        KTPDropDownList dropDownList = _paramControls[parameter] as KTPDropDownList;
        if (dropDownList != null)
        {
            dropDownList.MarkSelectedItems(parameter.SelectedValues);
        }
        else
        {
            KTPListBox listBox = _paramControls[parameter] as KTPListBox;
            if (listBox != null)
            {
                listBox.MarkSelectedItems(parameter.SelectedValues);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }
}
