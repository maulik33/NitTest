using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    public abstract class ReportPresenterBase<TView> : AuthenticatedPresenterBase<TView>
    {
        private readonly Dictionary<ReportParameter, ParamDependency> _dependencies;

        public ReportPresenterBase(Module module)
            : base(module)
        {
            Parameters = new Dictionary<string, ReportParameter>();
            _dependencies = new Dictionary<ReportParameter, ParamDependency>();
        }

        public Dictionary<string, ReportParameter> Parameters { get; private set; }

        public void AddParameter(ReportParameter parameter, params ReportParameter[] dependsOn)
        {
            Parameters.Add(parameter.Name, parameter);
            _dependencies.Add(parameter, new ParamDependency(dependsOn));
            foreach (ReportParameter param in dependsOn)
            {
                _dependencies[param].AddDependent(parameter);
            }
        }

        public void RefreshChildren(ReportParameter param, bool isDataRefresh)
        {
            ParamRefreshType flag = isDataRefresh ? ParamRefreshType.RefreshData : ParamRefreshType.Clear;

            foreach (ReportParameter child in _dependencies[param].Dependents)
            {
                child.RefreshType = flag;

                // child.Refresh(isDataRefresh);
                RefreshChildren(child, false);
            }
        }

        public void InitializeReport()
        {
            // Set default to ClearItems for all Report Parameters
            Parameters.ToList().ForEach(p => p.Value.RefreshType = ParamRefreshType.Clear);

            // Set flag to refresh 1st level Parameters.
            IEnumerable<ReportParameter> initParams = _dependencies.Where(p => p.Value.DependsOn.Count() == 0).Select(p => p.Key);
            initParams.ToList().ForEach(p => p.RefreshType = ParamRefreshType.RefreshData);

            // SetDataRefresh(initParams);

            // Set flag to refresh Parameters that has selectedValue.
            initParams = Parameters.Where(p => false == string.IsNullOrEmpty(p.Value.SelectedValues)).Select(p => p.Value);
            initParams.ToList().ForEach(p => p.RefreshType = ParamRefreshType.RefreshData);

            // Set Refresh data for items whose parent has selected value
            // initParams.ToList().ForEach(p => );
        }

        public virtual void InitParamValues()
        {
        }

        public void RefreshAll(Action<ReportParameter> markSelectedValues, Action<ReportParameter> clearControl)
        {
            foreach (ReportParameter param in Parameters.Values.Where(p => p.RefreshType != ParamRefreshType.None))
            {
                switch (param.RefreshType)
                {
                    case ParamRefreshType.RefreshData:
                        param.Refresh();
                        if (markSelectedValues != null)
                        {
                            markSelectedValues(param);
                        }

                        break;
                    case ParamRefreshType.Clear:
                        clearControl(param);
                        break;
                }
            }
        }

        private void SetDataRefresh(IEnumerable<ReportParameter> parameters)
        {
            foreach (ReportParameter param in parameters)
            {
                param.RefreshType = ParamRefreshType.RefreshData;
            }
        }

        private void SetFlags()
        {
        }

        private class ParamDependency
        {
            private readonly List<ReportParameter> _dependents;
            private readonly List<ReportParameter> _dependsOn;

            public ParamDependency(IEnumerable<ReportParameter> dependsOn)
            {
                _dependents = new List<ReportParameter>();
                _dependsOn = new List<ReportParameter>();
                _dependsOn.AddRange(dependsOn);
            }

            public IEnumerable<ReportParameter> Dependents
            {
                get
                {
                    return _dependents;
                }
            }

            public IEnumerable<ReportParameter> DependsOn
            {
                get
                {
                    return _dependsOn;
                }
            }

            public void AddDependent(ReportParameter dependentParameter)
            {
                _dependents.Add(dependentParameter);
            }
        }
    }
}
