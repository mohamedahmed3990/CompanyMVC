using CompanyMVC.DAL.Model;

namespace CompanyMVC.PL.Helpers
{
	public interface IMailSetting
	{
		public void SendMail(Email email);
	}
}
