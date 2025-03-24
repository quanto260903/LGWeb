using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace LGWebsite.Services.Common
{
    public  class EmailUtil
    {
        private readonly IUnitOfWork _unitOfWork;

        // Constructor để khởi tạo IUnitOfWork
        public EmailUtil(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                // Lấy thông tin cấu hình từ cơ sở dữ liệu
                var smtpHostConfig = await _unitOfWork.SystemConFig.FirstOrDefaultAsync("SMTP");
                var smtpPortConfig = await _unitOfWork.SystemConFig.FirstOrDefaultAsync("Port");
                var fromEmailConfig = await _unitOfWork.SystemConFig.FirstOrDefaultAsync("fromEmail");
                var fromPasswordConfig = await _unitOfWork.SystemConFig.FirstOrDefaultAsync("fromPassword");

                // Kiểm tra giá trị lấy được
                string smtpHost = smtpHostConfig?.ConfigValue;
                if (!int.TryParse(smtpPortConfig?.ConfigValue, out int smtpPort))
                {
                    smtpPort = 587; // Giá trị mặc định nếu không thể chuyển đổi
                }
                string fromEmail = fromEmailConfig?.ConfigValue;
                string fromPassword = fromPasswordConfig?.ConfigValue;

                using (var client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(toEmail);
                    client.Send(mailMessage);
                    return true; // Gửi email thành công
                }
            }
            catch (Exception ex) // Bắt lỗi cụ thể
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false; // Gửi email thất bại
            }
        }
    }
}
