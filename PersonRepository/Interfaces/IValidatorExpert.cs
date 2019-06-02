using PersonRepository.Entities;
using System;
using System.Linq.Expressions;

namespace PersonRepository.Interfaces
{ 
    public interface IValidatorExpert : IPersonRepositoryAdvanced
    {
        bool Run(Person person, Expression<Func<Person, bool>> validation);
    }
}
