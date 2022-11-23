using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using TaskManager.Core.Models;

namespace TaskManager.Scheduler.EmailSenders
{
    internal class DogFactsEmailSender : IEmailSender
    {
        private CronTask _task;
        public DogFactsEmailSender(CronTask task)
        {
            _task = task;
        }

        public async Task SendWithAttachmentAsync(string filepath)
        {
            await Email
                .From("taskmanager@example.com")
                .To(_task.User.Email)
                .Subject($"Task results from \"{_task.Api.Name}\"")
                .Body("See attachment!")
                .AttachFromFilename(filepath)
                .SendAsync();
        }
    }
}
