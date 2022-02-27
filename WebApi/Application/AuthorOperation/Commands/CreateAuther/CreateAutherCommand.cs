using System;
using System.Linq;
using System.Windows.Input;
using FluentValidation;
using WebApi.Application.OperationAbstract;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperation.Command.CreateAuther
{
    public class CreateAutherCommand
    {
        public CreateAutherViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public CreateAutherCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var auther = _dbContext.Authors.SingleOrDefault(x => x.Name == this.Model.Name);
            if (auther is not null)
                throw new InvalidOperationException("Yazar zaten mevcut.");
            CreateAutherCommandValidator validator = new();

            _dbContext.Authors.Add(new Author
            {
                Name = Model.Name,
                Birthday = Model.Birthday,
                Surname = Model.Surname,
            });

            _dbContext.SaveChanges();
        }

    }
    public class CreateAutherViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}