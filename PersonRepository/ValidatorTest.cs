using PersonRepository.Entities;
using PersonRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonRepository
{
    public class ValidatorTest
    {
        public void Validate(IPersonRepositoryBasic personRepository)
        {
            Console.WriteLine("Validando...");

            ValidateAdd(personRepository);
            ValidateUpdate(personRepository);
            ValidateDelete(personRepository);
            ValidateFilters(personRepository);
            ValidateGet(personRepository);

            if (personRepository is IPersonRepositoryAdvanced)
            {
                ValidateCapitalized(personRepository as IPersonRepositoryAdvanced);
                ValidateGroup(personRepository as IPersonRepositoryAdvanced);

                if (personRepository is IValidatorExpert)
                {
                    ValidateRun(personRepository as IValidatorExpert);
                }
            }

            Console.WriteLine("Todo ok!!!!");

        }

        private void ValidateRun(IValidatorExpert personRepository)
        {
            Console.WriteLine("Validando Run...");
            
            if (personRepository.Run(new Person()
            {
                Id = 1,
                Name = "ariel",
                Age = 22,
                Email = "ariel1@ariel.com",
            }, p => p.Name == "Krako" && p.Age == 22))
            {
                throw new Exception("No se corrio la validacion correctamente");
            }

            Console.WriteLine("Run ok");
        }

        private void ValidateGroup(IPersonRepositoryAdvanced personRepository)
        {
            Console.WriteLine("Validando agrupacion...");

            personRepository.People = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "ariel",
                    Age = 22,
                    Email = "ariel1@ariel.com",
                },
                new Person()
                {
                    Id = 2,
                    Name = "ariel Krako",
                    Age = 33,
                    Email = "ariel2@ariel.com",
                },
                new Person()
                {
                    Id = 3,
                    Name = "Ariel Krako",
                    Age = 5,
                    Email = "ariel3@ariel.com",
                },
                new Person()
                {
                    Id = 4,
                    Name = "Miguel Angel Gonzalez",
                    Age = 14,
                    Email = "ariel4@ariel.com",
                },

                new Person()
                {
                    Id = 5,
                    Name = "Miguel angel Perez",
                    Age = 14,
                    Email = "ariel5@ariel.com",
                },
            };

            var res = personRepository.GroupEmailByNameCount();

            if (res[1].Count() != 1 || res[2].Count() != 2 || res[3].Count() != 2 || !res[3].Any(e => e == "ariel5@ariel.com"))
            {
                throw new Exception("No se obtuvo la agrupacion");
            }

            Console.WriteLine("Agrupacion ok");
        }

        private void ValidateCapitalized(IPersonRepositoryAdvanced personRepository)
        {
            Console.WriteLine("Validando capitalizacion...");

            personRepository.People = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "ariel",
                    Age = 22,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 2,
                    Name = "ariel Krako",
                    Age = 33,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 3,
                    Name = "Ariel Krako",
                    Age = 5,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 4,
                    Name = "Miguel Angel Gonzalez",
                    Age = 14,
                    Email = "ariel@ariel.com",
                },

                new Person()
                {
                    Id = 5,
                    Name = "Miguel angel Perez",
                    Age = 14,
                    Email = "ariel@ariel.com",
                },
            };


            if (personRepository.GetNotCapitalizedIds().Count() != 3 && 
                !(personRepository.GetNotCapitalizedIds().Contains(1) &&
                personRepository.GetNotCapitalizedIds().Contains(2) &&
                personRepository.GetNotCapitalizedIds().Contains(5)))
            {
                throw new Exception("No se obtuvo la lista de nombres no capitalizados");
            }

            Console.WriteLine("Capitalizacion OK");
        }

        private void ValidateRange(IPersonRepositoryBasic personRepository)
        {
            Console.WriteLine("Validando rango...");

            personRepository.People = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "Ariel",
                    Age = 22,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 2,
                    Name = "Ariel Krako",
                    Age = 33,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 3,
                    Name = "Miguel",
                    Age = 5,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 4,
                    Name = "Miguel",
                    Age = 14,
                    Email = "ariel@ariel.com",
                },
            };

            if (personRepository.GetCountRangeAges(5, 16) != 2 ||
                personRepository.GetCountRangeAges(2, 4) != 2 ||
                personRepository.GetCountRangeAges(25, 26) != 0)
            {
                throw new Exception("No se obtuvo el rango correcto");
            }

            Console.WriteLine("Rango OK!");
        }

        private void ValidateGet(IPersonRepositoryBasic personRepository)
        {
            Console.WriteLine("Validando get...");

            personRepository.People = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "Ariel",
                    Age = 22,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 2,
                    Name = "Ariel Krako",
                    Age = 33,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 3,
                    Name = "Miguel",
                    Age = 5,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 4,
                    Name = "Miguel",
                    Age = 14,
                    Email = "ariel@ariel.com",
                },
            };

            if (personRepository.GetPerson(3).Age != 5 ||
                personRepository.GetPerson(2).Name != "Ariel Krako")
            {
                throw new Exception("No se obtuvo la persona correcta");
            }

            Console.WriteLine("Get OK!");
        }

        private void ValidateFilters(IPersonRepositoryBasic personRepository)
        {
            Console.WriteLine("Validando filtros...");

            personRepository.People = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "Ariel",
                    Age = 22,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 2,
                    Name = "Ariel Krako",
                    Age = 33,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 3,
                    Name = "Miguel",
                    Age = 5,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 4,
                    Name = "Miguel",
                    Age = 14,
                    Email = "ariel@ariel.com",
                },
            };

            if (personRepository.GetFiltered("Ariel", 0, null).Count() != 1 ||
                personRepository.GetFiltered("", 0, "ariel").Count() != 4 ||
                personRepository.GetFiltered("Miguel", 33, null).Count() != 0 ||
                personRepository.GetFiltered(null, 14, null).Count() != 1)
            {
                throw new Exception("No se filtro con los parametros requeridos");
            }

            Console.WriteLine("Filtros ok!");
        }

        private void ValidateAdd(IPersonRepositoryBasic personRepository)
        {
            Console.WriteLine("Validando add...");

            personRepository.People = new List<Person>();

            var personOk = new Person()
            {
                Id = 1,
                Name = "Ariel",
                Age = 14,
                Email = "ariel@ariel.com",
            };

            personRepository.Add(personOk);

            if (!personRepository.People.Any(p => p.Id == 1 && p.Name == "Ariel"))
            {
                throw new Exception("No se encuentra la persona insertada");
            }

            personRepository.Add(new Person()
            {
                Id = 2,
                Email = "ariel@ariel.com",
                Age = 14,
            });


            personRepository.Add(new Person()
            {
                Id = 2,
                Email = "ariel@ariel.com",
                Age = 14,
            });

            if (personRepository.People.Count(p => p.Id == 2) != 1)
            {
                throw new Exception("Permitio repetir ids");
            }


            personRepository.Add(new Person()
            {
                Id = 3,
                Email = "ariel@ariel.com",
                Age = 0,
            });

            if (personRepository.People.Any(p => p.Id == 3))
            {
                throw new Exception("Permitio edades menores a 0");
            }


            personRepository.Add(new Person()
            {
                Id = 4,
                Email = "ariel@ariel.com",
                Age = -3,
            });

            if (personRepository.People.Any(p => p.Id == 4))
            {
                throw new Exception("Permitio edades menores a 0");
            }


            personRepository.Add(new Person()
            {
                Id = 5,
                Email = "ariel@ariel",
                Age = 5,
            });

            if (personRepository.People.Any(p => p.Id == 5))
            {
                throw new Exception("Permitio email invalido");
            }

            personRepository.Add(new Person()
            {
                Id = 6,
                Email = "ariel.ariel",
                Age = 5,
            });

            if (personRepository.People.Any(p => p.Id == 6))
            {
                throw new Exception("Permitio email invalido");
            }

            Console.WriteLine("Add OK!");
        }

        private void ValidateUpdate(IPersonRepositoryBasic personRepository)
        {
            Console.WriteLine("Validando update...");

            personRepository.People = new List<Person>();

            var personOk = new Person()
            {
                Id = 1,
                Name = "Ariel",
                Age = 14,
                Email = "ariel@ariel.com",
            };

            personRepository.People.Add(personOk);

            personOk = new Person()
            {
                Id = 1,
                Name = "Ariel Krako",
                Age = 14,
                Email = "ariel@ariel.com",
            };

            personRepository.Update(personOk);

            if (!personRepository.People.Any(p => p.Id == 1 && p.Name == "Ariel Krako"))
            {
                throw new Exception("No se encuentra la persona insertada");
            }

            personRepository.Update(new Person()
            {
                Id = 2,
                Email = "ariel@ariel.com",
                Age = 14,
            });

            if (personRepository.People.Any(p => p.Id == 2))
            {
                throw new Exception("Permitio hacer update en un update inexistente");
            }


            personRepository.Update(new Person()
            {
                Id = 1,
                Email = "ariel@ariel.com",
                Age = 0,
            });

            if (personRepository.People.Any(p => p.Age < 0))
            {
                throw new Exception("Permitio edades menores a 0");
            }


            personRepository.Update(new Person()
            {
                Id = 1,
                Email = "ariel@ariel.com",
                Age = -3,
            });

            if (personRepository.People.Any(p => p.Age < 0))
            {
                throw new Exception("Permitio edades menores a 0");
            }


            personRepository.Update(new Person()
            {
                Id = 1,
                Email = "ariel@ariel",
                Age = 5,
            });

            if (personRepository.People.Any(p => p.Email == "ariel@ariel"))
            {
                throw new Exception("Permitio email invalido");
            }

            personRepository.Update(new Person()
            {
                Id = 1,
                Email = "ariel.ariel",
                Age = 5,
            });

            if (personRepository.People.Any(p => p.Email == "ariel.ariel"))
            {
                throw new Exception("Permitio email invalido");
            }

            Console.WriteLine("Update OK!");
        }

        private void ValidateDelete(IPersonRepositoryBasic personRepository)
        {
            Console.WriteLine("Validando delete...");

            personRepository.People = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "Ariel",
                    Age = 14,
                    Email = "ariel@ariel.com",
                },
                new Person()
                {
                    Id = 2,
                    Name = "Ariel",
                    Age = 14,
                    Email = "ariel@ariel.com",
                }
            };

            personRepository.Delete(2);

            if (personRepository.People.Any(p => p.Id == 2))
            {
                throw new Exception("No borro bien");
            }

            Console.WriteLine("Delete OK!");
        }
    }
}