﻿using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionariesSystem.Framework.Core.Commands.Create
{
    public class AddWordToDictionaryCommand : BaseCommand, ICommand
    {
        public const int NumberOfParameters = 2;
        private readonly IRepository<Word> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider user;

        public AddWordToDictionaryCommand(IRepository<Word> repository, IUnitOfWork unitOfWork, IUserProvider user)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(user, "user").IsNull().Throw();

            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.user = user;
        }
        protected override int ParametersCount
        {
            get
            {
                return NumberOfParameters;
            }
        }

        public override string Execute(IList<string> parameters)
        {
            return base.Execute(parameters);
        }
    }
}
