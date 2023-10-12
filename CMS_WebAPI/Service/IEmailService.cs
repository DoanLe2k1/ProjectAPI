using CMS_WebAPI.Models;

namespace CMS_WebAPI.Service
{
    public interface IEmailService
    {
        void SendEmail(ContactEmail request);
    }
}
