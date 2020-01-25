using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swivel.Search.Common;
using Swivel.Search.Service.Interface;

namespace Swivel.Search.Service
{
    public class MockNotificationService : BaseService, INotificationService
    {
        public MockNotificationService(IOptions<AppSettings> settings, ILogger<MockNotificationService> logger): base(logger, settings)
        {
        }

        public void InternalEmail(string subject, string message)
        {
            //Notification will be handled via a async queue. 
            //We will build the email content and then push the message to a queue
            //A background job or a queue triggered function app will pickup the message and process
            _logger.LogInformation($"Email from {_settings.SysEmail} to sent to {_settings.SupportEmail}. Subject: {subject} | Body: {message}");
        }
    }
}
