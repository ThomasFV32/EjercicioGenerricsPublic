using PersonRepository.Entities;
using System.Collections.Generic;

namespace PersonRepository.Interfaces
{
    public interface IPersonRepositoryBasic
    {
        List<Person> People { get; set; }

        /// <summary>
        /// Agrega una persona. 
        /// Validar que:
        /// <list type="bullet">
        ///     <item><description>El Id no exista en la colección.</description></item>
        ///     <item><description>La edad no sea 0 o negativa.</description></item>
        ///     <item><description>El email sea valido.</description></item>
        /// </list>
        /// </summary>
        /// <param name="person">La persona a agregar</param>
        void Add(Person person);

        /// <summary>
        /// Actualiza una persona. 
        /// Validar que:
        /// <list type="bullet">
        ///     <item><description>El Id exista en la colección.</description></item>
        ///     <item><description>La edad no sea 0 o negativa.</description></item>
        ///     <item><description>El email sea valido.</description></item>
        /// </list>
        /// </summary>
        /// <param name="person">La persona a actualizar</param>
        void Update(Person person);
        
        /// <summary>
        /// Borra una persona
        /// </summary>
        /// <param name="id">El id de la persona a boorar</param>
        void Delete(int id);

        /// <summary>
        /// Filtra el resultado de las personas.
        /// </summary>
        /// <param name="name">Nombre a filtrar. Valor vacio o null anula el filtro</param>
        /// <param name="age">Edad a filtrar. Valor 0 anula el filtro</param>
        /// <param name="email">Email a filtrar. Valor vacio o null anula el filtro. Puede filtrar por partes del mail</param>
        /// <returns>El listado de personas filtrado</returns>
        List<Person> GetFiltered(string name, int age, string email);

        /// <summary>
        /// Obtiene una persona 
        /// </summary>
        /// <param name="Id">Id de la persona</param>
        /// <returns>La persona que tiene el <paramref name="Id"/></returns>
        Person GetPerson(int Id);


        /// <summary>
        /// Obtiene la cantidad de personas en el rango de edad
        /// </summary>
        /// <param name="min">Minimo de edad (inclusive)</param>
        /// <param name="max">Maximo de edad (inclusive)</param>
        /// <returns>La cantidad de personas en el rango de edad</returns>
        int GetCountRangeAges(int min, int max);
    }
}
