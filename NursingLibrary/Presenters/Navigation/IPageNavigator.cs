namespace NursingLibrary.Presenters.Navigation
{
    public interface IPageNavigator
    {
        void NavigateTo(PageDirectory page, string frgament, string query);

        void NavigateTo(PageDirectory page);
        
        void NaviagteTo(PageDirectory page, string fragment, string query);
        
        void NavigateTo(AdminPageDirectory page, string fragment, string query);
        
        void NavigateTo(AdminPageDirectory page);
    }
}
