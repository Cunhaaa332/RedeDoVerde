using System;
using System.Collections.Generic;
using System.Text;

namespace RedeDoVerde.Domain.Account
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Account> Accounts { get; set; }

    }
}
