using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperation.Command.UpdateAuther
{
    public class UpdateAutherCommand
    {

        public int ModelId { get; set; }
        public UpdateAutherViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateAutherCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var auther = _dbContext.Authors.SingleOrDefault(p => p.Id == ModelId);

            if (auther is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            auther.Name = Model.Name;
            auther.Surname = Model.Surname;
            auther.Birthday = Model.Birthday;
            _dbContext.SaveChanges();

        }


    }

    public class UpdateAutherViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
    }
}