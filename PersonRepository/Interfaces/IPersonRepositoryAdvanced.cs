using System.Collections.Generic;

namespace PersonRepository.Interfaces
{
    public interface IPersonRepositoryAdvanced : IPersonRepositoryBasic
    {

        /// <summary>
        /// Obtiene los Ids de los usuarios que no tienen los nombres capitalizados (primera letra de los nombres y apellidos en mayusculas)
        /// </summary>
        /// <returns>Array con la lista de ids que no tienen los nombre capitalizado</returns>
        int[] GetNotCapitalizedIds();

        /// <summary>
        /// Obtiene un lista agrupada de los emails de las personas por la cantidad de nombres que tiene su nombre (si es "juan lopez" es 2, si es "juan carlos lopez" es 3, si es "juan carlos lopez garcia" es 4)
        /// </summary>
        /// <returns>Un diccionario que tiene como key la cantidad de nombres, y como value un array con todos los emails de las personas que tienen esa cantidad de nombres en el nombre</returns>
        Dictionary<int, string[]> GroupEmailByNameCount(); 
    }
}
