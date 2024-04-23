using Mailjet.Client;
using ProjectClock.BusinessLogic.Email.Models.Email;
using static ProjectClock.BusinessLogic.Email.Models.Email.EmailModel;

namespace ProjectClock.BusinessLogic.Email.EmailSender;

public abstract class EmailSender : IEmailSender
{
    public static MailjetClient CreateMailJetClient()
    {
        return new MailjetClient("beea9feda84222a0a11e4cdbfc0e2353", "623afce740a5175bacabe6180e263feb");
    }
    protected abstract Task Send(EmailModel emailModel);

    public async Task SendEmail(EmailModel emailModel)
    {
        await Send(emailModel);
    }

    public async Task SendEmail(string email, string subject, string body, List<EmailAttachment>? emailAttachment = null)
    {
        await Send(new EmailModel
        {
            EmailAdress = email,
            Subject = subject,
            Body = body,
            Attachments = emailAttachment
        });
    }
}
