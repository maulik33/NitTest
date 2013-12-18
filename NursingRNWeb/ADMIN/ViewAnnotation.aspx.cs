using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace NursingRNWeb.ADMIN
{
    public partial class ViewAnnotation : PageBase<IInstitutionView, InstitutionPresenter>, IInstitutionView
    {
        public int IID
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ActionType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public UserType UserTypeValue
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Institution Institution
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Program Program
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<TimeZones> TimeZones
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsInValidInstitution
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public string ProgramOfStudy
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.List);
        }

        public void BindData()
        {
            throw new NotImplementedException();
        }

        public void BindData(IEnumerable<TimeZones> timeZones, IEnumerable<Program> nurPrograms, IEnumerable<ProgramofStudy> programOfStudies)
        {
            throw new NotImplementedException();
        }

        public void ShowInstitutionResults(IEnumerable<Institution> Institutions, SortInfo sortMetaData)
        {
            throw new NotImplementedException();
        }

        public void PopulateCountry(IEnumerable<Country> country)
        {
            throw new NotImplementedException();
        }

        public void PopulateState(IEnumerable<State> state)
        {
            throw new NotImplementedException();
        }

        public void PopulateAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public void ExportInstitutions(IEnumerable<Institution> reportData, ReportAction reportAction)
        {
            throw new NotImplementedException();
        }

        public void PopulateInstitutionContacts(IEnumerable<InstitutionContact> institutionContacts)
        {
            throw new NotImplementedException();
        }

        #region ICohortView Members

        public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
        {
            throw new NotImplementedException();
        }
        #endregion
        public void PopulateProgramOfStudy(IEnumerable<Country> programOfStudies)
        {
            throw new NotImplementedException();
        }

        public void PopulateProgram(IEnumerable<Program> programs)
        {
            throw new ArgumentException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblAnnotation.Text = Presenter.GetAnnotation();
        }


        public string ErrorMessage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}