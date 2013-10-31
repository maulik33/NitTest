using System;

namespace NursingLibrary.Common
{
    public class ReportParameter
    {
        private readonly string _name;
        private Action _cb;
        private ParamRefreshType _refreshType;

        public ReportParameter(string name, Action cb)
        {
            _name = name;
            _refreshType = ParamRefreshType.None;
            ShowNotSelected = true;
            ShowSelectAll = true;
            SelectedValues = string.Empty;
            _cb = cb;
        }

        public ReportParameter(string name, Action cb, ParamRefreshType refreshType)
            : this(name, cb)
        {
            _refreshType = refreshType;
        }

        // public ReportParameter(Action<string> cb)
        // {
        //    ShowNotSelected = true;
        //    ShowSelectAll = true;
        //    _cb2 = cb;
        // }

        // public ReportParameter(Action<string, string> cb)
        // {
        //    ShowNotSelected = true;
        //    ShowSelectAll = true;
        //    _cb3 = cb;
        // }

        // public ReportParameter(Action cb)
        // {
        //    ShowNotSelected = true;
        //    ShowSelectAll = true;
        //    _cb = new Action<string>(o => cb());
        // }
        public string SelectedValues { get; set; }

        public bool ShowUnAssigned { get; set; }

        public bool ShowSelectAll { get; set; }

        public bool ShowNotSelected { get; set; }

        public ParamRefreshType RefreshType
        {
            get
            {
                return _refreshType;
            }

            set
            {
                if (value == ParamRefreshType.Clear && _refreshType == ParamRefreshType.RefreshData)
                {
                    return;
                }

                _refreshType = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void Refresh()
        {
            _cb();
        }
    }
}
