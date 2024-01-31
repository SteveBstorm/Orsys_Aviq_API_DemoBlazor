using System.ComponentModel.DataAnnotations;

namespace API_DemoBlazor.Models
{
    public class Movie
    {

        public int Id { get; set; }
        public string Title { get; set; }
     
        public int ReleaseYear { get; set; }
     
        public string Synopsis { get; set; }
  
        public Person Realisator { get; set; }
    }

    public class Person
    {
      
        public string LastName { get; set; }
     
        public string FirstName { get; set; }
    }
}
