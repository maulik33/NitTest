using System;
using System.Web.UI.WebControls;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace STUDENT
{
    public partial class ShowImages : StudentBasePage<IStudentShowImagesView, StudentShowImagesPresenter>, IStudentShowImagesView
    {
        private const string ImageFolder = "~/Temp/images/lippincott";

        public string Images
        {
            get
            {
                var o = ViewState["Images"];
                return o == null ? string.Empty : (string)o;
            }

            set
            {
                ViewState["Images"] = value;
            }
        }

        public void ShowImage()
        {
            if (Images == string.Empty)
            {
                return;
            }

            string[] Imgs = Images.Split('|');

            foreach (string s in Imgs)
            {
                string ImgUrl = ImageFolder + ("/" + s);
                var Img = new Image { ImageUrl = ImgUrl };
                PlaceHolder1.Controls.Add(Img);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Images = Request.QueryString["Images"];

                Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();
        }
    }
}