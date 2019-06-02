using System.Collections.Immutable;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Globalization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PersonRepository;
using PersonRepository.Entities;
using PersonRepository.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace PersonValidator
{
    public class RepoTarea : IPersonRepositoryAdvanced, IValidatorExpert
    {
        public List<PersonRepository.Entities.Person> People { get; set; }

        public RepoTarea()
        {
            People = new List<PersonRepository.Entities.Person>();
        }

        public void Add(PersonRepository.Entities.Person person)
        {
            var mail = new EmailAddressAttribute();

            if(!People.Exists(x=> x.Id == person.Id)) 
            { 
                if(person.Age>=1 && person.Age <=100)
                {
                    if(person.Email.Contains('@') && person.Email.Contains('.'))
                    {
                        People.Add(person);
                    } /* else Console.WriteLine("Add: Email invalido"); */
                } /* else Console.WriteLine("Add: Edad invalida"); */
            } /* else Console.WriteLine("Add: Id invalido"); */
        }

        public void Delete(int id)
        {
            int index = (People.FindIndex(x => x.Id == id));
            if(index!= -1) People.RemoveAt(index);
            /* else Console.WriteLine("Delete: No existe Id");  */           
        }

        public int GetCountRangeAges(int min, int max) => (from P in People where (P.Age >= min && P.Age <= max) select P.Age).Count();

        public PersonRepository.Entities.Person GetPerson(int Id)
        {          
           return People.Find(P => P.Id == Id);
        }

        public void Update(PersonRepository.Entities.Person person)
        {
                if(People.Exists(x=> x.Id == person.Id))
                {
                    Delete(person.Id);
                    Add(person);
                }
        }

        List<Person> IPersonRepositoryBasic.GetFiltered(string name, int age, string email)
        {
            Func<Person,bool> fillter_age = (p) =>p.Age == age || age == 0;
            Func<Person,bool> fillter_name = (p) => name is null || p.Name == name  || name == "";
            Func<Person,bool> fillter_email = (p) => email is null || email == "" || p.Email.Contains(email);
        
            Func<Person,bool> filter = (p) => (fillter_name(p) && fillter_age(p) && fillter_email(p));
            
            return (from p in People where filter(p) select p).ToList(); 
        }

        public int[] GetNotCapitalizedIds()
        {
            List<int> vec = new List<int>();
            TextInfo tx = CultureInfo.CurrentCulture.TextInfo;
            foreach (var item in People)
            {
                if((string.Compare(item.Name,tx.ToTitleCase(item.Name)))==-1)
                {
                    vec.Add(item.Id);
                }
            }
            return vec.ToArray();
        }

        public Dictionary<int, string[]> GroupEmailByNameCount()
        {
            Dictionary<int, string[]> temp = new Dictionary<int, string[]>();
            int[] cantNomb = new int[4];
            foreach (var item in People)
            {
                cantNomb[item.Name.Split(' ').Length]++;
            }

            for (int i = 0; i < cantNomb.Length; i++)
            {
                List<string> EmailNombre = new List<string>();
                foreach (var item in People)
                {
                    if (i == item.Name.Split(' ').Length)
                    {
                        EmailNombre.Add(item.Email);
                    }
                }
                if(EmailNombre.Count>0){string[] tempVec= EmailNombre.ToArray(); temp.Add(i,tempVec);}
                else temp.Add(i,null);

            }
            return temp;
        }

        public bool Run(Person person, Expression<Func<Person, bool>> validation)
        {
            return validation.Compile()(person);
        }
    }
}