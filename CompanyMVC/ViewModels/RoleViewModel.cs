namespace CompanyMVC.PL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
