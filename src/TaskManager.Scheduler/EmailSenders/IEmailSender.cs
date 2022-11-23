using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Scheduler.EmailSenders
{
    public interface IEmailSender
    {
        Task SendWithAttachmentAsync(string filepath);
    }
}
