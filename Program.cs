using PersonRepository.Interfaces;
using PersonRepository;
using System;

namespace PersonValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            RepoTarea validator= new RepoTarea();
            ValidatorTest test = new ValidatorTest();
            test.Validate(validator);
            Console.ReadKey();
        }
    }
}
