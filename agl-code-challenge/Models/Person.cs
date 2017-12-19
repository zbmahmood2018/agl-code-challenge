using System.Collections.Generic;

namespace agl_code_challenge.Models
{
    /// <summary>
    ///  Person class 
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Name of person
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// The Age
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// The List of Pets
        /// </summary>
        public List<Pet> Pets { get; set; }
    }
}