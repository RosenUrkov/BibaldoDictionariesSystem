﻿using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using System.Collections.Generic;
using System.Linq;

namespace DictionariesSystem.Framework.Core.Commands.Create
{
    public class AddWordToDictionaryCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "[wordName] [dictionaryTitle] [speechPart] [wordMeaning]";
        private const int NumberOfParameters = 4;

        private readonly IRepository<Dictionary> dictionaries;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        private readonly IDictionariesFactory dictionariesFactory;

        public AddWordToDictionaryCommand(IRepository<Dictionary> dictionaries, IUnitOfWork unitOfWork, IUserProvider userProvider, IDictionariesFactory dictionariesFactory)
        {
            Guard.WhenArgument(dictionaries, "dictionaries").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();
            Guard.WhenArgument(dictionariesFactory, "dictionariesFactory").IsNull().Throw();

            this.dictionaries = dictionaries;
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
            this.dictionariesFactory = dictionariesFactory;
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
            Guard.WhenArgument(parameters.Count, "parameters.count").IsLessThan(NumberOfParameters).Throw();

            string wordName = parameters[0];
            string dictionaryTitle = parameters[1];
            string speechPart = parameters[2];
            string wordDescription = string.Join(" ", parameters.Skip(3));
            
            var dictionary = this.dictionaries.All(d => d.Title == dictionaryTitle).FirstOrDefault();
            Guard.WhenArgument(dictionary, "No Such Dictionary in the system").IsNull().Throw();

            var foundWord = dictionary.Words.FirstOrDefault(w => w.Name == wordName);
            Guard.WhenArgument(foundWord, "Word Already exists").IsNotNull().Throw();

            Meaning wordMeaning = dictionary.Meanings.FirstOrDefault(m => m.Description == wordDescription);
            if (wordMeaning == null)
            {
                wordMeaning = this.dictionariesFactory.GetMeaning(wordDescription);
            }

            Word newWord = this.dictionariesFactory.GetWord(wordName, speechPart);
            newWord.Meanings.Add(wordMeaning);
            dictionary.Words.Add(newWord);
            
            this.unitOfWork.SaveChanges();

            string result = $"A new word: {wordName} was added into dictionary: {dictionaryTitle}\n{wordName} means {wordDescription}";
            return result;
        }
    }
}
