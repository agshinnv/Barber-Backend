using Domain.Models;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;  
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await _accountRepository.GetAll();
        }

        public async Task<IList<string>> GetRoles(AppUser user)
        {
            return await _accountRepository.GetRoles(user);
        }
    }
}
